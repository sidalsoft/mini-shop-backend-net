namespace mini_shop_backend_net.Application.Common.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string message)
        : base(message, 404)
    {
    }
}