using Moq;
using Movies.Common.Validators;
using Movies.Domain.Implementation;
using Movies.Repositories.Repositories;
using NUnit.Framework;

namespace Movies.Domain.Test
{
    public class BaseRepositoryTest
    {
        protected UserDomain UserDomain;

        protected Mock<IUserRepository> UserRepositoryMock;


        [SetUp]
        public void Setup()
        {
            UserRepositoryMock = new Mock<IUserRepository>();

            UserDomain = new UserDomain(UserRepositoryMock.Object, new RegisterValidator(), new LoginValidator());
        }
    }
}