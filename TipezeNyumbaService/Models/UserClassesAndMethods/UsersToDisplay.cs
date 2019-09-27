using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipezeNyumbaService.Models.UserClassesAndMethods
{
    public class UsersToDisplay
    {
        public List<UserOutputModel> FormatUsersToDisplay(List<User> allUsers)
        {
            List<UserOutputModel> usersToDisplay = new List<UserOutputModel>();
            foreach (var eachUser in allUsers)
            {
                usersToDisplay.Add(new UserOutputModel()
                {
                    userID = eachUser.userID,
                    firstName = eachUser.firstName,
                    lastName = eachUser.lastName,
                    email = eachUser.email,
                    phoneNumber = eachUser.phoneNumber,
                    dateTimeCreated = eachUser.dateTimeCreated,
                    userRoleForUser = eachUser.UserRole.role,
                    userSubscriptionType = eachUser.SubscriptionType.type,
                    accountState = eachUser.FieldState.state
                });
            }
            return usersToDisplay;
        }
    }
}
