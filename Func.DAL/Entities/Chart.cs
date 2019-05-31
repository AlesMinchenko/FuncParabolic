using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func.DAL.Entities
{
    public class Chart
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual UserData UserData { get; set; }
    }
}
