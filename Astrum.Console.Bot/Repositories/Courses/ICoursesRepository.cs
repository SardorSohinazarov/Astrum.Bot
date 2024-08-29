﻿using Astrum.Console.Bot.Models;
using Astrum.Console.Bot.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astrum.Console.Bot.Repositories.Courses
{
    public interface ICoursesRepository : IBaseRepository<int, Course>
    {
    }
}
