using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace task_2_part_2.Models
{
    [Bind(Exclude= "Id")]
    public class Product
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter product name")]
        [DisplayName("Product name")]
        [StringLength(50, ErrorMessage = "Product name must not exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Plese enter product category")]
        [DisplayName("Product category")]
        [StringLength(50, ErrorMessage = "Product category must not exceed 20 characters")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter product price")]
        [Range(0.1, 9999.0, ErrorMessage = "Sorry, price must be from 0.1 - 9999.0")]
        public decimal Price { get; set; }
    }
}