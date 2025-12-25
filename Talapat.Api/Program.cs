using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies; // Add this using directive
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using System.Threading.Tasks;
using Talabat.Application.AuthService;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Servicies.Contract;
using Talapat.Api.Errors;
using Talapat.Api.Helpers;
using Talapat.Api.Middleware;
using Talapat.Infrastructure.Basket_Repository;
using Talapat.Infrastructure.Data;
using Talapat.Infrastructure.Generic_Repository;
using Talapat.Infrastructure.Identity;


namespace Talapat.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);
            #region Configure Services

            // Add services to the Dependence Injection container.

            webApplicationBuilder.Services.AddControllers();
            // register required api services to DI Container
            webApplicationBuilder.Services.AddDbContext<TalabatDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"),
                    
               sql => sql.MigrationsAssembly("Talapat.Infrastructure"));
            });
            webApplicationBuilder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
             options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"),
               sql => sql.MigrationsAssembly("Talapat.Infrastructure"))
         );
            webApplicationBuilder.Services.AddScoped<IConnectionMultiplexer>(c =>
            {
                var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();
            webApplicationBuilder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = webApplicationBuilder.Configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = webApplicationBuilder.Configuration["JWT:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(webApplicationBuilder.Configuration["JWT:AuthKey"] ?? string.Empty)),
                        ValidateLifetime=true,
                        ClockSkew=TimeSpan.Zero



                    };
                });




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            //DI
            //webApplicationBuilder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            //webApplicationBuilder.Services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();
            webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            webApplicationBuilder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            webApplicationBuilder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            //    webApplicationBuilder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));
            webApplicationBuilder.Services.AddAutoMapper(typeof(MappingProfiles));
            webApplicationBuilder.Services.AddAutoMapper(typeof(Program));
            webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(
                options =>
                {
                    options.InvalidModelStateResponseFactory = ActionContext =>
                    {
                        var errors = ActionContext.ModelState
                            .Where(m => m.Value.Errors.Count > 0)
                            .SelectMany(e => e.Value.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToArray();
                        var response = new ApiValidationErrorResponse() { ValidationErrors=errors };
                        return new BadRequestObjectResult(response);
                       
                        
                    };
                });

        #endregion

        var app = webApplicationBuilder.Build();
             using var scope = app.Services.CreateScope();

            var service = scope.ServiceProvider;
            var _dbContext = service.GetRequiredService<TalabatDbContext>();
            var _identityDbContext = service.GetRequiredService<ApplicationIdentityDbContext>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            var _userManger = service.GetRequiredService<UserManager<ApplicationUser>>();


            try
            {
                await _dbContext.Database.MigrateAsync();
                await _identityDbContext.Database.MigrateAsync();
                await TalabatDbContextSeed.SeedAsync(_dbContext);
                await ApplicationIdentityContextSeed.SeeduserAsync(_userManger);

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }
            #region Configure Kesteral Middelwares
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
