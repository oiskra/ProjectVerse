
namespace projectverseAPI.Models
{
    public class ProfileComponent
    {
        public Guid Id { get; set; }
        public Guid ProfileDesignerId { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public int ColumnStart { get; set; }
        public int ColumnEnd { get; set; }
        public int RowStart { get; set; }
        public int RowEnd { get; set; }
        public string? Data { get; set; } //JSON
    }
}
