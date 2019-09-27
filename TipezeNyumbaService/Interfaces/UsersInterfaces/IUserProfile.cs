using TipezeNyumbaService.Models.UserClassesAndMethods;

namespace TipezeNyumbaService.Interfaces.UsersInterfaces
{
    // Manages user profiles
    public interface IUserProfile
    {
        // Adds new user in the interface
        void AddUser(UserInputModel newUser);
        // Remove user in the system based on user Id
        bool RemoveUser(int id);
        // Updates user profile in the system
        bool UpdateUserDetails(int id,UserInputModel userDetails);

        // Gets User role of user by string
        //UserRole GetUserRoleByString(string userRoleInWords);
        //// Gets User role of user by ID
        //UserRole GetUserRoleById(int id);

    }
}