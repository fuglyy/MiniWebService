namespace MiniWebService.Domain;

public sealed record Movie{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public double Rating {get; set; }
}