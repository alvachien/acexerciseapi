using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace acexerciseapi.Models
{
    public class ExerItemTypeViewModel
    {
        public Int32 ID { get; set; }
        [Required]
        [StringLength(50)]
        public String Name { get; set; }
        [StringLength(100)]
        public String Details { get; set; }
    }
}
