
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Talapat.Repository.Data;

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
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen(); 
            #endregion

            var app = webApplicationBuilder.Build();
             using var scope = app.Services.CreateScope();

            var service = scope.ServiceProvider;
            var _dbContext = service.GetRequiredService<TalabatDbContext>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync();
                await TalabatDbContextSeed.SeedAsync(_dbContext);

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }
            #region Configure Kesteral Middelwares

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
