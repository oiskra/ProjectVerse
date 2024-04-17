using System.ComponentModel;
using System.Reflection;

namespace projectverseAPI.DTOs
{
    public class ErrorResponseDTO
    {
        public string? Title { get; set; }
        public int? Status { get; set; }
        public object? Errors { get; set; }
    }
}
