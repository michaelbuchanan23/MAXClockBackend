namespace MAXClockAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTimestampModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Timestamps", "PIN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Timestamps", "PIN", c => c.Int(nullable: false));
        }
    }
}
