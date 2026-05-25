namespace Tutorial7.DTOs
{
    public class PCResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float Weight { get; set; }
        public int Warranty { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Stock { get; set; }
    }
}