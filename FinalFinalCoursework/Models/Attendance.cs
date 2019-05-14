using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int StudentID { get; set; }

        [ForeignKey("StudentID")]
        public virtual Student Std { get; set; }

        [Required]
        public int ModuleID { get; set; }

        [ForeignKey("ModuleID")]
        public virtual Module Module { get; set; }

        [Required]
        public int GroupID { get; set; }

        [ForeignKey("GroupID")]
        public virtual GroupIdName Grp { get; set; }

        [Required]
        public Status status { get; set; }

        

        public enum Status
        {
            P,
            A,
            L
        };

       


    }

    

}