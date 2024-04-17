﻿using projectverseAPI.Interfaces.Marker;

namespace projectverseAPI.Models
{
    public class SocialMedia : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
