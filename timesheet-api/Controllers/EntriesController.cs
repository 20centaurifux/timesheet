using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using timesheet_api.Models;
using timesheet_api.Models.View;
using timesheet_api.Data;

namespace timesheet_api.Controllers
{
    [Authorize]
    [Authorize(Policy="TimesheetOwner")]
    [Route("/[controller]")]
    public class EntriesController : Controller
    {
        private readonly TimesheetContext _context;

        public EntriesController(TimesheetContext context)
        {
            _context = context;
        }

        [HttpGet("/timesheet/{user}/{year}/{month}/{day}")]
        public IEnumerable<EntryView> Get(string user, int year, int month, int day)
        {
            return _context.Entries
                .Where(e => e.Date.Year == year && e.Date.Month == month && e.Date.Day == day
                            && e.User.Name.Equals(user, StringComparison.CurrentCultureIgnoreCase))
                .Include(e => e.Task)
                .Include(e => e.Project)
                .Select(e => EntryView.FromEntry(e));
        }

        private Entry EntryView_TryParse(EntryView entry)
        {
            Project project = null;

            var task = _context.Tasks
                .Include(t => t.ProjectGroup)
                .Single(t => t.Name.Equals(entry.task, StringComparison.CurrentCultureIgnoreCase));

            if(entry.project != null)
            {
                project = _context.Projects
                    .Include(g => g.Group)
                    .Single(p => p.Name.Equals(entry.project, StringComparison.CurrentCultureIgnoreCase));
            }

            if((task.ProjectGroup == null && project == null) || task.ProjectGroup.Id == project.Group.Id)
            {
                return new Entry()
                {
                    Seconds = timesheet_api.Utils.Converter.TimeStringToSeconds(entry.hours),
                    Task = task,
                    Project = project,
                };
            }

            return null;
        }

        [HttpPut("/timesheet/{username}/{year}/{month}/{day}")]
        public IActionResult Put(string username, int year, int month, int day, [FromBody]EntryView entry)
        {
            try
            {
                var newEntry = EntryView_TryParse(entry);
                
                if(newEntry != null)
                {
                    newEntry.Date = new DateTime(year, month, day);
                    newEntry.User = _context.Users.Single(u => u.Name.Equals(username, StringComparison.CurrentCultureIgnoreCase));

                    _context.Entries.Add(newEntry);
                    _context.SaveChanges();
                    
                    entry.id = newEntry.Id;

                    return Created(nameof(Get), entry);
                }
            }
            catch (System.InvalidOperationException) {}
            catch (System.ArgumentException) {}

            return BadRequest();
        }

        [HttpPost("/timesheet/{username}/entries/{id}")]
        public IActionResult Post(string username, int year, int month, int day, [FromBody]EntryView entry)
        {
            try
            {
                var to = _context.Entries.Single(e => e.Id == entry.id);
                var from = EntryView_TryParse(entry);
                
                if(to.User.Name.Equals(username, StringComparison.CurrentCultureIgnoreCase))
                {
                    to.Task = from.Task;
                    to.Project = from.Project;
                    to.Seconds = from.Seconds;

                    _context.Entries.Update(to);
                    _context.SaveChanges();

                    return Ok(Json(EntryView.FromEntry(to)));
                }
                else
                {
                    return Forbid();
                }
            }
            catch (System.InvalidOperationException) {}
            catch (System.ArgumentException) {}

            return BadRequest();
        }

        [HttpDelete("/timesheet/{username}/entries/{id}")]
        public IActionResult Delete(string username, int id)
        {
            try
            {
                var entry = _context.Entries.Single(e => e.Id == id);

                if(entry.User.Name.Equals(username, StringComparison.CurrentCultureIgnoreCase))
                {
                    _context.Entries.Remove(entry);
                    _context.SaveChanges();
                }
                else
                {
                    return Forbid();
                }

                return Ok(Json("ok"));
            }
            catch (System.InvalidOperationException) {}
            catch (System.ArgumentException) {}

            return BadRequest();
        }
    }
}