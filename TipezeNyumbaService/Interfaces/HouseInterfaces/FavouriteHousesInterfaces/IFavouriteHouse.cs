namespace TipezeNyumbaService.Interfaces.HouseInterfaces.FavouriteHousesInterfaces
{
    public interface IFavouriteHouse
    {
        // Adds a house to favourites
        bool AddHouseToFavourites(int houseId, int userId);
        // Remove house from favourites
        bool RemoveHouseFromFavourites(int houseId, int userId);
    }
}