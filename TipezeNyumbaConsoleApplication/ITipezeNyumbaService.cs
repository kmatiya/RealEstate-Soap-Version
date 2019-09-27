using Generic_Repository_and_Unit_of_Work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TipezeNyumbaService.Models;
using TipezeNyumbaService.ModelsWrapperObjects;

namespace TipezeNyumbaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITipezeNyumbaService" in both code and config file together.
    [ServiceContract]
    public interface ITipezeNyumbaService
    {
        //User Management Operation Contracts
        #region 
        [OperationContract]
        List<UserDetails> GetUsers();
        [OperationContract]
        bool AddANewUser(UserDetailsToAddOrUpdate newUser);
        [OperationContract]
        UserDetails GetUserProfile(int userID, string userToken);
        [OperationContract]
        string GetAuntheticationToken(string phoneNumber,string password);
        [OperationContract]
        List<UserAccounState> GetUserAccountStates();
        [OperationContract]
        List<UserType> GetUserRoles();
        [OperationContract]
        List<UserSubscriptionType> GetUserSubscriptionTypes();
        #endregion

        //House Management Operation Contracts
        #region 
        [OperationContract]
        HouseDetails GetSpecificHouse(int houseID);
        [OperationContract]
        List<HouseDetails> GetAllHousesByDistrict(string district);
        [OperationContract]
        List<HouseDetails> GetAllHousesByDateHouseAvailable(DateTime date);
        [OperationContract]
        List<HouseDetails> GetAllHousesByDateRange(DateTime startDate, DateTime endDate);
        [OperationContract]
        List<HousePaymentMode> GetHousePaymentModes();
        [OperationContract]
        List<HouseFenceType> GetHouseFenceType();
        [OperationContract]
        List<HouseForRentState> GetHouseForRentStates();
        [OperationContract]
        List<DistrictInMalawi> GetDistrictsInMalawi();
        [OperationContract]
        string AddAHouse(HouseDetails newHouse);
        [OperationContract]
        string UpdateHouseDetails(HouseDetails houseDetailsFromClient);
        #endregion
        /*[OperationContract]
        string UpLoadHouse(House newHouse, HouseContactDetail houseContactDetails);
        [OperationContract]
        string UpDateHouseDetails(int houseID);
        [OperationContract]
        string RemoveHouse(int houseID);*/
    }
}
