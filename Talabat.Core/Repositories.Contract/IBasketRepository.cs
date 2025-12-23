using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Basket;

namespace Talabat.Core.Repositories.Contract
{
    public  interface IBasketRepository
    {
        Task<CutomerBasket> GetBasketAsync(string basketId);
        Task<CutomerBasket> UpdateBasketAsync(CutomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
