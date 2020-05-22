using System.Collections.Generic;

namespace Gallery.DAL.Migrations
{
    using Gallery.DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Gallery.DAL.Models.SqlDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Gallery.DAL.Models.SqlDbContext";
        }
    }
}
