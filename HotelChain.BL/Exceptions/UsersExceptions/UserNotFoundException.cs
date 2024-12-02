namespace HotelChain.BL.Exceptions.UsersExceptions;

public class UserNotFoundException(string message) : ApplicationException(message);