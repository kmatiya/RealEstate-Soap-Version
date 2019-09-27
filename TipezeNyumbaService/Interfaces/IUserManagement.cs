using Generic_Repository_and_Unit_of_Work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipezeNyumbaService.ModelsWrapperObjects;

namespace TipezeNyumbaService.Interfaces
{
    public interface IUserManagement
    {
        AddOrUpdateUser AddNewUser(AddOrUpdateUser newUser);
        AddOrUpdateUser UpdateUserProfile(int userID);
        string RemoveUser(int userID);
        string DeactiveUserAccount(int userID);
        string ActivateUserAccount(int userID);
        List<User> GetSystemAdmins();
        List<User> GetAllHouseOwners();
        List<User> GetAllHouseViewer();
        List<UserPhoneNumber> GetUserPhoneNumbers(int userID);
    }
}
