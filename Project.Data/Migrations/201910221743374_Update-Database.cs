namespace Project.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gun", "OwnerId", c => c.Guid(nullable: false));
            DropColumn("dbo.Gun", "OwneId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gun", "OwneId", c => c.Guid(nullable: false));
            DropColumn("dbo.Gun", "OwnerId");
        }
    }
}
