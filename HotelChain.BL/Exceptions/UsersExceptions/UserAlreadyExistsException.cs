namespace HotelChain.BL.Exceptions.UsersExceptions;

public class UserAlreadyExistsException(string message) : ApplicationException(message);