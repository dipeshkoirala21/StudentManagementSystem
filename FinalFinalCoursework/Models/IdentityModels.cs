using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinalFinalCoursework.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<GroupIdName> Groups { get; set; }
        public DbSet<GroupModule> GroupModules { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Student_Modules> Student_Modules { get; set; }
        public DbSet<StudentTeacher> StudentTeachers { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Teacher_Module> Teacher_Modules { get; set; }
        public DbSet<TeacherGroup> TeacherGroups { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<FinalFinalCoursework.Models.login> logins { get; set; }

        public System.Data.Entity.DbSet<FinalFinalCoursework.Models.StudentService> StudentServices { get; set; }
    }
}