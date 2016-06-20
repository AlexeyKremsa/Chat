
namespace Chat.Services
{
    public class BaseService
    {
        protected void ThrowIfNull<TArgument>(TArgument argument)
        {
            if (argument == null)
            {
                throw new System.ArgumentNullException(typeof(TArgument).Name);
            }
        }
    }
}
