namespace DotaHeroRecommender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CounterPicks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Counter1_Id = c.Int(),
                        Counter2_Id = c.Int(),
                        Counter3_Id = c.Int(),
                        Counter4_Id = c.Int(),
                        Counter5_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Heroes", t => t.Counter1_Id)
                .ForeignKey("dbo.Heroes", t => t.Counter2_Id)
                .ForeignKey("dbo.Heroes", t => t.Counter3_Id)
                .ForeignKey("dbo.Heroes", t => t.Counter4_Id)
                .ForeignKey("dbo.Heroes", t => t.Counter5_Id)
                .Index(t => t.Counter1_Id)
                .Index(t => t.Counter2_Id)
                .Index(t => t.Counter3_Id)
                .Index(t => t.Counter4_Id)
                .Index(t => t.Counter5_Id);
            
            CreateTable(
                "dbo.Heroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Counters_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CounterPicks", t => t.Counters_Id)
                .Index(t => t.Counters_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CounterPicks", "Counter5_Id", "dbo.Heroes");
            DropForeignKey("dbo.CounterPicks", "Counter4_Id", "dbo.Heroes");
            DropForeignKey("dbo.CounterPicks", "Counter3_Id", "dbo.Heroes");
            DropForeignKey("dbo.CounterPicks", "Counter2_Id", "dbo.Heroes");
            DropForeignKey("dbo.CounterPicks", "Counter1_Id", "dbo.Heroes");
            DropForeignKey("dbo.Heroes", "Counters_Id", "dbo.CounterPicks");
            DropIndex("dbo.Heroes", new[] { "Counters_Id" });
            DropIndex("dbo.CounterPicks", new[] { "Counter5_Id" });
            DropIndex("dbo.CounterPicks", new[] { "Counter4_Id" });
            DropIndex("dbo.CounterPicks", new[] { "Counter3_Id" });
            DropIndex("dbo.CounterPicks", new[] { "Counter2_Id" });
            DropIndex("dbo.CounterPicks", new[] { "Counter1_Id" });
            DropTable("dbo.Heroes");
            DropTable("dbo.CounterPicks");
        }
    }
}
