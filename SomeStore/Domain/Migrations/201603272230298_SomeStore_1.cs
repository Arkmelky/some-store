namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeStore_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StoreProducts", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.StoreProducts", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.StoreProducts", "Category", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StoreProducts", "Category", c => c.String());
            AlterColumn("dbo.StoreProducts", "Description", c => c.String());
            AlterColumn("dbo.StoreProducts", "Name", c => c.String());
        }
    }
}
