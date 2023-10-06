﻿using Talabat.Core.Entities;

namespace Talabat.APIs.DTOs
{
    public class ProductToReturnDto
    {

        public int Id { get; set; }
       
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int ProductBrandId { get; set; } // Foreign Key 

        public string ProductBrand { get; set; } // Navigational Property [One]


        public int ProductTypeId { get; set; } // Foreign Key 

        public string ProductType { get; set; } // Navigational Property [One]

    }
}
