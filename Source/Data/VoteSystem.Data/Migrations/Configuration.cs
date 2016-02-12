namespace VoteSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<VoteSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            // TODO Ankk: set to false in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(VoteSystemDbContext context)
        {
            // This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            context.VoteSystems.AddOrUpdate(
                v => v.Name,
                new VoteSystem() { Name = "Ankk", DateCreated = DateTime.Now, EndDateTime = DateTime.Now, StarDateTime = DateTime.Now },
                new VoteSystem() { Name = "Ankk2", DateCreated = DateTime.Now, EndDateTime = DateTime.Now, StarDateTime = DateTime.Now });
        }
    }
}
