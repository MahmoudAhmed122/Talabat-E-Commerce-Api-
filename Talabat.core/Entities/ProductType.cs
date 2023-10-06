using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class ProductType:BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }


    }
}
