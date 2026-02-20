using Microsoft.AspNetCore.Mvc;
using MiniWebService.Domain;
using MiniWebService.Errors;
using MiniWebService.Services;

namespace MiniWebService.Controllers;

[ApiController]
[Route("api/movies")]
public sealed class MovieController : ControllerBase
{
    private readonly IMovieRepository _repo;

    public MovieController(IMovieRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Получить список фильмов.
    /// </summary>
    [HttpGet]

    public IActionResult GetAll(
        [FromQuery] double? minRating,
        [FromQuery] string? sortBy,
        [FromQuery] bool desc = false
    )
    {
        var movies = _repo.GetAll();

        if (minRating.HasValue){
            movies = movies.Where(m => m.Rating >= minRating.Value);
        }

        movies = sortBy?.ToLower() switch
        {
            "name" => desc
            ? movies.OrderByDescending(m => m.Name)
            : movies.OrderBy(m => m.Name),

            "rating" => desc
            ? movies.OrderByDescending(m => m.Rating)
            : movies.OrderBy(m => m.Rating),

            "genre" => desc
            ? movies.OrderByDescending(m => m.Genre)
            : movies.OrderBy(m => m.Genre),
            
            _ => movies
        };

        return Ok(movies);
    }

    /// <summary>
    /// Получить фильм по id.
    /// </summary>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var movie = _repo.GetById(id);

        if (movie == null) {
            throw new NotFoundException($"Movie with id = {id} not found");
        }

        

        return Ok(movie);
    }

    /// <summary>
    /// Создать фильм.
    /// </summary>
    [HttpPost]
    public IActionResult Create(CreateMovieRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name)){
            throw new ValidationException("Name required");
        }

        if (request.Name.Length > 100){            
            throw new ValidationException("Name too long (max 100)");
        }

        if (string.IsNullOrWhiteSpace(request.Genre)){
            throw new ValidationException("Name required");
        }

        if (request.Rating < 0 || request.Rating > 10){
            throw new ValidationException("Rating must be between 0 and 10");
        }
        var movie = new Movie
        {
            Name = request.Name,
            Rating = request.Rating,
            Genre = request.Genre
        };
        var created = _repo.Add(movie);
        return Ok(created);
    }
}