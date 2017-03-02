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

        protected override void Seed(ActorContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Actors.AddOrUpdate(
              p => p.FirstName,
              new Actor
              {
                  FirstName = "Sigbjørn",
                  LastName = "Solheim",
                  Address = "Oslogate 2",
                  ImgUrl = "/Images/profiles/profile_andrew_nilsen.jpg",
                  ActorGenderId = 2,
                  BirthDate = DateTime.Parse("1955-06-16 00:00:00.000"),
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
                  ImgUrl = "/Images/profiles/profile_2.jpg",
                  ActorGenderId = 2,
                  BirthDate = DateTime.Parse("1988-06-16 00:00:00.000"),
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
                  ImgUrl = "/Images/profiles/profile_erik_skold.jpg",
                  ActorGenderId = 2,
                  BirthDate = DateTime.Parse("2000-06-16 00:00:00.000"),
                  VoiceTests = new List<VoiceTest>
                  {
                  new VoiceTest { VoiceTestTitle = "Donald 1", VoiceTestUrl = "/audio/siri_1.mp3"},
                  new VoiceTest { VoiceTestTitle = "Langbein 2", VoiceTestUrl = "/audio/siri_2.mp3" }
                  }
              }
            );

            //context.Genders.AddOrUpdate(
            //    g => g.GenderId,
            //    new Gender
            //    {
            //        GenderName = "Both"
            //    },
            //    new Gender
            //    {
            //        GenderName = "Mannlig"
            //    },
            //    new Gender
            //    {
            //        GenderName = "Female"
            //    }
            //    );

            //context.DialectNames.AddOrUpdate(
            //    d => d.DialectListId,
            //    new DialectName
            //    {
            //        DialectListName = "Østlending"
            //    },
            //    new DialectName
            //    {
            //        DialectListName = "Vestlending"
            //    },
            //    new DialectName
            //    {
            //        DialectListName = "Nordlending"
            //    },
            //    new DialectName
            //    {
            //        DialectListName = "Trønder"
            //    },
            //    new DialectName
            //    {
            //        DialectListName = "Sørlending"
            //    }
            //    );



        }
    }
}
