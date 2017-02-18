using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Search.Entities
{
    public class SalesLineItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "ItemName")]
        public string ItemName { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Qty")]
        public int Qty { get; set; }

        [Display(Name = "OrderId")]
        public int OrderId { get; set; }

        [Display(Name = "CompanyId")]
        public int CompanyId { get; set; }
    }
}
