using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClub.Data;
using MovieClub.Models;

namespace MovieClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MoviesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<List<Movie>> GetAll()
        {
            return _db.Movies.OrderByDescending(m => m.Votes).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetById(int id)
        {
            var movie = _db.Movies.Find(id);
            if (movie == null) 
            {
                return NotFound();
            };
            return movie;
        }

        [HttpGet("unwatched")]
        public ActionResult<List<Movie>> GetUnwatched()
        {
            return _db.Movies.Where(x => !x.IsWatched).ToList();
        }
        [HttpGet("winner")]
        public ActionResult<Movie> GetMostVoted()
        {
            return _db.Movies.OrderByDescending(x => x.Votes).First();
        }
        [HttpPost]
        public ActionResult<Movie> PostMovie(Movie movie)
        {
            movie.Votes = 0;
            movie.IsWatched = false;
            movie.AddedAt = DateTime.Now;
            return CreatedAtAction(nameof(GetById), new { id = movie.MovieId }, movie);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTitelYearGenre(int id, Movie movie)
        {
            var existing = _db.Movies.Find(id);
            if (existing == null) return NotFound();

            existing.Title = movie.Title;
            existing.Year = movie.Year;
            existing.Genre = movie.Genre;
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id, Movie movie)
        {
            var existing = _db.Movies.Find(id);
            if (existing == null) return NotFound();

            _db.Remove(movie);

            return NoContent();
        }
    }
}
