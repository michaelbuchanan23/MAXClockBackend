namespace MAXClockAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedclassmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "Instructor_Id", "dbo.Instructors");
            DropIndex("dbo.Classes", new[] { "Instructor_Id" });
            RenameColumn(table: "dbo.Classes", name: "Instructor_Id", newName: "InstructorId");
            AddColumn("dbo.Students", "Class_Id", c => c.Int());
            AlterColumn("dbo.Classes", "InstructorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "InstructorId");
            CreateIndex("dbo.Students", "Class_Id");
            AddForeignKey("dbo.Students", "Class_Id", "dbo.Classes", "Id");
            AddForeignKey("dbo.Classes", "InstructorId", "dbo.Instructors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "InstructorId", "dbo.Instructors");
            DropForeignKey("dbo.Students", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Students", new[] { "Class_Id" });
            DropIndex("dbo.Classes", new[] { "InstructorId" });
            AlterColumn("dbo.Classes", "InstructorId", c => c.Int());
            DropColumn("dbo.Students", "Class_Id");
            RenameColumn(table: "dbo.Classes", name: "InstructorId", newName: "Instructor_Id");
            CreateIndex("dbo.Classes", "Instructor_Id");
            AddForeignKey("dbo.Classes", "Instructor_Id", "dbo.Instructors", "Id");
        }
    }
}
