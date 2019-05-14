using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public class TimeTable
    {
        [Key]
        public int TimeTableID { get; set; }

        [Required]
        [DisplayName("Class Name")]
        public string ClassName { get; set; }

        [Required]
        [DisplayName("Class Type")]
        public string ClassType { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public string Day { get; set; }

        [Required]
        public int TeacherID { get; set; }

        [ForeignKey("TeacherID")]
        public virtual Teacher Tch { get; set; }

        [Required]
        public int GroupID { get; set; }

        
        public virtual GroupIdName Group { get; set; }

        public int ModuleID { get; set; }

        [ForeignKey("ModuleID")]
        public virtual Module Mod { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Module Module { get; set; }
    }
}