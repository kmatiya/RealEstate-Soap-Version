namespace TipezeNyumbaService.Interfaces.HouseInterfaces.HouseInterestedClientsInterfaces
{
    public interface IHouseInterestedClients
    {
        // Adds a user under list of interested clients
        bool AddInterestedClient(int houseId, int userId, string clientState);
    }
}