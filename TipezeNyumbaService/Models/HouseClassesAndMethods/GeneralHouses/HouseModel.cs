using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TipezeNyumbaService.Models.HouseClassesAndMethods.GeneralHouses
{
    public class HouseModel
    {
        public int houseID { get; set; }
        public int bedrooms { get; set; }
        public bool masterBedroomEnsuite { get; set; }
        public bool selfContained { get; set; }
        public Nullable<int> numberOfGarages { get; set; }
        public  string districtHouseIsLocated { get; set; }
        public  string locationInDistrict { get; set; }
        public string dateHouseWillBeAvailable { get; set; }
        public decimal price { get; set; }
        public string dateUploaded { get; set; }
        public string description { get; set; }
        public string modeOfPayment { get; set; }
        public string houseState { get; set; }
        public string fenceType { get; set; }
        public string phoneNumber1 { get; set; }
        public string phoneNumber2 { get; set; }
        public string whatsAppContactNumber { get; set; }
        public string email { get; set; }
        public int userId { get; set; }
    }
}