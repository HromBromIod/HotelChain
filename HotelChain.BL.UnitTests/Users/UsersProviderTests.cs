using System.Linq.Expressions;
using HotelChain.BL.UnitTests.Helpers;
using HotelChain.BL.Users.Provider;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository;
using Moq;

namespace HotelChain.BL.UnitTests.Users;

public class UsersProviderTests
{
    [Test]
    public void GetUsersTest()
    {
        Expression expression = null;
        var repositoryMock = new Mock<IRepository<UserEntity>>();
        repositoryMock.Setup(repository => repository.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
            .Callback((Expression<Func<UserEntity, bool>> x) => expression = x);
        var usersProvider = new UsersProvider(repositoryMock.Object, MapperHelper.Mapper);
        var result = usersProvider.GetUsers();
        
        repositoryMock.Verify(repository => repository
            .GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()), Times.Once);
    }
}