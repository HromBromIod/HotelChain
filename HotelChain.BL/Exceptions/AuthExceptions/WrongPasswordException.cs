namespace HotelChain.BL.Exceptions.AuthExceptions;

public class WrongPasswordException(string message) : ApplicationException(message);