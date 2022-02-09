using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission6.Models
{
    public class TaskContext :DbContext
    {

        public TaskContext (DbContextOptions <TaskContext> options) : base (options)
        {

        }
        public DbSet<TaskModel> responses { get; set; }
        public DbSet<Category> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //seeding the database
            mb.Entity<TaskModel>().HasData(
                new TaskModel
                {
                    TaskId = 1,
                    Task = "Sweep the Floor",
                    Date = "02/11/22",
                    Quadrant = 3,
                    CategoryId = 2,
                    Completed = false
                },
                new TaskModel
                {
                    Task = "Clean the Microwave",
                    Date = "02/11/22",
                    Quadrant = 2,
                    CategoryId = 1,
                    Completed = false
                }
            );
            mb.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Home" },
                new Category { CategoryId = 2, CategoryName = "School" },
                new Category { CategoryId = 3, CategoryName = "Work" },
                new Category { CategoryId = 4, CategoryName = "Church" }
            );
        }
    }
}
