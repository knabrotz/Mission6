using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission6.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission6.Controllers
{
    public class HomeController : Controller
    {
        
        private TaskContext taskContext { get; set; }

        public HomeController(TaskContext x)
        {
            taskContext = x;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Tasks()
        {
            ViewBag.Categories = taskContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Tasks(Task task)
        {
            if (ModelState.IsValid)
            {
                taskContext.Add(task);
                taskContext.SaveChanges();

                return RedirectToAction("TaskGrid");
            }
            else
            {
                ViewBag.Categories = taskContext.Categories.ToList();
                return View(task);
            }
        }

        public IActionResult TaskGrid()
        {
            var tasks = taskContext.Tasks
                .Include(x => x.Task)
                .ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Edit(int taskid)
        {
            ViewBag.Categories = taskContext.Categories.ToList();

            var task = taskContext.Tasks.Single(x => x.TaskId == taskid);
            return View("Tasks", task);
        }

        [HttpPost]
        public IActionResult Edit(Task task)
        {
            taskContext.Update(task);
            taskContext.SaveChanges();

            return RedirectToAction("TaskGrid");
        }

        [HttpGet]
        public IActionResult Delete(int taskid)
        {
            var task = taskContext.Tasks.Single(x => x.TaskId == taskid);
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(Task task)
        {
            taskContext.Tasks.Remove(task);
            taskContext.SaveChanges();
            return RedirectToAction("TaskGrid");
        }
    }
}
