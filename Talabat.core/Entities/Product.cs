using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int ProductBrandId { get; set; } // Foreign Key 

        public ProductBrand ProductBrand { get; set; } // Navigational Property [One]


        public int ProductTypeId { get; set; } // Foreign Key 

        public ProductType ProductType { get; set; } // Navigational Property [One]


    }
}
