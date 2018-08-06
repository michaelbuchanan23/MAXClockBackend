namespace MAXClockAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCORS : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Timestamps", "TimeIn", c => c.DateTime());
            AlterColumn("dbo.Timestamps", "TimeOut", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Timestamps", "TimeOut", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Timestamps", "TimeIn", c => c.DateTime(nullable: false));
        }
    }
}
