using System;
using System.ComponentModel.DataAnnotations;

namespace Inveon.Core.Models.DTOs
{
    public class ProductDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0} Can't be null")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} Can't be null")]
        public string Barcode { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} Field must be greater than 1")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} Field must be greater than 1")]
        public int Quantity { get; set; }
        public ProductImage ProductImage { get; set; }
    }
}
