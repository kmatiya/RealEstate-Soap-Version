using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipezeNyumbaService.Models.HouseClassesAndMethods
{
    public class BookHouseModel
    {
        public int houseId { set; get; }
        public int userId { set; get; }
        public string dateOfMeeting { set; get; }
        public string bookingStartTime { get; set; }
        public string bookingEndTime { get; set; }
    }
}
