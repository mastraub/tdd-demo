using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace ModelsTests
{
    [TestClass]
    public class UnitTest1 : ADbIntegrationTest
    {
        private UserCrud _sut;

        [TestInitialize]
        public void Initialize()
        {
            var dbContext = new MyDbContext();
            _sut = new UserCrud(dbContext);
        }

        [TestMethod]
        public void GetAll_NoUsers_EmptyList()
        {
            _sut.GetAll()
                .Should()
                .BeEmpty();
        }

        [TestMethod]
        public void GetAll_SomeUsers_ListOfAllUsers()
        {
            var user1 = new User
            {
                Name = "Name1",
                Email = "a@b.c1"
            };
            var user2 = new User
            {
                Name = "Name2",
                Email = "a@b.c2"
            };

            _sut.Create(user1);
            _sut.Create(user2);

            _sut.GetAll()
                .Select(x => x.Name)
                .Should()
                .BeEquivalentTo("Name1", "Name2");
        }

    }
}