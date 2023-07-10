namespace Application.Exceptions;

public class InvalidLoginException : Exception
{
    private string Email { get; }
    private string Password { get; }

    public InvalidLoginException(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public InvalidLoginException(string message, string email, string password) : base(message)
    {
        Email = email;
        Password = password;
    }

    public InvalidLoginException(string message, Exception innerException, string email, string password) : base(message, innerException)
    {
        Email = email;
        Password = password;
    }

    public InvalidLoginException(string message) : base(message)
    {
    }
}