namespace FinalFinalCoursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _default : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentServices",
                c => new
                    {
                        StudentServiceID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StudentServiceID);
            
            AddColumn("dbo.Attendances", "StudentService_StudentServiceID", c => c.Int());
            CreateIndex("dbo.Attendances", "StudentService_StudentServiceID");
            AddForeignKey("dbo.Attendances", "StudentService_StudentServiceID", "dbo.StudentServices", "StudentServiceID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "StudentService_StudentServiceID", "dbo.StudentServices");
            DropIndex("dbo.Attendances", new[] { "StudentService_StudentServiceID" });
            DropColumn("dbo.Attendances", "StudentService_StudentServiceID");
            DropTable("dbo.StudentServices");
        }
    }
}
