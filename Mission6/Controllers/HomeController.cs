using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return RedirectToAction("TaskGrid");
        }

        [HttpGet]
        public IActionResult Tasks()
        {
            ViewBag.Categories = taskContext.categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Tasks(TaskModel taskEntry)
        {
            if (ModelState.IsValid)
            {
                taskContext.Add(taskEntry);
                taskContext.SaveChanges();

                return RedirectToAction("TaskGrid");
            }
            else
            {
                ViewBag.Categories = taskContext.categories.ToList();
                return View(taskEntry);
            }
        }

        public IActionResult TaskGrid()
        {
            var tasks = taskContext.responses
                .Include(x => x.Category)
                .ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Edit(int taskid)
        {
            ViewBag.Categories = taskContext.categories.ToList();

            var task = taskContext.responses.Single(x => x.TaskId == taskid);
            return View("Tasks", task);
        }

        [HttpPost]
        public IActionResult Edit(TaskModel taskEntry)
        {
            taskContext.Update(taskEntry);
            taskContext.SaveChanges();

            return RedirectToAction("TaskGrid");
        }

        [HttpGet]
        public IActionResult Delete(int taskid)
        {
            var task = taskContext.responses.Single(x => x.TaskId == taskid);
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(TaskModel taskEntry)
        {
            taskContext.responses.Remove(taskEntry);
            taskContext.SaveChanges();
            return RedirectToAction("TaskGrid");
        }
    }
}
