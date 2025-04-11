using Xunit;
using KooliProjekt.WinFormsApp.Api;

public class ResultOfTTests
{
    [Fact]
    public void Success_ShouldReturnSuccessResultWithValue()
    {
        // Arrange
        int expected = 42;

        // Act
        var result = Result<int>.Success(expected);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.HasError);
        Assert.Null(result.Error);
        Assert.Equal(expected, result.Value);
    }

    [Fact]
    public void Failure_ShouldReturnErrorResultWithDefaultValue()
    {
        // Arrange
        string errorMessage = "Something went wrong";

        // Act
        var result = Result<string>.Failure<string>(errorMessage);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.HasError);
        Assert.Equal(errorMessage, result.Error);
        Assert.Null(result.Value); // default(string) is null
    }
}
