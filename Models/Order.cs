using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeekFive.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; } 
        public Users User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Authorized";

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Payment Payments { get; set; }


    }
}

