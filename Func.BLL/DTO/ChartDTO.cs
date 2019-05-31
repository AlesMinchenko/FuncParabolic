﻿using Func.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func.BLL.DTO
{
    public class ChartDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual UserDataDTO UserData { get; set; }
    }
}
