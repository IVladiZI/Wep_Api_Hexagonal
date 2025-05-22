namespace Domain.Entities
{
    public class ApiResponse
    {
        public int State { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public object? Exceptions { get; set; }
        public string? Title { get; set; }
        public string? Code { get; set; }
    }
}
