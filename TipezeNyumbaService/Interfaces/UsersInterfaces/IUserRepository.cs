using System.Collections.Generic;
using TipezeNyumbaService.Models.UserClassesAndMethods;

namespace TipezeNyumbaService.Interfaces.UsersInterfaces
{
    // Manages querying users in the system based on different conditions
    public interface IUserRepository
    {
        // Get a single user base on userId
        UserInputModel GetUser(int userId);
        // Get all users in the system
        List<UserOutputModel> GetAllUsers();
        // Get all users in the system based on the user's role
        List<UserOutputModel> GetUsers(string systemRole);
        // Get all activated users
        List<UserOutputModel> GetActivatedUsers();
        // Get all deactivated users
        List<UserOutputModel> GetDeactivatedUsers();
        // Get all blocked Users
       List<UserOutputModel> GetBlockedUsers();
    }
}