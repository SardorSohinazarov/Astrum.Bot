﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astrum.Console.Bot.Models
{
    public class Course
    {
        //Qales
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
