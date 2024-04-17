namespace projectverseAPI.Models
{
    public class CollaborationApplicant
    {
        public Guid Id { get; set; }
        public Guid ApplicantUserId { get; set; }
        public User? ApplicantUser { get; set; }
        public Guid AppliedPositionId { get; set; }
        public CollaborationPosition? AppliedPosition { get; set; }
        public Guid AppliedCollaborationId { get; set; }
        public Collaboration? AppliedCollaboration { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public DateTime AppliedOn { get; set; }
    }

    public enum ApplicationStatus
    {
        Rejected,
        Pending,
        Accepted
    }
}
