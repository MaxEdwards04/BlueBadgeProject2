namespace Project.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        PrimaryGun = c.String(nullable: false),
                        PrimaryAttach = c.String(nullable: false),
                        SecondaryGun = c.String(nullable: false),
                        SecondaryAttach = c.String(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ClassId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Class");
        }
    }
}
