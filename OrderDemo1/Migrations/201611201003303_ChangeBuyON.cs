namespace OrderDemo1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBuyON : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderHeaders", "BuyOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderHeaders", "BuyOn", c => c.String());
        }
    }
}
