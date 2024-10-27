using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp
{
    public class Order
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public string District { get; set; }
        public DateTime DeliveryTime { get; set; }
        public Order(int id, int weight, string district, DateTime deliveryTime)
        {
            Id = id;
            Weight = weight;
            District = district;
            DeliveryTime = deliveryTime;
        }   
    }
}
