using Generic_Repository_and_Unit_of_Work.Unit_of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using TipezeNyumbaService.Interfaces.HouseInterfaces.GeneralHouseInterfaces;

namespace TipezeNyumbaService.Models.HouseClassesAndMethods.GeneralHouses
{
    public class GeneralHouseManagement: UnitOfWorkInstance, IHouseProfile, IHouseRepository
    {
        private readonly AccountStateManagement _fieldStateManagement = new AccountStateManagement();
        readonly HouseDisplay _houseDisplay = new HouseDisplay();
        public VariableValidation variableValidation = new VariableValidation();
        public bool AddHouse(HouseModel newHouseModel)
        {
            District checkDistrictHouseIsLocated = GetDistrictByName(newHouseModel.districtHouseIsLocated);
            if (checkDistrictHouseIsLocated == null)
            {
                return false;
            }
            LocationsInDistrict checkLocationInDistrict = GetLocationInDistrictByName(checkDistrictHouseIsLocated.districtID,newHouseModel.locationInDistrict);
            if (checkLocationInDistrict== null)
            {
                return false;
            }
            HouseState checkHouseState = GetHouseStatesByName(newHouseModel.houseState);
            if (checkHouseState == null)
            {
                return false;
            }
            FenceType checkFenceType = GetFenceTypeByName(newHouseModel.fenceType);
            if (checkFenceType == null)
            {
                return false;
            }
            int activatedState = _fieldStateManagement.GetActivatedState().fieldStateID;

            House newHouse = new House()
            {
                districtHouseIsLocated = checkDistrictHouseIsLocated.districtID,
                locationWithInDistrict = checkLocationInDistrict.districtLocationID,
                bedrooms = newHouseModel.bedrooms,
                masterBedroomEnsuite = newHouseModel.masterBedroomEnsuite,
                selfContained = newHouseModel.selfContained,
                numberOfGarages = newHouseModel.numberOfGarages,
                fenceType = checkFenceType.fenceTypeID,
                dateHouseWillBeAvailable = Convert.ToDateTime(newHouseModel.dateHouseWillBeAvailable),
                price = newHouseModel.price,
                modeOfPayment = Convert.ToInt32(newHouseModel.modeOfPayment),
                dateUploaded = DateTime.Now,
                description = newHouseModel.description,
                currentHouseState = checkHouseState.houseStateID,
                state = activatedState
            };

            HouseContactDetail houseContactDetail = new HouseContactDetail()
            {
                houseID = newHouse.houseID,
                phoneNumber1 = newHouseModel.phoneNumber1,
                phoneNumber2 = newHouseModel.phoneNumber2,
                whatsAppContactNumber = newHouseModel.whatsAppContactNumber,
                email = newHouseModel.email,
                state = activatedState
            };
                

            HouseOwner setHouseOwner = new HouseOwner()
            {
                userID = newHouseModel.userId,
                houseID = newHouseModel.houseID,
                status = activatedState
            };

            TipezeNyumbaUnitOfWork.Repository<House>().Add(newHouse);
            TipezeNyumbaUnitOfWork.Repository<HouseContactDetail>().Add(houseContactDetail);
            TipezeNyumbaUnitOfWork.Repository<HouseOwner>().Add(setHouseOwner);
            TipezeNyumbaUnitOfWork.SaveChanges();
            return true;
        }
        public List<HouseModel> GetAllAvailableHousesByDateRange(string district, DateTime startDate, DateTime endDate, string location = null)
        {
            List<House> getHouses;
            List<HouseModel> houseList = new List<HouseModel>();
            if (location == null)
            {
                getHouses = TipezeNyumbaUnitOfWork.Repository<House>()
                    .GetAll(u => u.District.name.ToLower() == district.ToLower() && u.dateHouseWillBeAvailable >= startDate.Date && u.dateHouseWillBeAvailable <= endDate.Date && u.HouseState.HouseStatus.ToLower() == "available").ToList();
            }
            else
            {
                getHouses = TipezeNyumbaUnitOfWork.Repository<House>()
                    .GetAll(u => u.District.name.ToLower() == district.ToLower() && u.dateHouseWillBeAvailable >= startDate && u.dateHouseWillBeAvailable <= endDate && u.LocationsInDistrict.location.ToLower() == location.ToLower() && u.HouseState.HouseStatus.ToLower() == "available").ToList();
            }
            if (getHouses.Count == 0)
            {
                return null;
            }
            foreach (var eachHouse in getHouses)
            {
                string paymentModeInWords = eachHouse.PaymentMode.number + " " + eachHouse.PaymentMode.DurationType.type;
                HouseContactDetail houseContactDetail = TipezeNyumbaUnitOfWork.Repository<HouseContactDetail>()
                    .Get(u => u.houseID == eachHouse.houseID);
                _houseDisplay.SetHouseDetails(houseContactDetail, houseList, eachHouse, paymentModeInWords);
            }
            return houseList;
        }

