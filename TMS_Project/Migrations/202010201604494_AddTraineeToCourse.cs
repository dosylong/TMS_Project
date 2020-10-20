namespace TMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTraineeToCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineeToCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TraineeId = c.String(maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId)
                .Index(t => t.TraineeId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeToCourses", "TraineeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TraineeToCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TraineeToCourses", new[] { "CourseId" });
            DropIndex("dbo.TraineeToCourses", new[] { "TraineeId" });
            DropTable("dbo.TraineeToCourses");
        }
    }
}
