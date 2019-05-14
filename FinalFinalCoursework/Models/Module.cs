using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public class Module
    {
        [Key]
        public int ModuleID { get; set; }

        [Required]
        [DisplayName("Module Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Credit Hours")]
        public int CreditHrs { get; set; }

        [Required]
        [DisplayName("Semester Name")]
        public int Sem { get; set; }

        
        [Required]
        public int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public virtual Course Crs { get; set; }

        //public virtual List<Student> students { get; set; }
        public virtual List<Student_Modules> studentModule { get; set; }
        public virtual List<Teacher_Module> teacherModule { get; set; }
        public virtual List<GroupModule> GroupModules { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
    }
}