        public List<HouseModel> GetAllHouses()
        {
            List<House> getHouses = TipezeNyumbaUnitOfWork.Repository<House>().GetAll().ToList();
            List<HouseModel> houseList = new List<HouseModel>();
            foreach (var eachHouse in getHouses)
            {
                string paymentModeInWords = eachHouse.PaymentMode.number + " "+ eachHouse.PaymentMode.DurationType.type;
                HouseContactDetail houseContactDetail = TipezeNyumbaUnitOfWork.Repository<HouseContactDetail>()
                    .Get(u => u.houseID == eachHouse.houseID);
                _houseDisplay.SetHouseDetails(houseContactDetail, houseList, eachHouse, paymentModeInWords);
            }
            return houseList;
        }

       
        public List<HouseModel> GetAllHousesByDateHouseWillBeAvailable(string  district, DateTime date, string location = null)
        {
            List<House> getHouses;
            List<HouseModel> houseList = new List<HouseModel>();
            if (location == null)
            {
                getHouses = TipezeNyumbaUnitOfWork.Repository<House>()
                    .GetAll(u => u.District.name.ToLower() == district.ToLower() && u.dateHouseWillBeAvailable == date.Date && u.HouseState.HouseStatus.ToLower() == "available").ToList();
            }
            else
            {
                getHouses = TipezeNyumbaUnitOfWork.Repository<House>()
                    .GetAll(u => u.District.name.ToLower() == district.ToLower() && u.dateHouseWillBeAvailable == date.Date && u.LocationsInDistrict.location.ToLower() == location.ToLower() && u.HouseState.HouseStatus.ToLower() == "available").ToList();
            }
            if (getHouses.Count == 0)
            {
                return null;
            }
            foreach (var eachHouse in getHouses)
            {
                string paymentModeInWords = eachHouse.PaymentMode.number + " " + eachHouse.PaymentMode.DurationType.type;
                HouseContactDetail houseContactDetail = TipezeNyumbaUnitOfWork.Repository<HouseContactDetail>()
                    .Get(u => u.houseID == eachHouse.houseID);
                _houseDisplay.SetHouseDetails(houseContactDetail, houseList, eachHouse, paymentModeInWords);
            }
            return houseList;
        }
        public List<HouseModel> GetHousesByPriceRange(string district, decimal minimumPrice, decimal maximumPrice, string location = null)
        {
            List<House> getHouses = string.Equals(location, null, StringComparison.Ordinal) ? TipezeNyumbaUnitOfWork.Repository<House>().GetAll(u => u.District.name.ToLower() == district.ToLower() && u.price >= minimumPrice && u.price <= maximumPrice && u.HouseState.HouseStatus.ToLower() == "available").ToList() : TipezeNyumbaUnitOfWork.Repository<House>().GetAll(u => u.District.name.ToLower() == district.ToLower() && u.price >= minimumPrice && u.price <= maximumPrice && u.LocationsInDistrict.location.ToLower() == location.ToLower() && u.HouseState.HouseStatus.ToLower() == "available").ToList();
            if (getHouses.Count == 0)
            {
                return null;
            }
            List<HouseModel> houseList = new List<HouseModel>();
            foreach (var eachHouse in getHouses)
            {
                string paymentModeInWords = eachHouse.PaymentMode.number + " " + eachHouse.PaymentMode.DurationType.type;
                HouseContactDetail houseContactDetail = TipezeNyumbaUnitOfWork.Repository<HouseContactDetail>()
                    .Get(u => u.houseID == eachHouse.houseID);
                _houseDisplay.SetHouseDetails(houseContactDetail, houseList, eachHouse, paymentModeInWords);
            }
            return houseList;
        }
        public List<HouseModel> GetAllHousesByDistrict(string district)
        {
            List<House> getHouses = TipezeNyumbaUnitOfWork.Repository<House>()
                .GetAll(u => u.District.name.ToLower() == district.ToLower() && u.HouseState.HouseStatus.ToLower() == "available").ToList();
            List<HouseModel> houseList = new List<HouseModel>();
            foreach (var eachHouse in getHouses)
            {
                string paymentModeInWords = eachHouse.PaymentMode.number + " " + eachHouse.PaymentMode.DurationType.type;
                HouseContactDetail houseContactDetail = TipezeNyumbaUnitOfWork.Repository<HouseContactDetail>()
                    .Get(u => u.houseID == eachHouse.houseID);
                _houseDisplay.SetHouseDetails(houseContactDetail, houseList, eachHouse, paymentModeInWords);
            }
            return houseList;
        }
        public bool UpdateHouseStatus(int houseId, string houseStatus)
        {
            HouseState checkHouseState = GetHouseStatesByName(houseStatus);
            if (checkHouseState == null)
            {
                return false;
            }
            try
            {
                House getHouseToUpdate = TipezeNyumbaUnitOfWork.Repository<House>().Get(u => u.houseID == houseId);
                getHouseToUpdate.currentHouseState = checkHouseState.houseStateID;
                TipezeNyumbaUnitOfWork.Repository<House>().Attach(getHouseToUpdate);
                TipezeNyumbaUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private District GetDistrictByName(string district)
        {
            District getDistrict = TipezeNyumbaUnitOfWork.Repository<District>()
                .Get(u => u.name.ToLower() == district.ToLower());
            return getDistrict;
        }

        private HouseState GetHouseStatesByName(string houseState)
        {
            HouseState getHouseState = TipezeNyumbaUnitOfWork.Repository<HouseState>()
                .Get(u => u.HouseStatus.ToLower() == houseState.ToLower());
            return getHouseState;
        }

        private FenceType GetFenceTypeByName(string fenceType)
        {
            FenceType getFenceType = TipezeNyumbaUnitOfWork.Repository<FenceType>()
                .Get(u => u.typeOfFence.ToLower() == fenceType.ToLower());
            return getFenceType;
        }

        private LocationsInDistrict GetLocationInDistrictByName(int districtId,string location)
        {
            LocationsInDistrict getDistrictLocation = TipezeNyumbaUnitOfWork.Repository<LocationsInDistrict>()
                .Get(u => u.districtID == districtId && u.location.ToLower() == location.ToLower());
            return getDistrictLocation;
        }
      
        public bool RemoveHouse(int id)
        {
            House houseToRemove = TipezeNyumbaUnitOfWork.Repository<House>().Get(u => u.houseID == id);
            if (houseToRemove == null) return false;
            TipezeNyumbaUnitOfWork.Repository<House>().Delete(houseToRemove);
            TipezeNyumbaUnitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateHouseDetails(int id, HouseModel houseDetails)
        {
            District checkDistrictHouseIsLocated = GetDistrictByName(houseDetails.districtHouseIsLocated);
            if (checkDistrictHouseIsLocated == null)
            {
                return false;
            }
            LocationsInDistrict checkLocationInDistrict = GetLocationInDistrictByName(checkDistrictHouseIsLocated.districtID, houseDetails.districtHouseIsLocated);
            if (checkLocationInDistrict == null)
            {
                return false;
            }
            HouseState checkHouseState = GetHouseStatesByName(houseDetails.houseState);
            if (checkHouseState == null)
            {
                return false;
            }
            FenceType checkFenceType = GetFenceTypeByName(houseDetails.fenceType);
            if (checkFenceType == null)
            {
                return false;
            }
            
            House houseToUpdate = TipezeNyumbaUnitOfWork.Repository<House>().Get(u => u.houseID == id);
            if (houseToUpdate == null) return false;
            houseToUpdate.districtHouseIsLocated = checkDistrictHouseIsLocated.districtID;
            houseToUpdate.locationWithInDistrict = checkLocationInDistrict.districtLocationID;
            houseToUpdate.bedrooms = houseDetails.bedrooms;
            houseToUpdate.masterBedroomEnsuite = houseDetails.masterBedroomEnsuite;
            houseToUpdate.selfContained = houseDetails.selfContained;
            houseToUpdate.numberOfGarages = houseDetails.numberOfGarages;
            houseToUpdate.fenceType = checkFenceType.fenceTypeID;
            houseToUpdate.dateHouseWillBeAvailable = Convert.ToDateTime(houseDetails.dateHouseWillBeAvailable);
            houseToUpdate.price = houseDetails.price;
            houseToUpdate.modeOfPayment = Convert.ToInt32(houseDetails.modeOfPayment);
            houseToUpdate.dateUploaded = Convert.ToDateTime(houseDetails.dateUploaded);
            houseToUpdate.description = houseDetails.description;
            houseToUpdate.currentHouseState = checkHouseState.houseStateID;

            houseToUpdate.HouseContactDetail.phoneNumber1 = houseDetails.phoneNumber1;
            houseToUpdate.HouseContactDetail.phoneNumber2 = houseDetails.phoneNumber2;
            houseToUpdate.HouseContactDetail.whatsAppContactNumber = houseDetails.whatsAppContactNumber;
            houseToUpdate.HouseContactDetail.email = houseDetails.email;

            TipezeNyumbaUnitOfWork.Repository<House>().Attach(houseToUpdate);
            TipezeNyumbaUnitOfWork.SaveChanges();
            return true;
        }

        public HouseModel GetHouseById(int id)
        {
            House getHouse = TipezeNyumbaUnitOfWork.Repository<House>().Get(u => u.houseID == id);
            
            if (getHouse == null)
            {
                return null;
            }
            string paymentModeInWords = getHouse.PaymentMode.number + " " + getHouse.PaymentMode.DurationType.type;
            HouseModel houseToDisplay = new HouseModel()
            {
                houseID = getHouse.houseID,
                districtHouseIsLocated    = getHouse.District.name,
                bedrooms =  getHouse.bedrooms,
                masterBedroomEnsuite = getHouse.masterBedroomEnsuite,
                selfContained = getHouse.selfContained,
                numberOfGarages = getHouse.numberOfGarages,
                fenceType = getHouse.FenceType1.typeOfFence,
                dateHouseWillBeAvailable = getHouse.dateHouseWillBeAvailable.ToLongDateString(),
                price = getHouse.price,
                modeOfPayment = paymentModeInWords,
                dateUploaded = getHouse.dateUploaded.ToLongDateString(),
                description = getHouse.description,
                houseState = getHouse.HouseState.HouseStatus
            };
            return houseToDisplay;
        }
    }
}
