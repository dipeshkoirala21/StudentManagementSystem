using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public class TeacherGroup
    {
        [Key]
        [Column(Order = 1)]
        public int TeacherID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int GroupID { get; set; }
        /*  [Key]
          [Column(Order = 3)]
          public int CourseId { get; set; }*/

        //public int sem{get;set;}

        public virtual Teacher Teacher { get; set; }
        public virtual GroupIdName Group { get; set; }
        //public virtual Course courses { get; set; }
    }
}