using Generic_Repository_and_Unit_of_Work.Models;
using Generic_Repository_and_Unit_of_Work.Unit_of_Work;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TipezeNyumbaService" in both code and config file together.
    public class TipezeNyumbaService : ITipezeNyumbaService
    {
        GenericUoW TipezeNyumbaServiceUnitOfWork = new GenericUoW(new FindAHouseEntities());
        HashPassword HashPasswordObject = new HashPassword();
        TokenManagement tokenManagement = new TokenManagement();

        #region
        //This region contains all user related contract implementations
        public List<UserDetails> GetUsers()
        {
            List<User> allusers = TipezeNyumbaServiceUnitOfWork.Repository<User>().GetAll().ToList();
            UserDetails eachUser = new UserDetails();
            List<UserDetails> RetrievedUsers = new List<UserDetails>();
            foreach (User each in allusers)
            {
                eachUser = new UserDetails();
                eachUser.userID = each.userID;
                eachUser.firstName = each.firstName;
                eachUser.lastName = each.lastName;
                eachUser.email = each.email;
                eachUser.phoneNumber = each.phoneNumber;
                eachUser.dateTimeCreated = each.dateTimeCreated;
                eachUser.userSubscriptionType = each.UserSubscriptions.FirstOrDefault().SubscriptionType.type;
                eachUser.accountState = each.FieldState.state;
                eachUser.userRoleForUser = each.UserRole.role;


                RetrievedUsers.Add(eachUser);
            }
            return RetrievedUsers;
        }
        public bool AddANewUser(UserDetailsToAddOrUpdate UserDetailsSubmitted)
        {
            try
            {
                HashPasswordObject = new HashPassword(); 
                User NewUser = new User();
                NewUser.firstName = UserDetailsSubmitted.firstName;
                NewUser.lastName = UserDetailsSubmitted.lastName;
                NewUser.email = UserDetailsSubmitted.email;
                NewUser.phoneNumber = UserDetailsSubmitted.phoneNumber;
                NewUser.dateTimeCreated = DateTime.Now;
                NewUser.userSubscriptionType = Convert.ToInt32(UserDetailsSubmitted.userSubscriptionType);
                NewUser.password = HashPasswordObject.createHashedPassword(UserDetailsSubmitted.password);
                NewUser.passwordSalt = HashPasswordObject.salt;
                NewUser.accountState = getFieldStateID("Activated");
                NewUser.userType = Convert.ToInt32(UserDetailsSubmitted.userRoleForUser);

               UserSubscription registerUserSubscription = new UserSubscription();
               registerUserSubscription.userID = NewUser.userID;
               registerUserSubscription.startDate = DateTime.Now;
               registerUserSubscription.endDate = new DateTime(2019, 3, 09);
               registerUserSubscription.state = getFieldStateID("Activated");
               registerUserSubscription.subcriptionType = Convert.ToInt32(UserDetailsSubmitted.userSubscriptionType);
               TipezeNyumbaServiceUnitOfWork.Repository<User>().Add(NewUser);
               TipezeNyumbaServiceUnitOfWork.Repository<UserSubscription>().Add(registerUserSubscription);
               TipezeNyumbaServiceUnitOfWork.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                return false;
                throw;
            }
        }
        public string GetAuntheticationToken(string phoneNumber, string password)
        {
            User getUserByPhoneNumber = TipezeNyumbaServiceUnitOfWork.Repository<User>().Get(u => u.phoneNumber == phoneNumber);
            if (getUserByPhoneNumber == null)
            {
                return "ERROR:User Phone number is not registered in the system.";
            }
            else
            {
                
                bool checkUserPasswordIsCorrect = HashPasswordObject.isPasswordCorrect(getUserByPhoneNumber, password);
                if (checkUserPasswordIsCorrect == true)
                {
                    /*Include additional code for authenticating user subscription and state of account number*/
                    return tokenManagement.GenerateToken(getUserByPhoneNumber);
                }
                else
                {
                    return "ERROR:Wrong Credentials given";
                }
            }
        }
        public UserDetails GetUserProfile(int userID, string userToken)
        {
            bool ValidateUserToken = tokenManagement.ValidateToken(userToken);
            if (ValidateUserToken == true)
            {
                User SelectedUser = TipezeNyumbaServiceUnitOfWork.Repository<User>().Get(u => u.userID == userID);
                return new UserDetails
                {
                    firstName = SelectedUser.firstName,
                    lastName = SelectedUser.lastName,
                    email = SelectedUser.email,
                    phoneNumber = SelectedUser.phoneNumber,
                    dateTimeCreated = SelectedUser.dateTimeCreated,
                    userSubscriptionType = SelectedUser.UserSubscriptions.FirstOrDefault().SubscriptionType.type,
                    userRoleForUser = SelectedUser.UserRole.role
                };
            }
            else
            {
                return new UserDetails();
            }

        }
        public List<UserAccounState> GetUserAccountStates()
        {
            try
            {
                List<FieldState> currentFieldStatesInDB = TipezeNyumbaServiceUnitOfWork.Repository<FieldState>().GetAll().ToList();
                List<UserAccounState> userStatesAvailable = new List<UserAccounState>();
                foreach (var eachState in currentFieldStatesInDB)
                {
                    userStatesAvailable.Add(new UserAccounState { accountStateID = eachState.fieldStateID, accountState = eachState.state });
                }
                return userStatesAvailable;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<UserType> GetUserRoles()
        {
            try
            {
                List<UserRole> currentUserRolesInDB = TipezeNyumbaServiceUnitOfWork.Repository<UserRole>().GetAll(u => u.FieldState.state == "Activated").ToList();
                List<UserType> userRolesAvailable = new List<UserType>();
                foreach (var eachRole in currentUserRolesInDB)
                {
                    userRolesAvailable.Add(new UserType { userTypeID = eachRole.userRoleID, userRoleForUser = eachRole.role });
                }
                return userRolesAvailable;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<UserSubscriptionType> GetUserSubscriptionTypes()
        {
            try
            {
                List<SubscriptionType> currentSubscriptionTypesInDB = TipezeNyumbaServiceUnitOfWork.Repository<SubscriptionType>().GetAll(u => u.FieldState.state == "Activated").ToList();
                List<UserSubscriptionType> userSubscriptionsAvailable = new List<UserSubscriptionType>();
                foreach (var eachSubscriptionType in currentSubscriptionTypesInDB)
                {
                    userSubscriptionsAvailable.Add(new UserSubscriptionType { subscriptionTypeID= eachSubscriptionType.subscriptionID, subscription = eachSubscriptionType.type + " ("+ eachSubscriptionType.durationNumber+ " "+ eachSubscriptionType.DurationType.type+ ")"});
                }
                return userSubscriptionsAvailable;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region 

        //Implementation of all house related contracts


        public HouseDetails GetSpecificHouse(int houseID)
        {
            try
            {
                House SpecifHouseToGet = TipezeNyumbaServiceUnitOfWork.Repository<House>().Get(u => u.houseID == houseID);
                HouseDetails HouseByHouseID = new HouseDetails();
                HouseByHouseID.houseID = SpecifHouseToGet.houseID;
                HouseByHouseID.district = SpecifHouseToGet.District.name;
                HouseByHouseID.bedrooms = SpecifHouseToGet.bedrooms;
                HouseByHouseID.masterBedroomEnsuite = SpecifHouseToGet.masterBedroomEnsuite;
                HouseByHouseID.selfContained = SpecifHouseToGet.selfContained;
                HouseByHouseID.numberOfGarages = SpecifHouseToGet.numberOfGarages;
                HouseByHouseID.fenceType = SpecifHouseToGet.FenceType1.typeOfFence;
                HouseByHouseID.dateHouseWillBeAvailable = SpecifHouseToGet.dateHouseWillBeAvailable;
                HouseByHouseID.price = SpecifHouseToGet.price;
                HouseByHouseID.modeOfPayment = SpecifHouseToGet.PaymentMode.number + " " + SpecifHouseToGet.PaymentMode.DurationType.type;
                HouseByHouseID.description = SpecifHouseToGet.description;
                HouseByHouseID.houseState = SpecifHouseToGet.HouseState.HouseStatus;
                List<HouseContactDetail> houseContactDetails = SpecifHouseToGet.HouseContactDetails.ToList();
                foreach(var eachHouseontactDetail in houseContactDetails)
                {
                    HouseByHouseID.phoneNumber1 = eachHouseontactDetail.phoneNumber1;
                    HouseByHouseID.phoneNumber2 = eachHouseontactDetail.phoneNumber2;
                    HouseByHouseID.whatsAppContactNumber = eachHouseontactDetail.whatsAppContactNumber;
                    HouseByHouseID.email = eachHouseontactDetail.email;
                }
                return HouseByHouseID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<HouseDetails> GetAllHousesByDateHouseAvailable(DateTime date)
        {
            try
            {
                List<House> HousesByDateAvailble = TipezeNyumbaServiceUnitOfWork.Repository<House>().GetAll(u => u.dateHouseWillBeAvailable == date).ToList();
                List<HouseDetails> availableHouses = new List<HouseDetails>();
                HouseDetails eachHouseDetails = new HouseDetails();
                foreach(House eachHouse in HousesByDateAvailble)
                {
                    eachHouseDetails.houseID = eachHouse.houseID;
                    eachHouseDetails.district = eachHouse.District.name;
                    eachHouseDetails.bedrooms = eachHouse.bedrooms;
                    eachHouseDetails.masterBedroomEnsuite = eachHouse.masterBedroomEnsuite;
                    eachHouseDetails.selfContained = eachHouse.selfContained;
                    eachHouseDetails.numberOfGarages = eachHouse.numberOfGarages;
                    eachHouseDetails.fenceType = eachHouse.FenceType1.typeOfFence;
                    eachHouseDetails.dateHouseWillBeAvailable = eachHouse.dateHouseWillBeAvailable;
                    eachHouseDetails.price = eachHouse.price;
                    eachHouseDetails.modeOfPayment = eachHouse.PaymentMode.number + " " + eachHouse.PaymentMode.DurationType.type;
                    eachHouseDetails.description = eachHouse.description;
                    eachHouseDetails.houseState = eachHouse.HouseState.HouseStatus;


                    availableHouses.Add(eachHouseDetails);
                }
                return availableHouses;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<HouseDetails> GetAllHousesByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<House> HousesByDateAvailble = TipezeNyumbaServiceUnitOfWork.Repository<House>().GetAll(u => u.dateHouseWillBeAvailable >= startDate && u.dateHouseWillBeAvailable <= endDate).ToList();
                List<HouseDetails> availableHouses = new List<HouseDetails>();
                HouseDetails eachHouseDetails = new HouseDetails();
                foreach (House eachHouse in HousesByDateAvailble)
                {
                    eachHouseDetails.houseID = eachHouse.houseID;
                    eachHouseDetails.district = eachHouse.District.name;
                    eachHouseDetails.bedrooms = eachHouse.bedrooms;
                    eachHouseDetails.masterBedroomEnsuite = eachHouse.masterBedroomEnsuite;
                    eachHouseDetails.selfContained = eachHouse.selfContained;
                    eachHouseDetails.numberOfGarages = eachHouse.numberOfGarages;
                    eachHouseDetails.fenceType = eachHouse.FenceType1.typeOfFence;
                    eachHouseDetails.dateHouseWillBeAvailable = eachHouse.dateHouseWillBeAvailable;
                    eachHouseDetails.price = eachHouse.price;
                    eachHouseDetails.modeOfPayment = eachHouse.PaymentMode.number + " " + eachHouse.PaymentMode.DurationType.type;
                    eachHouseDetails.description = eachHouse.description;
                    eachHouseDetails.houseState = eachHouse.HouseState.HouseStatus;
                    List<HouseContactDetail> houseContactDetails = eachHouse.HouseContactDetails.ToList();
                    foreach (var eachHouseContactDetail in houseContactDetails)
                    {
                        eachHouseContactDetail.phoneNumber1 = eachHouseContactDetail.phoneNumber1;
                        eachHouseContactDetail.phoneNumber2 = eachHouseContactDetail.phoneNumber2;
                        eachHouseContactDetail.whatsAppContactNumber = eachHouseContactDetail.whatsAppContactNumber;
                        eachHouseContactDetail.email = eachHouseContactDetail.email;
                    }
                    availableHouses.Add(eachHouseDetails);
                }
                return availableHouses;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<HouseDetails> GetAllHousesByDistrict(string district)
        {
            try
            {
                List<House> HousesInRepository = TipezeNyumbaServiceUnitOfWork.Repository<House>().GetAll(u =>u.District.name ==district).ToList();
                List<HouseDetails> HousesToBeRetrieved = new List<HouseDetails>();
                HouseDetails EachHouseDetails = new HouseDetails();
                foreach(House eachHouse in HousesInRepository)
                {
                    EachHouseDetails = new HouseDetails();
                    EachHouseDetails.houseID = eachHouse.houseID;
                    EachHouseDetails.bedrooms = eachHouse.bedrooms;
                    EachHouseDetails.masterBedroomEnsuite = eachHouse.masterBedroomEnsuite;
                    EachHouseDetails.selfContained = eachHouse.selfContained;
                    EachHouseDetails.numberOfGarages = eachHouse.numberOfGarages;
                    EachHouseDetails.dateHouseWillBeAvailable = eachHouse.dateHouseWillBeAvailable;
                    EachHouseDetails.price = eachHouse.price;
                    EachHouseDetails.dateUploaded = eachHouse.dateUploaded;
                    EachHouseDetails.description = eachHouse.description;
                    EachHouseDetails.houseState = eachHouse.HouseState.HouseStatus;
                    EachHouseDetails.fenceType = eachHouse.FenceType1.typeOfFence;
                    EachHouseDetails.district = eachHouse.District.name;
                    EachHouseDetails.modeOfPayment = eachHouse.PaymentMode.number + " " + eachHouse.PaymentMode.DurationType.type;
                    List<HouseContactDetail> houseContactDetails = eachHouse.HouseContactDetails.ToList();
                    foreach (var eachHouseontactDetail in houseContactDetails)
                    {
                        EachHouseDetails.phoneNumber1 = eachHouseontactDetail.phoneNumber1;
                        EachHouseDetails.phoneNumber2 = eachHouseontactDetail.phoneNumber2;
                        EachHouseDetails.whatsAppContactNumber = eachHouseontactDetail.whatsAppContactNumber;
                        EachHouseDetails.email = eachHouseontactDetail.email;
                    }
                    HousesToBeRetrieved.Add(EachHouseDetails);
                }
                return HousesToBeRetrieved;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<HousePaymentMode> GetHousePaymentModes()
        {
            try
            {
                List<PaymentMode> currentPaymentModesInDB = TipezeNyumbaServiceUnitOfWork.Repository<PaymentMode>().GetAll(u => u.FieldState.state == "Activated").ToList();
                List<HousePaymentMode> paymentModesAvailable = new List<HousePaymentMode>();
                HousePaymentMode eachPayment = new HousePaymentMode();
                foreach (var mode in currentPaymentModesInDB)
                {
                    eachPayment = new HousePaymentMode();
                    eachPayment.modeOfPaymentID = mode.modeOfPaymentID;
                    eachPayment.paymentMode = mode.number + " " + mode.DurationType.type;
                    paymentModesAvailable.Add(eachPayment);
                }
                return paymentModesAvailable;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public List<HouseFenceType> GetHouseFenceType()
        {
            try
            {
                List<FenceType> currentFenceTypesInDB = TipezeNyumbaServiceUnitOfWork.Repository<FenceType>().GetAll(u => u.FieldState.state =="Activated").ToList();
                List<HouseFenceType> fenceTypesAvailable = new List<HouseFenceType>();
                HouseFenceType eachFenceType = new HouseFenceType();
                foreach (var eachtype in currentFenceTypesInDB)
                {
                    fenceTypesAvailable.Add(new HouseFenceType { fenceTypeID = eachtype.fenceTypeID, HouseFensetype = eachtype.typeOfFence });
                }
                return fenceTypesAvailable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<HouseForRentState> GetHouseForRentStates()
        {
            try
            {
                List<HouseState> currentHouseStateInDB = TipezeNyumbaServiceUnitOfWork.Repository<HouseState>().GetAll().ToList();
                List<HouseForRentState> HouseStatesAvailable = new List<HouseForRentState>();
                foreach (var eachtype in currentHouseStateInDB)
                {
                   HouseStatesAvailable.Add(new HouseForRentState{ houseStateID = eachtype.houseStateID, HouseStatus =eachtype.HouseStatus });
                }
                return HouseStatesAvailable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<DistrictInMalawi> GetDistrictsInMalawi()
        {
            try
            {
                List<District> currentDistrictsInDB = TipezeNyumbaServiceUnitOfWork.Repository<District>().GetAll(u=>u.FieldState.state == "Activated").ToList();
                List<DistrictInMalawi> districtsAvailable = new List<DistrictInMalawi>();
                foreach (var currentDistrict in currentDistrictsInDB)
                {
                    districtsAvailable.Add(new DistrictInMalawi { districtID = currentDistrict.districtID, name = currentDistrict.name });
                }
                return districtsAvailable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string AddAHouse(HouseDetails newHouse)
        {

            try
            {
                //House details to put in house entity
                House houseToAddInDB = new House();
                houseToAddInDB.districtHouseIsLocated = Convert.ToInt32(newHouse.district);
                houseToAddInDB.bedrooms = newHouse.bedrooms;
                houseToAddInDB.masterBedroomEnsuite = newHouse.masterBedroomEnsuite;
                houseToAddInDB.selfContained = newHouse.selfContained;
                houseToAddInDB.numberOfGarages = newHouse.numberOfGarages;
                houseToAddInDB.fenceType = Convert.ToInt32(newHouse.fenceType);
                houseToAddInDB.dateHouseWillBeAvailable = newHouse.dateHouseWillBeAvailable;
                houseToAddInDB.price = newHouse.price;
                houseToAddInDB.modeOfPayment = Convert.ToInt32(newHouse.modeOfPayment);
                houseToAddInDB.dateUploaded = DateTime.Now;
                houseToAddInDB.description = newHouse.description;
                houseToAddInDB.currentHouseState = Convert.ToInt32(newHouse.houseState);
                houseToAddInDB.state = getFieldStateID("Activated");

                
                //House contact details to add for new house
                HouseContactDetail newHouseDetail = new HouseContactDetail();
                newHouseDetail.phoneNumber1 = newHouse.phoneNumber1;
                newHouseDetail.phoneNumber2 = newHouse.phoneNumber2;
                newHouseDetail.email = newHouse.email;
                newHouseDetail.whatsAppContactNumber = newHouse.whatsAppContactNumber;
                newHouseDetail.houseID = houseToAddInDB.houseID;
                newHouseDetail.state = getFieldStateID("Activated");
                TipezeNyumbaServiceUnitOfWork.Repository<House>().Add(houseToAddInDB);
                TipezeNyumbaServiceUnitOfWork.Repository<HouseContactDetail>().Add(newHouseDetail);
                TipezeNyumbaServiceUnitOfWork.SaveChanges();

                return "Successful";
            }
            catch (Exception)
            {
                return "Failed to add new house, please confirm if the input parameters were inserted as required";
            }
        }
        int getFieldStateID(string stateInWords)
        {
            FieldState requistedFieldState = TipezeNyumbaServiceUnitOfWork.Repository<FieldState>().Get(u => u.state == stateInWords);
            return requistedFieldState.fieldStateID;
        }

        public string UpdateHouseDetails(HouseDetails houseDetailsFromClient)
        {
            try
            {
                //Updating the house table
                House houseToUpdateInDB = TipezeNyumbaServiceUnitOfWork.Repository<House>().Get(u => u.houseID == houseDetailsFromClient.houseID);
                houseToUpdateInDB.districtHouseIsLocated = Convert.ToInt32(houseDetailsFromClient.district);
                houseToUpdateInDB.bedrooms = houseDetailsFromClient.bedrooms;
                houseToUpdateInDB.masterBedroomEnsuite = houseDetailsFromClient.masterBedroomEnsuite;
                houseToUpdateInDB.selfContained = houseDetailsFromClient.selfContained;
                houseToUpdateInDB.numberOfGarages = houseDetailsFromClient.numberOfGarages;
                houseToUpdateInDB.fenceType = Convert.ToInt32(houseDetailsFromClient.fenceType);
                houseToUpdateInDB.dateHouseWillBeAvailable = houseDetailsFromClient.dateHouseWillBeAvailable;
                houseToUpdateInDB.price = houseDetailsFromClient.price;
                houseToUpdateInDB.modeOfPayment = Convert.ToInt32(houseDetailsFromClient.modeOfPayment);
                houseToUpdateInDB.dateUploaded = DateTime.Now;
                houseToUpdateInDB.description = houseDetailsFromClient.description;
                houseToUpdateInDB.currentHouseState = Convert.ToInt32(houseDetailsFromClient.houseState);

                //Updating the HouseContactDetails table
                HouseContactDetail updatedHouseContactDetails = TipezeNyumbaServiceUnitOfWork.Repository<HouseContactDetail>().Get(u => u.houseID == houseDetailsFromClient.houseID);
                updatedHouseContactDetails.email = houseDetailsFromClient.email;
                updatedHouseContactDetails.phoneNumber1 = houseDetailsFromClient.phoneNumber1;
                updatedHouseContactDetails.phoneNumber2 = houseDetailsFromClient.phoneNumber2;
                updatedHouseContactDetails.whatsAppContactNumber = houseDetailsFromClient.whatsAppContactNumber;

                TipezeNyumbaServiceUnitOfWork.Repository<House>().Attach(houseToUpdateInDB);
                TipezeNyumbaServiceUnitOfWork.Repository<HouseContactDetail>().Attach(updatedHouseContactDetails);
                TipezeNyumbaServiceUnitOfWork.SaveChanges();
                return "Successful";
            }
            catch (Exception ex)
            {
                return "Failed to add new house, please confirm if the input parameters were inserted as required "+ex.Message;
            }
        }

#endregion
    }
}
