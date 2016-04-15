namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeStore_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StoreProducts", "ImageData", c => c.Binary());
            AddColumn("dbo.StoreProducts", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StoreProducts", "ImageMimeType");
            DropColumn("dbo.StoreProducts", "ImageData");
        }
    }
}
