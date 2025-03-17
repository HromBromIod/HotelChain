using FluentAssertions;
using HotelChain.BL.Exceptions.UsersExceptions;
using HotelChain.BL.UnitTests.Helpers;
using HotelChain.BL.Users.Entity;
using HotelChain.BL.Users.Manager;
using HotelChain.DataAccess.Entities;
using HotelChain.Repository;
using Moq;

namespace HotelChain.BL.UnitTests.Users;

public class UsersManagerTests
{
    [Test]
    public void DeleteUserTest()
    {
        var users = new List<UserEntity>
        {
            new()
        };
        
        var usersRepositoryMock = new Mock<IRepository<UserEntity>>();
        usersRepositoryMock.Setup(repository => repository.Delete(It.IsAny<UserEntity>()))
            .Callback(() => { users.RemoveAt(0); });
        usersRepositoryMock.Setup(repository => repository.GetById(0))
            .Returns(new UserEntity());

        var permissionsRepositoryMock = new Mock<IRepository<PermissionEntity>>();
        
        var usersManager = new UsersManager(
            usersRepositoryMock.Object, 
            permissionsRepositoryMock.Object, 
            MapperHelper.Mapper);
        usersManager.DeleteUser(0);
        
        usersRepositoryMock.Verify(repository => repository
            .Delete(It.IsAny<UserEntity>()), Times.Once);
        usersRepositoryMock.Verify(repository => repository
            .GetById(It.IsAny<int>()), Times.Once);
        users.Should().BeEmpty();
    }

    [Test]
    public void UpdateUserTest()
    {
        var userEntity = new UserEntity
        {
            FullName = "aboba"
        };

        var newUserEntity = new UserEntity()
        {
            FullName = "abba"
        };
        
        var usersRepositoryMock = new Mock<IRepository<UserEntity>>();
        usersRepositoryMock.Setup(repository => repository.Save(It.IsAny<UserEntity>()))
            .Returns(() => newUserEntity );
        usersRepositoryMock.Setup(repository => repository.GetById(0))
            .Returns(userEntity);
        
        var permissionsRepositoryMock = new Mock<IRepository<PermissionEntity>>();
        
        var usersManager = new UsersManager(
            usersRepositoryMock.Object, 
            permissionsRepositoryMock.Object, 
            MapperHelper.Mapper);
        var model = usersManager.UpdateUser(0, new UpdateUserModel());
        
        usersRepositoryMock.Verify(repository => repository
            .GetById(It.IsAny<int>()), Times.Once);
        usersRepositoryMock.Verify(repository => repository
            .Save(It.IsAny<UserEntity>()), Times.Once);
        model.FullName.Should().Be("abba");
        
        usersRepositoryMock = new Mock<IRepository<UserEntity>>();
        usersRepositoryMock.Setup(repository => repository.GetById(It.IsAny<int>()))
            .Returns(() => null );
        
        usersManager = new UsersManager(
            usersRepositoryMock.Object, 
            permissionsRepositoryMock.Object, 
            MapperHelper.Mapper);

        var expectedAct = () => usersManager.UpdateUser(0, new UpdateUserModel());
        expectedAct.Should().Throw<UserNotFoundException>();
    }
}