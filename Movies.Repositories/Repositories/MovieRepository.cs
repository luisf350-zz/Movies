using Microsoft.Extensions.Logging;
using Movies.Entities.Context;
using Movies.Entities.Entities;
using Movies.Repositories.Infrastructure;

namespace Movies.Repositories.Repositories
{
    public class MovieRepository: GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext context, ILogger<MovieRepository> logger) : base(context, logger)
        {
        }
    }
}