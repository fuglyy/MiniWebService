namespace MiniWebService.Domain;

public sealed record CreateMovieRequest{
    public string Name { get; init; } = "";
    public string Genre { get; init; } = "";
    public double Rating {get; init; }
}