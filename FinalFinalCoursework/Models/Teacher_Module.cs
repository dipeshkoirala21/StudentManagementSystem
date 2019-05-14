using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public class Teacher_Module
    {
        [Key]
        [Column(Order = 1)]
        public int TeacherID { get; set; }
         
       

        [Key]
        [Column(Order = 2)]
        public int ModuleID { get; set; }

        [Key]
        [Column(Order = 3)]
        public int GroupID { get; set; }


        public virtual Module Mod { get; set; }
        public virtual Teacher Tch { get; set; }
        //public virtual Group Group { get; set; }
    }
}