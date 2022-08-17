namespace Api.Requests
{
    public record EditRequest
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
