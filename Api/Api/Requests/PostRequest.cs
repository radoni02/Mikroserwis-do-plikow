namespace Api.Requests
{
    public record PostRequest
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
