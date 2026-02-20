using MiniWebService.Domain;

namespace MiniWebService.Services;

public interface IMovieRepository{
    IEnumerable<Movie> GetAll();

    Movie? GetById(Guid id);

    Movie Add(Movie movie);
}