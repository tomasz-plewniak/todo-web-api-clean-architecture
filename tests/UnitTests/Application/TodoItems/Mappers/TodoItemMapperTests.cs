using Application.TodoItems.Mappers;
using Contracts.Common.TodoItems;
using Contracts.Responses.TodoItems;
using Domain.TodoItems;
using Domain.Users;

namespace UnitTests.Application.TodoItems.Mappers;

public class TodoItemMapperTests
{
    [Fact]
    public void ToDto_WhenTodoItemEntityProvided_ShouldMapAllPropertiesCorrectly()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        UserEntity userEntity = new("testuser", "test@example.com")
        {
            Id = userId
        };

        TodoItemEntity todoItemEntity = new()
        {
            Id = Guid.NewGuid(),
            Title = "Test Task",
            Description = "Test Description",
            Completed = true,
            DueDate = DateTime.UtcNow.AddDays(7),
            Priority = Priority.High,
            UpdatedAt = DateTime.UtcNow.AddHours(-1),
            UserId = userId,
            UserEntity = userEntity
        };

        // Act
        TodoItemDto result = todoItemEntity.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(todoItemEntity.Id, result.Id);
        Assert.Equal(todoItemEntity.Title, result.Title);
        Assert.Equal(todoItemEntity.Description, result.Description);
        Assert.Equal(todoItemEntity.Completed, result.Completed);
        Assert.Equal(todoItemEntity.DueDate, result.DueDate);
        Assert.Equal(PriorityDto.High, result.Priority);
        Assert.Equal(todoItemEntity.CreatedAt, result.CreatedAt);
        Assert.Equal(todoItemEntity.UpdatedAt, result.UpdatedAt);
        Assert.Equal(todoItemEntity.UserId, result.UserId);
        Assert.NotNull(result.User);
        Assert.Equal(userEntity.Id, result.User.Id);
    }

    [Fact]
    public void ToDto_WhenOptionalPropertiesAreNull_ShouldMapCorrectly()
    {
        // Arrange
        TodoItemEntity todoItemEntity = new()
        {
            Id = Guid.NewGuid(),
            Title = "Minimal Task",
            Description = null,
            Completed = false,
            DueDate = null,
            Priority = null,
            UpdatedAt = null,
            UserId = Guid.NewGuid(),
            UserEntity = null
        };

        // Act
        TodoItemDto result = todoItemEntity.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(todoItemEntity.Id, result.Id);
        Assert.Equal(todoItemEntity.Title, result.Title);
        Assert.Null(result.Description);
        Assert.False(result.Completed);
        Assert.Null(result.DueDate);
        Assert.Null(result.Priority);
        Assert.Equal(todoItemEntity.CreatedAt, result.CreatedAt);
        Assert.Null(result.UpdatedAt);
        Assert.Equal(todoItemEntity.UserId, result.UserId);
        Assert.Null(result.User);
    }

    [Theory]
    [InlineData(Priority.Low, PriorityDto.Low)]
    [InlineData(Priority.Medium, PriorityDto.Medium)]
    [InlineData(Priority.High, PriorityDto.High)]
    public void ToDto_WhenPriorityProvided_ShouldMapCorrectly(Priority entityPriority, PriorityDto expectedDto)
    {
        // Arrange
        TodoItemEntity todoItemEntity = new()
        {
            Id = Guid.NewGuid(),
            Title = "Priority Test Task",
            Priority = entityPriority,
            UserId = Guid.NewGuid()
        };

        // Act
        TodoItemDto result = todoItemEntity.ToDto();

        // Assert
        Assert.Equal(expectedDto, result.Priority);
    }

    [Fact]
    public void ToDto_WhenEmptyCollection_ShouldReturnEmptyCollection()
    {
        // Arrange
        IEnumerable<TodoItemEntity> entities = [];

        // Act
        IEnumerable<TodoItemDto> result = entities.ToDto();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ToDto_WhenCollectionWithMultipleEntities_ShouldMapAllEntities()
    {
        // Arrange
        Guid userId1 = Guid.NewGuid();
        Guid userId2 = Guid.NewGuid();

        TodoItemEntity entity1 = new()
        {
            Id = Guid.NewGuid(),
            Title = "Task 1",
            Description = "Description 1",
            Completed = true,
            Priority = Priority.High,
            UserId = userId1
        };

        TodoItemEntity entity2 = new()
        {
            Id = Guid.NewGuid(),
            Title = "Task 2",
            Description = null,
            Completed = false,
            Priority = Priority.Low,
            UserId = userId2
        };

        List<TodoItemEntity> entities = [entity1, entity2];

        // Act
        List<TodoItemDto> result = entities.ToDto().ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        // Verify first entity mapping
        Assert.Equal(entity1.Id, result[0].Id);
        Assert.Equal(entity1.Title, result[0].Title);
        Assert.Equal(entity1.Description, result[0].Description);
        Assert.Equal(entity1.Completed, result[0].Completed);
        Assert.Equal(PriorityDto.High, result[0].Priority);
        Assert.Equal(entity1.UserId, result[0].UserId);

        // Verify second entity mapping
        Assert.Equal(entity2.Id, result[1].Id);
        Assert.Equal(entity2.Title, result[1].Title);
        Assert.Null(result[1].Description);
        Assert.Equal(entity2.Completed, result[1].Completed);
        Assert.Equal(PriorityDto.Low, result[1].Priority);
        Assert.Equal(entity2.UserId, result[1].UserId);
    }

    [Fact]
    public void ToDto_WhenUserEntityIsProvided_ShouldMapUserCorrectly()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        UserEntity userEntity = new("john_doe", "john@example.com")
        {
            Id = userId,
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        };

        TodoItemEntity todoItemEntity = new()
        {
            Id = Guid.NewGuid(),
            Title = "Task with User",
            UserId = userId,
            UserEntity = userEntity
        };

        // Act
        TodoItemDto result = todoItemEntity.ToDto();

        // Assert
        Assert.NotNull(result.User);
        Assert.Equal(userEntity.Id, result.User.Id);
        Assert.Equal(userEntity.UserName, result.User.UserName);
        Assert.Equal(userEntity.Email, result.User.Email);
        Assert.Equal(userEntity.CreatedAt, result.User.CreatedAt);
        Assert.Equal(userEntity.UpdatedAt, result.User.UpdatedAt);
    }

    [Fact]
    public void ToDto_WhenCompletedStateVaried_ShouldMapCorrectly()
    {
        // Arrange
        TodoItemEntity completedEntity = new()
        {
            Id = Guid.NewGuid(),
            Title = "Completed Task",
            Completed = true,
            UserId = Guid.NewGuid()
        };

        TodoItemEntity incompleteEntity = new()
        {
            Id = Guid.NewGuid(),
            Title = "Incomplete Task",
            Completed = false,
            UserId = Guid.NewGuid()
        };

        // Act
        TodoItemDto completedResult = completedEntity.ToDto();
        TodoItemDto incompleteResult = incompleteEntity.ToDto();

        // Assert
        Assert.True(completedResult.Completed);
        Assert.False(incompleteResult.Completed);
    }

    [Theory]
    [InlineData("Simple Task", "Simple description")]
    [InlineData("Task with special chars!", "Description with émojis 🚀")]
    [InlineData("Long task name with many characters to test boundary conditions", null)]
    public void ToDto_WithVariousTaskData_ShouldMapCorrectly(string title, string? description)
    {
        // Arrange
        TodoItemEntity todoItemEntity = new()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            UserId = Guid.NewGuid()
        };

        // Act
        TodoItemDto result = todoItemEntity.ToDto();

        // Assert
        Assert.Equal(title, result.Title);
        Assert.Equal(description, result.Description);
    }

    [Fact]
    public void ToDto_WhenDueDateInPast_ShouldMapCorrectly()
    {
        // Arrange
        DateTime pastDate = DateTime.UtcNow.AddDays(-5);
        TodoItemEntity todoItemEntity = new()
        {
            Id = Guid.NewGuid(),
            Title = "Overdue Task",
            DueDate = pastDate,
            UserId = Guid.NewGuid()
        };

        // Act
        TodoItemDto result = todoItemEntity.ToDto();

        // Assert
        Assert.Equal(pastDate, result.DueDate);
    }

    [Fact]
    public void ToDto_WhenDueDateInFuture_ShouldMapCorrectly()
    {
        // Arrange
        DateTime futureDate = DateTime.UtcNow.AddDays(10);
        TodoItemEntity todoItemEntity = new()
        {
            Id = Guid.NewGuid(),
            Title = "Future Task",
            DueDate = futureDate,
            UserId = Guid.NewGuid()
        };

        // Act
        TodoItemDto result = todoItemEntity.ToDto();

        // Assert
        Assert.Equal(futureDate, result.DueDate);
    }

    [Fact]
    public void ToDto_WhenEntityHasDefaultGuid_ShouldPreserveGuid()
    {
        // Arrange
        TodoItemEntity todoItemEntity = new()
        {
            Id = Guid.Empty,
            Title = "Test Task",
            UserId = Guid.Empty
        };

        // Act
        TodoItemDto result = todoItemEntity.ToDto();

        // Assert
        Assert.Equal(Guid.Empty, result.Id);
        Assert.Equal(Guid.Empty, result.UserId);
    }
}