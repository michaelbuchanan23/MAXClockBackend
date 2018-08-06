namespace MAXClockAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedstudentmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Students", new[] { "Class_Id" });
            CreateTable(
                "dbo.StudentClasses",
                c => new
                    {
                        Student_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Class_Id })
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Class_Id);
            
            AddColumn("dbo.Students", "ClassId", c => c.Int(nullable: false));
            DropColumn("dbo.Students", "Class_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Class_Id", c => c.Int());
            DropForeignKey("dbo.StudentClasses", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.StudentClasses", "Student_Id", "dbo.Students");
            DropIndex("dbo.StudentClasses", new[] { "Class_Id" });
            DropIndex("dbo.StudentClasses", new[] { "Student_Id" });
            DropColumn("dbo.Students", "ClassId");
            DropTable("dbo.StudentClasses");
            CreateIndex("dbo.Students", "Class_Id");
            AddForeignKey("dbo.Students", "Class_Id", "dbo.Classes", "Id");
        }
    }
}
