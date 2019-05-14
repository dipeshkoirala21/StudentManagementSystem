using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public enum TGender
    {
        Male,
        Female,
    };
    public enum Type
    {
        Lecturer,
        Tutor,
    };
    public class Teacher
    {

        [Key]
        public int TeacherID { get; set; }

        [Required]
        [DisplayName("Teacher's Name")]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required][DataType(DataType.Password)]
        public string Password { get; set; }

        [Required][DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public TGender Gender { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DisplayName("Teacher Type")]
        public Type Type { get; set; }

        //public virtual List<Student> students { get; set; }
        public virtual List<StudentTeacher> StudentTeachers { get; set; }
        public virtual List<TeacherGroup> TeacherGroups { get; set; }
        public virtual List<Teacher_Module> Teacher_Modules { get; set; }
    }

}