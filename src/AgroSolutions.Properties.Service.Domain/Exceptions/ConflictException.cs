using System.Runtime.Serialization;

namespace AgroSolutions.Properties.Service.Domain.Exceptions;

[Serializable]
public class ConflictException : CustomExceptionBase
{
    public ConflictException() : base("Já existe um registro com os dados informados.") { }

    public ConflictException(string message) : base(message) { }

    public ConflictException(string message, Exception inner) : base(message, inner) { }

    protected ConflictException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
