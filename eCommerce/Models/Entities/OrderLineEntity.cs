﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Models.Entities
{
    [Index(nameof(Id), IsUnique = false)]

    public class OrderLineEntity
    {

        public OrderLineEntity()
        {

        }

        public OrderLineEntity(string articleNumber, string productName, decimal productPrice, int amount, decimal linePrice)
        {
            ProductArticleNumber = articleNumber;
            Amount = amount;
            ProductName = productName;
            ProductPrice = productPrice;

            LinePrice = linePrice;
        }

        public OrderLineEntity(int orderId, string productArticleNumber, string productName, decimal productPrice, int amount, decimal linePrice)
        {
            OrderId = orderId;
            ProductArticleNumber = productArticleNumber;
            ProductName = productName;
            ProductPrice = productPrice;
            Amount = amount;

            LinePrice = linePrice;
        }


        //[Key]
        public int Id { get; set; }

        
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }


        [Required]
        public string ProductArticleNumber { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required, Column(TypeName = ("money"))]
        public decimal ProductPrice { get; set; }
        //public ProductEntity Product { get; set; }


        [Required]
        public int Amount { get; set; }

        [Required, Column(TypeName = ("money"))]
        public decimal LinePrice { get; set; } 

    }
}
