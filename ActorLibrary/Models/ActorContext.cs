using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ActorLibrary.Models
{

    public class ActorContext : DbContext
    {
        public ActorContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<ActorContext>(new CreateDatabaseIfNotExists<ActorContext>());
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<VoiceTest> VoiceTests { get; set; }
        public DbSet<Gender> Genders { get; set; }
    }
}