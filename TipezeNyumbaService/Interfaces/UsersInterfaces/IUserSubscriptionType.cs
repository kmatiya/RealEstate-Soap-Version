using TipezeNyumbaService.Models;

namespace TipezeNyumbaService.Interfaces.UsersInterfaces
{
    public interface IUserSubscriptionType
    {
        SubscriptionType GetSubscriptionTypeById(int id);
        SubscriptionType GetSubscriptionTypeByString(string subscriptionType);
    }
}
