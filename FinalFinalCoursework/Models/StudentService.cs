using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public class StudentService
    {
        [Key]
        public int StudentServiceID { get; set; }

        [Required]
        [DisplayName("Student Service")]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual List<Attendance> Attendances { get; set; }


    }
}