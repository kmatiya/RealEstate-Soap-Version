using System.Collections.Generic;
using TipezeNyumbaService.Models.UserClassesAndMethods;

namespace TipezeNyumbaService.Interfaces.HouseInterfaces.HouseInterestedClientsInterfaces
{
    public interface IHouseInterestedClientsRepository
    {
        List<UserOutputModel> GetInterestedHouseClients(int houseId);

    }
}