using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        [DisplayName("Course Name")]
        public string Name { get; set; }


        public virtual List<Student> students { get; set; }

        public virtual List<Module> modules { get; set; }

        public virtual List<Teacher> teachers { get; set; }
    }
}