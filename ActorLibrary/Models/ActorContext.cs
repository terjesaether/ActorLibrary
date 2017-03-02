using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ActorLibrary.Migrations;

namespace ActorLibrary.Models
{

    public class ActorContext : DbContext
    {
        public ActorContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<ActorContext>(new CreateDatabaseIfNotExists<ActorContext>());
            //Database.SetInitializer<ActorContext>(new MigrateDatabaseToLatestVersion<ActorContext, Configuration>());
            //Database.SetInitializer<ActorContext>(new DropCreateDatabaseIfModelChanges<ActorContext>());
            //Database.SetInitializer<ActorContext>(new DropCreateDatabaseAlways<ActorContext>());

        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<VoiceTest> VoiceTests { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<DialectName> DialectNames { get; set; }
        public DbSet<Dialect> ActorDialects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Actor>()
                .HasMany(t => t.Dialects);

            base.OnModelCreating(modelBuilder);
        }

    }



}