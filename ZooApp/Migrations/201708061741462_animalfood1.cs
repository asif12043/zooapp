namespace ZooApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class animalfood1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnimalFoods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnimalId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                        Quantiy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animals", t => t.AnimalId, cascadeDelete: true)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .Index(t => t.AnimalId)
                .Index(t => t.FoodId);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, name: "Ix_FoodName)");
            
            AlterColumn("dbo.Animals", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Animals", "Origin", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Animals", "Name", name: "Ix_AnimalName)");
            CreateIndex("dbo.Animals", "Origin", name: "Ix_AnimalOrigin)");
            DropColumn("dbo.Animals", "Food");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animals", "Food", c => c.String());
            DropForeignKey("dbo.AnimalFoods", "FoodId", "dbo.Foods");
            DropForeignKey("dbo.AnimalFoods", "AnimalId", "dbo.Animals");
            DropIndex("dbo.Foods", "Ix_FoodName)");
            DropIndex("dbo.Animals", "Ix_AnimalOrigin)");
            DropIndex("dbo.Animals", "Ix_AnimalName)");
            DropIndex("dbo.AnimalFoods", new[] { "FoodId" });
            DropIndex("dbo.AnimalFoods", new[] { "AnimalId" });
            AlterColumn("dbo.Animals", "Origin", c => c.String());
            AlterColumn("dbo.Animals", "Name", c => c.String());
            DropTable("dbo.Foods");
            DropTable("dbo.AnimalFoods");
        }
    }
}
