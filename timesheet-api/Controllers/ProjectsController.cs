using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using timesheet_api.Models;
using timesheet_api.Utils;

namespace timesheet_api.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    public class ProjectsController : Controller
    {
        private TimesheetContext _context;

        public ProjectsController(TimesheetContext context)
        {
            _context = context;
        }

        // GET api/projects
        [HttpGet]
        public Dictionary<string, IEnumerable<string>> Get()
        {
            var m = new Dictionary<string, IEnumerable<string>>();

            foreach(var group in _context.ProjectGroups.Include(g => g.Projects))
            {
                m[group.Name] = group.Projects.Select(p => p.Name);
            }

            return m;
        }
    }
}