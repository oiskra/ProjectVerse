﻿namespace projectverseAPI.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public IList<string> UsedTechnologies { get; set; }
        public bool IsPrivate  { get; set; }
    }
}
