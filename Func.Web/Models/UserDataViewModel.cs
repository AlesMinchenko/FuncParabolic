using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Func.Web.Models
{
    public class UserDataViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int A { get; set; }
        [Required]
        public int B { get; set; }
        [Required]
        public int C { get; set; }

        public decimal Step { get; set; }
        [Required]
        public int RangeFrom { get; set; }
        [Required]
        public int RangeTo { get; set; }

        public virtual ICollection<ChartViewModel> Charts { get; set; }

        public UserDataViewModel()
        {
            Charts = new List<ChartViewModel>();
        }
    }
}