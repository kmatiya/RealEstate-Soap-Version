using Generic_Repository_and_Unit_of_Work.Unit_of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using TipezeNyumbaService.Interfaces.HouseInterfaces.FavouriteHousesInterfaces;
using TipezeNyumbaService.Models.HouseClassesAndMethods.GeneralHouses;

namespace TipezeNyumbaService.Models.HouseClassesAndMethods.FavouriteHouses
{
    public class HouseFavouritesManagement : UnitOfWorkInstance,IFavouriteHouse, IFavouriteHouseRepository
    {
        private readonly AccountStateManagement _fieldStateManagement = new AccountStateManagement();
        readonly HouseDisplay _houseDisplay = new HouseDisplay();
        public VariableValidation variableValidation = new VariableValidation();
        public bool AddHouseToFavourites(int houseId, int userId)
        {
            try
            {
                HouseFavourite checkHouseFavouriteExist = TipezeNyumbaUnitOfWork.Repository<HouseFavourite>()
                    .Get(u => u.houseID == houseId && u.userID == userId);
                int activatedState = _fieldStateManagement.GetActivatedState().fieldStateID;
                House checkIfHouseExist = TipezeNyumbaUnitOfWork.Repository<House>().Get(u => u.houseID == houseId);
                if (checkIfHouseExist == null)
                {
                    return false;
                }
                if (checkHouseFavouriteExist != null)
                {
                    if (checkHouseFavouriteExist.status == activatedState)
                    {
                        return true;
                    }
                    checkHouseFavouriteExist.status = activatedState;
                    TipezeNyumbaUnitOfWork.Repository<HouseFavourite>().Attach(checkHouseFavouriteExist);
                    TipezeNyumbaUnitOfWork.SaveChanges();
                    return true;
                }
                HouseFavourite newHouseFavourite = new HouseFavourite()
                {
                    userID = userId,
                    houseID = houseId,
                    status = _fieldStateManagement.GetActivatedState().fieldStateID
                };
                TipezeNyumbaUnitOfWork.Repository<HouseFavourite>().Add(newHouseFavourite);
                TipezeNyumbaUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //public bool MakeHouseFavourite(int houseId, int userId)
        //{
        //    HouseFavourite getHouseFavourite = TipezeNyumbaUnitOfWork.Repository<HouseFavourite>()
        //        .Get(u => u.houseID == houseId && u.userID == userId);
        //    if (getHouseFavourite == null)
        //    {
        //        return false;
        //    }
        //    if (getHouseFavourite.FieldState.state.ToLower() == "Activated".ToLower())
        //    {
        //        return true;
        //    }
        //    getHouseFavourite.status = _fieldStateManagement.GetActivatedState().fieldStateID;
        //    TipezeNyumbaUnitOfWork.Repository<HouseFavourite>().Attach(getHouseFavourite);
        //    TipezeNyumbaUnitOfWork.SaveChanges();
        //    return true;
        //}
        public bool RemoveHouseFromFavourites(int houseId, int userId)
        {
            HouseFavourite getHouseFavourite = TipezeNyumbaUnitOfWork.Repository<HouseFavourite>()
                .Get(u => u.houseID == houseId && u.userID == userId);
            if (getHouseFavourite == null)
            {
                return false;
            }
            getHouseFavourite.status = _fieldStateManagement.GetDeactivatedState().fieldStateID;
            TipezeNyumbaUnitOfWork.Repository<HouseFavourite>().Attach(getHouseFavourite);
            TipezeNyumbaUnitOfWork.SaveChanges();
            return true;
        }
        
        public List<HouseModel> GetHousesAsFavourites(int userId)
        {
            List<HouseFavourite> getHouses = TipezeNyumbaUnitOfWork.Repository<HouseFavourite>().GetAll(u => u.userID == userId && u.status == _fieldStateManagement.GetActivatedState().fieldStateID).ToList();
            List<HouseModel> houseList = new List<HouseModel>();
            foreach (var eachHouse in getHouses)
            {
                string paymentModeInWords = eachHouse.House.PaymentMode.number + " " + eachHouse.House.PaymentMode.DurationType.type;
                HouseContactDetail houseContactDetail = TipezeNyumbaUnitOfWork.Repository<HouseContactDetail>()
                    .Get(u => u.houseID == eachHouse.houseID);
                _houseDisplay.SetHouseDetails(houseContactDetail, houseList, eachHouse.House, paymentModeInWords);
            }
            return houseList;
        }

        public HouseModel GetHouseFavouriteById(int houseId, int userId)
        {
            HouseFavourite getFavouriteHouse = TipezeNyumbaUnitOfWork.Repository<HouseFavourite>().Get(u => u.houseID == houseId && u.userID == userId && u.status == _fieldStateManagement.GetActivatedState().fieldStateID);

            if (getFavouriteHouse == null)
            {
                return null;
            }
            string paymentModeInWords = getFavouriteHouse.House.PaymentMode.number + " " + getFavouriteHouse.House.PaymentMode.DurationType.type;
            HouseModel houseToDisplay = new HouseModel()
            {
                houseID = getFavouriteHouse.houseID,
                districtHouseIsLocated = getFavouriteHouse.House.District.name,
                bedrooms = getFavouriteHouse.House.bedrooms,
                masterBedroomEnsuite = getFavouriteHouse.House.masterBedroomEnsuite,
                selfContained = getFavouriteHouse.House.selfContained,
                numberOfGarages = getFavouriteHouse.House.numberOfGarages,
                fenceType = getFavouriteHouse.House.FenceType1.typeOfFence,
                dateHouseWillBeAvailable = getFavouriteHouse.House.dateHouseWillBeAvailable.ToLongDateString(),
                price = getFavouriteHouse.House.price,
                modeOfPayment = paymentModeInWords,
                dateUploaded = getFavouriteHouse.House.dateUploaded.ToLongDateString(),
                description = getFavouriteHouse.House.description,
                houseState = getFavouriteHouse.House.HouseState.HouseStatus
            };
            return houseToDisplay;
        }
    }
}
