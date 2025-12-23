using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Basket;
using Talabat.Core.Repositories.Contract;
using Talapat.Api.Errors;

namespace Talapat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        public IBasketRepository _basketRepository { get; }

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CutomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket is null ? new CutomerBasket(id) : basket);
        }

        [HttpPost]
        public async Task<ActionResult<CutomerBasket>> UpdateBasket(CutomerBasket basket)
        {
            var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);
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
