namespace HotelChain.BL.Exceptions.AuthExceptions;

public class WrongCreationUserDataException(string message) : ApplicationException(message);