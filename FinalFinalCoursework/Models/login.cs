using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalFinalCoursework.Models
{
    public class login
    {
        public enum UserType
        {
            Student,
            Teacher,
            StudentService,
            Admin,
        };
        [Key]
        public int loginID { get; set; }

        [DisplayName("Username")]
        public string username { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string password { get; set; }

        [DisplayName("User Type")]
        public UserType usertype { get; set; }
    }
}