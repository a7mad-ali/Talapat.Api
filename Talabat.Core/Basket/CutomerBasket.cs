using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Basket
{
    public class CutomerBasket
    {
        public string Id { get; set; } = null!;
        public List<BasketItem> Items { get; set; } = null!;

        public CutomerBasket(string id) 
        {
            
            Id = id;
            Items = new List<BasketItem>();
        }
    }
}
