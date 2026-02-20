using System.Collections.Concurrent;
using MiniWebService.Domain;

namespace MiniWebService.Services;

/// <summary>
/// Простое хранилище в памяти процесса.
/// </summary>
public sealed class InMemoryMovieRepository : IMovieRepository
{
    private readonly ConcurrentDictionary<Guid, Movie> _movies = new();

    public IEnumerable<Movie> GetAll()
        => _movies.Values
            .OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
            .ToArray();

    public Movie? GetById(Guid id)
        => _movies.TryGetValue(id, out var movie) ? movie : null;

    public Movie Add(Movie movie)
    {
        var id = Guid.NewGuid();
        movie.Id = id;

        _movies[id] = movie;
        return movie;
    }
}
