using AgroSolutions.Properties.Service.Domain.Exceptions;

namespace AgroSolutions.Properties.Tests.Exceptions;

public class NotFoundExceptionTests
{
    [Fact]
    public void NotFoundException_DefaultConstructor_ShouldHaveDefaultMessage()
    {
        // Act
        var exception = new NotFoundException();

        // Assert
        Assert.Equal("Não foi possível localizar os dados solicitados.", exception.Message);
    }

    [Fact]
    public void NotFoundException_CustomMessage_ShouldHaveProvidedMessage()
    {
        // Arrange
        var customMessage = "Propriedade não encontrada.";

        // Act
        var exception = new NotFoundException(customMessage);

        // Assert
        Assert.Equal(customMessage, exception.Message);
    }

    [Fact]
    public void NotFoundException_WithInnerException_ShouldSetInnerException()
    {
        // Arrange
        var innerException = new Exception("Inner exception");
        var customMessage = "Erro interno";

        // Act
        var exception = new NotFoundException(customMessage, innerException);

        // Assert
        Assert.Equal(customMessage, exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }
}

public class ConflictExceptionTests
{
    [Fact]
    public void ConflictException_DefaultConstructor_ShouldHaveDefaultMessage()
    {
        // Act
        var exception = new ConflictException();

        // Assert
        Assert.Equal("Já existe um registro com os dados informados.", exception.Message);
    }

    [Fact]
    public void ConflictException_CustomMessage_ShouldHaveProvidedMessage()
    {
        // Arrange
        var customMessage = "Conflito de dados detected.";

        // Act
        var exception = new ConflictException(customMessage);

        // Assert
        Assert.Equal(customMessage, exception.Message);
    }

    [Fact]
    public void ConflictException_WithInnerException_ShouldSetInnerException()
    {
        // Arrange
        var innerException = new Exception("Inner exception");
        var customMessage = "Erro de conflito";

        // Act
        var exception = new ConflictException(customMessage, innerException);

        // Assert
        Assert.Equal(customMessage, exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }
}

public class CustomExceptionBaseTests
{
    [Fact]
    public void CustomExceptionBase_MessageConstructor_ShouldSetMessage()
    {
        // Arrange
        var customMessage = "Test message";

        // Act
        var exception = new TestCustomException(customMessage);

        // Assert
        Assert.Equal(customMessage, exception.Message);
    }

    [Fact]
    public void CustomExceptionBase_MessageAndInnerException_ShouldSetBoth()
    {
        // Arrange
        var customMessage = "Test message";
        var innerException = new Exception("Inner");

        // Act
        var exception = new TestCustomException(customMessage, innerException);

        // Assert
        Assert.Equal(customMessage, exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    private class TestCustomException : CustomExceptionBase
    {
        public TestCustomException() { }

        public TestCustomException(string message) : base(message) { }

        public TestCustomException(string message, Exception innerException) : base(message, innerException) { }
    }
}
