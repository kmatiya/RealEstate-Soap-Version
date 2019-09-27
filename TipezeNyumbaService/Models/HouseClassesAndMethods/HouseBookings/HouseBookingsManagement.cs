using Generic_Repository_and_Unit_of_Work.Unit_of_Work;
using System.Collections.Generic;
using System.Linq;
using TipezeNyumbaService.Interfaces.HouseInterfaces.HouseBookingsInterfaces;
using System;

namespace TipezeNyumbaService.Models.HouseClassesAndMethods.HouseBookings
{
    public class HouseBookingsManagement: UnitOfWorkInstance,IHouseBookings
    {
        readonly AccountStateManagement fieldStateManagement = new AccountStateManagement();
        public bool BookHouseAppointment(BookHouseModel newBookHouseModel)
        {
            try
            {
                HouseBooking newHouseBooking = new HouseBooking()
                {
                    houseID = newBookHouseModel.houseId,
                    userID = newBookHouseModel.userId,
                    dateOfMeeting = Convert.ToDateTime(newBookHouseModel.dateOfMeeting),
                    timeFrom = Convert.ToDateTime(newBookHouseModel.bookingStartTime).TimeOfDay,
                    timeTo = Convert.ToDateTime(newBookHouseModel.bookingEndTime).TimeOfDay,
                    status = fieldStateManagement.GetActivatedState().fieldStateID,
                    dateCreated = DateTime.Now
                };
                TipezeNyumbaUnitOfWork.Repository<HouseBooking>().Add(newHouseBooking);
                TipezeNyumbaUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<DisplayBookHouseModel> GetBookedHouses()
        {
            List<HouseBooking> getHouses = TipezeNyumbaUnitOfWork.Repository<HouseBooking>().GetAll().ToList();
            List<DisplayBookHouseModel> houseList = new List<DisplayBookHouseModel>();
            foreach (var eachHouse in getHouses)
            {
                string paymentModeInWords = eachHouse.House.PaymentMode.number + " " + eachHouse.House.PaymentMode.DurationType.type;
                HouseContactDetail houseContactDetail = TipezeNyumbaUnitOfWork.Repository<HouseContactDetail>()
                    .Get(u => u.houseID == eachHouse.houseID);
                if (houseContactDetail == null)
                {
                    houseList.Add(new DisplayBookHouseModel()
                    {
                        houseID = eachHouse.houseID,
                        bedrooms = eachHouse.House.bedrooms,
                        masterBedroomEnsuite = eachHouse.House.masterBedroomEnsuite,
                        selfContained = eachHouse.House.selfContained,
                        numberOfGarages = eachHouse.House.numberOfGarages,
                        dateHouseWillBeAvailable = eachHouse.House.dateHouseWillBeAvailable.ToLongDateString(),
                        price = eachHouse.House.price,
                        dateUploaded = eachHouse.House.dateUploaded.ToLongDateString(),
                        description = eachHouse.House.description,
                        modeOfPayment = paymentModeInWords,
                        houseState = eachHouse.House.HouseState.HouseStatus,
                        fenceType = eachHouse.House.FenceType1.typeOfFence,
                        districtHouseIsLocated = eachHouse.House.District.name,
                        locationInDistrict = eachHouse.House.LocationsInDistrict.location,
                        bookedBy = eachHouse.User.firstName + " " + eachHouse.User.lastName,
                        bookedByPhoneNumber = eachHouse.User.phoneNumber,
                        bookingStartTime = eachHouse.timeFrom.ToString(),
                        bookingEndTime = eachHouse.timeTo.ToString()
                    });
                }
                else
                {
                    houseList.Add(new DisplayBookHouseModel()
                    {
                        houseID = eachHouse.houseID,
                        bedrooms = eachHouse.House.bedrooms,
                        masterBedroomEnsuite = eachHouse.House.masterBedroomEnsuite,
                        selfContained = eachHouse.House.selfContained,
                        numberOfGarages = eachHouse.House.numberOfGarages,
                        dateHouseWillBeAvailable = eachHouse.House.dateHouseWillBeAvailable.ToLongDateString(),
                        price = eachHouse.House.price,
                        dateUploaded = eachHouse.House.dateUploaded.ToLongDateString(),
                        description = eachHouse.House.description,
                        modeOfPayment = paymentModeInWords,
                        houseState = eachHouse.House.HouseState.HouseStatus,
                        fenceType = eachHouse.House.FenceType1.typeOfFence,
                        phoneNumber1 = eachHouse.House.HouseContactDetail.phoneNumber1,
                        phoneNumber2 = eachHouse.House.HouseContactDetail.phoneNumber2,
                        whatsAppContactNumber = eachHouse.House.HouseContactDetail.whatsAppContactNumber,
                        email = eachHouse.House.HouseContactDetail.email,
                        districtHouseIsLocated = eachHouse.House.District.name,
                        locationInDistrict = eachHouse.House.LocationsInDistrict.location,
                        bookedBy = eachHouse.User.firstName + " " + eachHouse.User.lastName,
                        bookedByPhoneNumber = eachHouse.User.phoneNumber,
                        bookingStartTime = eachHouse.timeFrom.ToString(),
                        bookingEndTime = eachHouse.timeTo.ToString()
                    });
                }
            }
            return houseList;
        }
    }
}