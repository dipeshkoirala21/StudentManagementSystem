using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public enum SGender
    {
        Male,
        Female
    };
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [DisplayName("Student's Name")]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required][DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required][DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public SGender Gender { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        [DisplayName("Birth Date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DisplayName("Enroll Date")]
        public DateTime EnrollDate { get; set; }

        [Required]
        public int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public virtual Course Crs { get; set; }

        [Required]
        public int GroupID { get; set; }

        [ForeignKey("GroupID")]
        public virtual GroupIdName Grp { get; set; }

        //public virtual List<Teacher> teachers { get; set; }
        public virtual List<Student_Modules> Student_Modules { get; set; }
        public virtual List<StudentTeacher> StudentTeachers { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
    }

}