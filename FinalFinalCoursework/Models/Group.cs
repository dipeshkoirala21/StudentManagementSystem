using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public class GroupIdName
    {
        public enum DivisionType
        {
            C1, C2, C3, C4, C5, C6, C7, D1, D2, D3, D4, D5, D6, D7
        }

        [Key]
        public int GroupID { get; set; }

        [Required]
        [DisplayName("Group")]
        public string Name { get; set; }

        public virtual List<Student> students { get; set; }
        public virtual List<Teacher> Teachers { get; set; }
        public virtual List<TeacherGroup> TeacherGroups { get; set; }
        public virtual List<GroupModule> GroupModules { get; set; }
        public virtual List<TimeTable> TimeTables { get; set; }
    }
}