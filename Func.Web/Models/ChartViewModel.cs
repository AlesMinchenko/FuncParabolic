using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Func.Web.Models
{
    public class ChartViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual UserDataViewModel UserData { get; set; }
    }
}