using Application.Users.Mappers;
using Contracts.Responses.Users;
using Domain.Users;

namespace UnitTests.Application.Users.Mappers;

public class UserMapperTests
{
    [Fact]
    public void ToDto_WhenUserEntityProvided_ShouldMapAllPropertiesCorrectly()
    {
        // Arrange
        UserEntity userEntity = new("john_doe", "john@example.com")
        {
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        UserDto result = userEntity.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Id, result.Id);
        Assert.Equal(userEntity.UserName, result.UserName);
        Assert.Equal(userEntity.Email, result.Email);
        Assert.Equal(userEntity.CreatedAt, result.CreatedAt);
        Assert.Equal(userEntity.UpdatedAt, result.UpdatedAt);
        Assert.Empty(result.TodoItems);
    }

    [Fact]
    public void ToDto_WhenUserEntityHasNullUpdatedAt_ShouldMapCorrectly()
    {
        // Arrange
        UserEntity userEntity = new("jane_doe", "jane@example.com")
        {
            Id = Guid.NewGuid(),
            UpdatedAt = null
        };

        // Act
        UserDto result = userEntity.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Id, result.Id);
        Assert.Equal(userEntity.UserName, result.UserName);
        Assert.Equal(userEntity.Email, result.Email);
        Assert.Equal(userEntity.CreatedAt, result.CreatedAt);
        Assert.Null(result.UpdatedAt);
    }

    [Fact]
    public void ToDto_WhenEmptyCollection_ShouldReturnEmptyCollection()
    {
        // Arrange
        IEnumerable<UserEntity> entities = [];

        // Act
        IEnumerable<UserDto> result = entities.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToDto_WhenCollectionWithMultipleEntities_ShouldMapAllEntities()
    {
        // Arrange
        UserEntity entity1 = new("user1", "user1@example.com")
        {
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        };

        UserEntity entity2 = new("user2", "user2@example.com")
        {
            Id = Guid.NewGuid(),
            UpdatedAt = null
        };

        List<UserEntity> entities = [entity1, entity2];

        // Act
        List<UserDto> result = entities.ToDto().ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        // Verify first entity mapping
        Assert.Equal(entity1.Id, result[0].Id);
        Assert.Equal(entity1.UserName, result[0].UserName);
        Assert.Equal(entity1.Email, result[0].Email);
        Assert.Equal(entity1.CreatedAt, result[0].CreatedAt);
        Assert.Equal(entity1.UpdatedAt, result[0].UpdatedAt);

        // Verify second entity mapping
        Assert.Equal(entity2.Id, result[1].Id);
        Assert.Equal(entity2.UserName, result[1].UserName);
        Assert.Equal(entity2.Email, result[1].Email);
        Assert.Equal(entity2.CreatedAt, result[1].CreatedAt);
        Assert.Null(result[1].UpdatedAt);
    }

    [Fact]
    public void ToDto_WhenCollectionWithSingleEntity_ShouldMapCorrectly()
    {
        // Arrange
        UserEntity entity = new("single_user", "single@example.com")
        {
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.UtcNow
        };

        List<UserEntity> entities = [entity];

        // Act
        List<UserDto> result = entities.ToDto().ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(entity.Id, result[0].Id);
        Assert.Equal(entity.UserName, result[0].UserName);
        Assert.Equal(entity.Email, result[0].Email);
        Assert.Equal(entity.CreatedAt, result[0].CreatedAt);
        Assert.Equal(entity.UpdatedAt, result[0].UpdatedAt);
    }

    [Theory]
    [InlineData("testuser", "test@example.com")]
    [InlineData("admin", "admin@company.com")]
    [InlineData("user123", "user123@domain.org")]
    public void ToDto_WithVariousUserData_ShouldMapCorrectly(string userName, string email)
    {
        // Arrange
        UserEntity userEntity = new(userName, email)
        {
            Id = Guid.NewGuid()
        };

        // Act
        UserDto result = userEntity.ToDto();

        // Assert
        Assert.Equal(userEntity.UserName, result.UserName);
        Assert.Equal(userEntity.Email, result.Email);
    }

    [Fact]
    public void ToDto_WhenEntityHasDefaultGuid_ShouldPreserveGuid()
    {
        // Arrange
        UserEntity userEntity = new("test", "test@example.com")
        {
            Id = Guid.Empty // Empty Guid
        };

        // Act
        UserDto result = userEntity.ToDto();

        // Assert
        Assert.Equal(Guid.Empty, result.Id);
    }
}