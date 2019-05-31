using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func.DAL.Entities
{
    public class UserData
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

        public virtual ICollection<Chart> Charts { get; set; }

        public UserData()
        {
            Charts = new List<Chart>();
        }
    }
}
