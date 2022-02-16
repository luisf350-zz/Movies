using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Movies.Entities.Context;
using Movies.Repositories.Repositories;
using NUnit.Framework;

namespace Movies.Repositories.Test
{
    public class BaseRepositoryTest
    {
        protected AppDbContext Context;
        protected UserRepository UserRepository;
        protected MovieRepository MovieRepository;

        protected Mock<ILogger<UserRepository>> UserRepositoryLogger;
        protected Mock<ILogger<MovieRepository>> MovieRepositoryLogger;

        [SetUp]
        public void Setup()
        {
            UserRepositoryLogger = new Mock<ILogger<UserRepository>>();
            MovieRepositoryLogger = new Mock<ILogger<MovieRepository>>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Test")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            Context = new AppDbContext(options);

            UserRepository = new UserRepository(Context, UserRepositoryLogger.Object);
            MovieRepository = new MovieRepository(Context, MovieRepositoryLogger.Object);

        }

        [TearDown]
        public void TearDown()
        {
            foreach (var item in MovieRepository.GetAll().Result)
            {
                _ = MovieRepository.Delete(item);
            }

            foreach (var item in UserRepository.GetAll().Result)
            {
                _ = UserRepository.Delete(item);
            }
        }
    }
}