using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace acexerciseapi.Models
{
    public class ExerItemViewModel
    {
        public Int32 ID { get; set; }
        [Required]
        [StringLength(100)]
        public String Question { get; set; }
        [Required]
        [StringLength(100)]
        public String Answer { get; set; }
        [Required]
        [StringLength(50)]
        public String Types { get; set; }
        [StringLength(100)]
        public String CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
