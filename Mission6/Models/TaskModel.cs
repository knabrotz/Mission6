using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission6.Models
{
    public class TaskModel
    {
        [Key]
        [Required]
        public string Task { get; set; }

        public string Date { get; set; }
        [Required(ErrorMessage = "You must enter a Quadrant for the Task")]
        public int Quadrant { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool Completed { get; set; }
    }
}