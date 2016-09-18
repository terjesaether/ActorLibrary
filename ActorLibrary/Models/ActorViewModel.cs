using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActorLibrary;

namespace ActorLibrary.Models
{
    public class ActorViewModel
    {
        public Actor Actor { get; set; }

        public List<VoiceTest> VoiceTests { get; set; }
    }
}