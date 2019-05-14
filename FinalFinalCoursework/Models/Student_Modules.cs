using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public class Student_Modules
    {
        [Key]
        [Column(Order = 1)]
        public int StudentID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ModuleID { get; set; }


        public virtual Student Std { get; set; }
        public virtual Module Mod { get; set; }
    }
}