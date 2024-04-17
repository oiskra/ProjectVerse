﻿using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Collaboration
{
    public class CollaborationApplicantDTO
    {
        public Guid Id { get; set; }
        public string ApplicantUserName { get; set; }
        public string ApplicantEmail { get; set; }
        public Guid ApplicantUserId { get; set; }
        public Guid AppliedPositionId { get; set; } 
        public ApplicationStatus ApplicationStatus { get; set; }
        public DateTime AppliedOn { get; set; }
    }
}
