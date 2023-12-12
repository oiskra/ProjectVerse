namespace projectverseAPI.DTOs.User
{
    public class UpdateUserRequestDTO
    {
        public Guid? Id { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
    }
}
