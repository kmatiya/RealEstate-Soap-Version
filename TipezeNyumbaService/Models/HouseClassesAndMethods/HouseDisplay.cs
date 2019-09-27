using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipezeNyumbaService.Models.HouseClassesAndMethods.GeneralHouses;

namespace TipezeNyumbaService.Models.HouseClassesAndMethods
{
    public class HouseDisplay
    {
        public void SetHouseDetails(HouseContactDetail houseContactDetail, List<HouseModel> houseList, House eachHouse,
            string paymentModeInWords)
        {
            if (houseContactDetail == null)
            {
                houseList.Add(new HouseModel()
                {
                    houseID = eachHouse.houseID,
                    bedrooms = eachHouse.bedrooms,
                    masterBedroomEnsuite = eachHouse.masterBedroomEnsuite,
                    selfContained = eachHouse.selfContained,
                    numberOfGarages = eachHouse.numberOfGarages,
                    dateHouseWillBeAvailable = eachHouse.dateHouseWillBeAvailable.ToLongDateString(),
                    price = eachHouse.price,
                    dateUploaded = eachHouse.dateUploaded.ToLongDateString(),
                    description = eachHouse.description,
                    modeOfPayment = paymentModeInWords,
                    houseState = eachHouse.HouseState.HouseStatus,
                    fenceType = eachHouse.FenceType1.typeOfFence,
                    districtHouseIsLocated = eachHouse.District.name,
                    locationInDistrict = eachHouse.LocationsInDistrict.location
                });
            }
            else
            {
                houseList.Add(new HouseModel()
                {
                    houseID = eachHouse.houseID,
                    bedrooms = eachHouse.bedrooms,
                    masterBedroomEnsuite = eachHouse.masterBedroomEnsuite,
                    selfContained = eachHouse.selfContained,
                    numberOfGarages = eachHouse.numberOfGarages,
                    dateHouseWillBeAvailable = eachHouse.dateHouseWillBeAvailable.ToLongDateString(),
                    price = eachHouse.price,
                    dateUploaded = eachHouse.dateUploaded.ToLongDateString(),
                    description = eachHouse.description,
                    modeOfPayment = paymentModeInWords,
                    houseState = eachHouse.HouseState.HouseStatus,
                    fenceType = eachHouse.FenceType1.typeOfFence,
                    phoneNumber1 = eachHouse.HouseContactDetail.phoneNumber1,
                    phoneNumber2 = eachHouse.HouseContactDetail.phoneNumber2,
                    whatsAppContactNumber = eachHouse.HouseContactDetail.whatsAppContactNumber,
                    email = eachHouse.HouseContactDetail.email,
                    districtHouseIsLocated = eachHouse.District.name,
                    locationInDistrict = eachHouse.LocationsInDistrict.location
                });
            }
        }
    }
}
