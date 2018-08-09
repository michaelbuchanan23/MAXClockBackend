namespace MAXClockAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTimestampModelPIN : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Timestamps", "PIN", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Timestamps", "PIN");
        }
    }
}
