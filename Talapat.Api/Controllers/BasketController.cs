using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Repositories.Contract;
using Talapat.Api.DTOs;
using Talapat.Api.Errors;

namespace Talapat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CutomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket is null ? new CutomerBasket(id) : basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CutomerBasket>(basket);
            var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (createdOrUpdatedBasket is null) return BadRequest(new ApiResponse(400,"There is and error occured"));
            return Ok(createdOrUpdatedBasket);
            


        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
