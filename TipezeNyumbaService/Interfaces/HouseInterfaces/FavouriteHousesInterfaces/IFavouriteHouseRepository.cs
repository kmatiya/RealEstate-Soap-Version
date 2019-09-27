using System.Collections.Generic;
using TipezeNyumbaService.Models.HouseClassesAndMethods.GeneralHouses;

namespace TipezeNyumbaService.Interfaces.HouseInterfaces.FavouriteHousesInterfaces
{
    public interface IFavouriteHouseRepository
    {
        // Get houses as favourites
        List<HouseModel> GetHousesAsFavourites(int userId);
        // Get house as favourites by Id
        HouseModel GetHouseFavouriteById(int houseId, int userId);
    }
}