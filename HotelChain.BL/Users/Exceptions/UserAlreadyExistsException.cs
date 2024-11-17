namespace HotelChain.BL.Users.Exceptions;

public class UserAlreadyExistsException : ApplicationException
{
    public UserAlreadyExistsException(string message) : base(message) { }
}