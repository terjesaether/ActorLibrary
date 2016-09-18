namespace ActorLibrary.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ActorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ActorLibrary.Models.ActorContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Actors.AddOrUpdate(
              p => p.FirstName,
              new Actor
              {
                  FirstName = "Andrew",
                  LastName = "Nilsen",
                  Address = "Oslogate 2",
                  ImgUrl = "/img/profile_erik_skold.jpg",
                  VoiceTests = new List<VoiceTest>
                  {
                  new VoiceTest { VoiceTestTitle = "sang1", VoiceTestUrl = "/audio/siri_1.mp3"},
                  new VoiceTest { VoiceTestTitle = "sang2", VoiceTestUrl = "/audio/siri_2.mp3" }
                  }
              },

              new Actor
              {
                  FirstName = "Erik",
                  LastName = "Skøld",
                  ImgUrl = "/img/profile_2.jpg",
                  VoiceTests = new List<VoiceTest>
                  {
                  new VoiceTest { VoiceTestTitle = "Prat 1", VoiceTestUrl = "/audio/siri_1.mp3"},
                  new VoiceTest { VoiceTestTitle = "Jalling 2", VoiceTestUrl = "/audio/siri_2.mp3" }
                  }
              },

              new Actor
              {
                  FirstName = "Anderz",
                  LastName = "Eide",
                  ImgUrl = "/img/profile_erik_skold.jpg",
                  VoiceTests = new List<VoiceTest>
                  {
                  new VoiceTest { VoiceTestTitle = "Donald 1", VoiceTestUrl = "/audio/siri_1.mp3"},
                  new VoiceTest { VoiceTestTitle = "Langbein 2", VoiceTestUrl = "/audio/siri_2.mp3" }
                  }
              }
            );

        }
    }
}
