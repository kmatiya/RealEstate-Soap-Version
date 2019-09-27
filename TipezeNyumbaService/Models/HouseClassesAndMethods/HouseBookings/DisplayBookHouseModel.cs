using TipezeNyumbaService.Models.HouseClassesAndMethods.GeneralHouses;

namespace TipezeNyumbaService.Models.HouseClassesAndMethods
{
    public class DisplayBookHouseModel:HouseModel
    {
        public string bookedBy { set; get; }
        public string bookedByPhoneNumber { set; get; }
        public string dateOfMeeting { set; get; }
        public string bookingStartTime { get; set; }
        public string bookingEndTime { get; set; }
    }
}