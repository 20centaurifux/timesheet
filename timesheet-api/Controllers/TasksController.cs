using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using timesheet_api.Models;
using timesheet_api.Models.View;
using timesheet_api.Data;

namespace timesheet_api.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    public class TasksController : Controller
    {
        private readonly TimesheetContext _context;

        public  TasksController(TimesheetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Dictionary<string, TaskView> Get()
        {
            var m = new Dictionary<string, TaskView>();

            foreach(var task in _context.Tasks.Include(t => t.ProjectGroup))
            {
                m[task.Name] = new TaskView()
                {
                    productive = task.Productive,
                    projects = task.ProjectGroup == null ? null : task.ProjectGroup.Name
                };
            }

            return m;
        }
    }
}