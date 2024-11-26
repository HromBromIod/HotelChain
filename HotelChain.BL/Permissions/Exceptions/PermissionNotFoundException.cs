namespace HotelChain.BL.Permissions.Exceptions;

public class PermissionNotFoundException : Exception
{
    public PermissionNotFoundException() { }

    public PermissionNotFoundException(string message) : base(message) { }
}