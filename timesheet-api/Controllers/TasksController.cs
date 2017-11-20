using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using timesheet_api.Models;
using timesheet_api.Models.View;

namespace timesheet_api.Controllers
{
    [Route("/[controller]")]
    public class TasksController : Controller
    {
        private TimesheetContext _context;

        public  TasksController(TimesheetContext context)
        {
            _context = context;
        }

        // GET api/tasks
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