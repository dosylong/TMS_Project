namespace TMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseIdToTopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Topics", "CourseId");
            AddForeignKey("dbo.Topics", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "CourseId", "dbo.Courses");
            DropIndex("dbo.Topics", new[] { "CourseId" });
            DropColumn("dbo.Topics", "CourseId");
        }
    }
}
