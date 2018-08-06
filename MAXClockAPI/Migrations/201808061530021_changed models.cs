namespace MAXClockAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedmodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "InstructorId", "dbo.Instructors");
            DropIndex("dbo.Classes", new[] { "InstructorId" });
            RenameColumn(table: "dbo.Classes", name: "InstructorId", newName: "Instructor_Id");
            AlterColumn("dbo.Classes", "Instructor_Id", c => c.Int());
            CreateIndex("dbo.Classes", "Instructor_Id");
            AddForeignKey("dbo.Classes", "Instructor_Id", "dbo.Instructors", "Id");
            DropColumn("dbo.Students", "ClassId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "ClassId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Classes", "Instructor_Id", "dbo.Instructors");
            DropIndex("dbo.Classes", new[] { "Instructor_Id" });
            AlterColumn("dbo.Classes", "Instructor_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Classes", name: "Instructor_Id", newName: "InstructorId");
            CreateIndex("dbo.Classes", "InstructorId");
            AddForeignKey("dbo.Classes", "InstructorId", "dbo.Instructors", "Id", cascadeDelete: true);
        }
    }
}
