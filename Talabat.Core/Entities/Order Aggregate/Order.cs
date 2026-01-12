using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; } = null!;
         //   public int DeliveryMethodId {  get; set; }//foregin key
        public DeliveryMethod DeliveryMethod { get; set; } = null!; // navigational property one 

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        //[NotMapped]
        //public decimal Total =>  SubTotal + DeliveryMethod.Cost;  
        public string PaymentIntentId { get; set; } = string.Empty;
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;

    }
} 