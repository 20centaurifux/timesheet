using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using timesheet_api.Models;
using timesheet_api.Models.View;

namespace timesheet_api.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    public class EntriesController : Controller
    {
        private readonly TimesheetContext _context;

        public EntriesController(TimesheetContext context)
        {
            _context = context;
        }

        // GET api/timesheet/user/year/month/day
        [HttpGet("/timesheet/{user}/{year}/{month}/{day}")]
        public IEnumerable<EntryView> Get(string user, int year, int month, int day)
        {
            return _context.Entries
                .Where(e => e.Date.Year == year && e.Date.Month == month && e.Date.Day == day && e.User.Name.ToLower() == user.ToLower())
                .Include(e => e.Task)
                .Include(e => e.Project)
                .Select(e => EntryView.FromEntry(e));
        }

        [HttpPost("/timesheet/{username}/{year}/{month}/{day}")]
        public EntryView Post(string username, int year, int month, int day, [FromBody]EntryView entry)
        {
            var task = _context.Tasks
                .Include(t => t.ProjectGroup)
                .Single(t => t.Name == entry.task);
            
            var project = _context.Projects
                .Include(g => g.Group)
                .Single(p => p.Name == entry.project);
            
            var user = _context.Users.Single(u => u.Name == username);
            var minutes = timesheet_api.Utils.Converter.TimeStringToMinutes(entry.hours);

            EntryView result = null;

            if(user != null && task.ProjectGroup.Id == project.Group.Id)
            {
                var e = new Entry()
                {
                    Date = new DateTime(year, month, day),
                    Minutes = minutes,
                    Task = task,
                    Project = project,
                    User = user
                };

                _context.Entries.Add(e);
                _context.SaveChanges();
                
                entry.id = e.Id;
                result = entry;
            }

            return result;
        }
    }
}