using System;
using System.Collections.Generic;
using System.Linq;
using Generic_Repository_and_Unit_of_Work.Models;
using TipezeNyumbaService.Interfaces;
using Generic_Repository_and_Unit_of_Work.Unit_of_Work;
using TipezeNyumbaService.Interfaces.UsersInterfaces;

namespace TipezeNyumbaService.Models.UserClassesAndMethods
{
    public class UserManagement : UnitOfWorkInstance, IUserProfile, IUserRepository, IStates
    {
        readonly AccountStateManagement _fieldStateManagement = new AccountStateManagement();
        public VariableValidation variableValidation = new VariableValidation();
        readonly UsersToDisplay _usersToDisplay = new UsersToDisplay();
        public void AddUser(UserInputModel newUser)
        {
            SubscriptionType checkUserSubscription = GetSubscriptionTypeByString(newUser.userSubscriptionType);
            FieldState checkFieldState = _fieldStateManagement.GetActivatedState();
            UserRole checkUserRole = GetUserRoleByString(newUser.userRoleForUser);

            HashPassword hashPasswordObject = new HashPassword();
            User userDetails = new User()
            {
                firstName = newUser.firstName,
                lastName = newUser.lastName,
                email = newUser.email,
                dateTimeCreated = DateTime.Now,
                phoneNumber = newUser.phoneNumber,
                accountState = checkFieldState.fieldStateID,
                userSubscriptionType = checkUserSubscription.subscriptionID,
                userType = checkUserRole.userRoleID,
                password = hashPasswordObject.CreateHashedPassword(newUser.password),
                passwordSalt = hashPasswordObject.Salt
            };

            TipezeNyumbaUnitOfWork.Repository<User>().Add(userDetails);
            TipezeNyumbaUnitOfWork.SaveChanges();
        }
        public List<UserOutputModel> GetAllUsers()
        {
            List<User> allUsers = TipezeNyumbaUnitOfWork.Repository<User>().GetAll().ToList();
            if (allUsers.Count == 0)
            {
                return null;
            }
            return _usersToDisplay.FormatUsersToDisplay(allUsers);
        }

        public UserInputModel GetUser(int userId)
        {
            User userToGet = TipezeNyumbaUnitOfWork.Repository<User>().Get(u => u.userID == userId);
            if (userToGet == null)
            {
                return null;
            }
            UserInputModel userModel = new UserInputModel()
            {
                userID = userToGet.userID,
                firstName = userToGet.firstName,
                lastName = userToGet.lastName,
                email = userToGet.email,
                phoneNumber = userToGet.phoneNumber,
                dateTimeCreated = userToGet.dateTimeCreated,
                userRoleForUser = userToGet.UserRole.role,
                userSubscriptionType = userToGet.SubscriptionType.type,
                accountState = userToGet.FieldState.state
            };
            return userModel;
        }

        public List<UserOutputModel> GetUsers(string systemRole)
        {
            List<User> allUsers = TipezeNyumbaUnitOfWork.Repository<User>().GetAll(u => u.UserRole.role == systemRole)
                .ToList();
            if (allUsers.Count == 0)
            {
                return null;
            }
            return _usersToDisplay.FormatUsersToDisplay(allUsers);
        }

        public bool RemoveUser(int id)
        {
            User userToRemove = TipezeNyumbaUnitOfWork.Repository<User>().Get(u => u.userID == id);
            if (userToRemove == null) return false;
            TipezeNyumbaUnitOfWork.Repository<User>().Delete(userToRemove);
            TipezeNyumbaUnitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateUserDetails(int id, UserInputModel userDetails)
        {
            User userToUpdate = TipezeNyumbaUnitOfWork.Repository<User>().Get(u => u.userID == id);
            if (userToUpdate == null) return false;
            userToUpdate.firstName = userDetails.firstName;
            userToUpdate.lastName = userDetails.lastName;
            userToUpdate.email = userDetails.email;
            userToUpdate.userType = GetUserRoleByString(userDetails.userRoleForUser).userRoleID;
            userToUpdate.userSubscriptionType = GetSubscriptionTypeByString(userDetails.userSubscriptionType).subscriptionID;
            userToUpdate.phoneNumber = userDetails.phoneNumber;
            TipezeNyumbaUnitOfWork.Repository<User>().Attach(userToUpdate);
            TipezeNyumbaUnitOfWork.SaveChanges();
            return true;
        }
        public List<UserOutputModel> GetBlockedUsers()
        {
            string blockedStateToLower = "Blocked";
            List<User> allUsers = TipezeNyumbaUnitOfWork.Repository<User>().GetAll(u => u.FieldState.state.ToLower() == blockedStateToLower.ToLower())
                .ToList();
            return _usersToDisplay.FormatUsersToDisplay(allUsers);
        }
        public bool ActivateObject(int id)
        {
            User userAccountToActivate = TipezeNyumbaUnitOfWork.Repository<User>().Get(u => u.userID == id);
            if (userAccountToActivate == null) return false;
            userAccountToActivate.FieldState.state = "Activated";
            TipezeNyumbaUnitOfWork.Repository<User>().Attach(userAccountToActivate);
            TipezeNyumbaUnitOfWork.SaveChanges();
            return true;
        }
        public bool DeactivateObject(int id)
        {
            User userAccountToActivate = TipezeNyumbaUnitOfWork.Repository<User>().Get(u => u.userID == id);
            if (userAccountToActivate == null) return false;
            userAccountToActivate.FieldState.state = "Deactivated";
            TipezeNyumbaUnitOfWork.Repository<User>().Attach(userAccountToActivate);
            TipezeNyumbaUnitOfWork.SaveChanges();
            return true;
        }

        public List<UserOutputModel> GetActivatedUsers()
        {
            List<User> activatedUsers = TipezeNyumbaUnitOfWork.Repository<User>()
                .GetAll(u => u.FieldState.state == "Activated").ToList();
            if (activatedUsers.Count == 0)
            {
                return null;
            }
            return _usersToDisplay.FormatUsersToDisplay(activatedUsers);
        }

        public List<UserOutputModel> GetDeactivatedUsers()
        {
            List<User> deactivatedUsers = TipezeNyumbaUnitOfWork.Repository<User>()
                .GetAll(u => u.FieldState.state == "Deactivated").ToList();
            if (deactivatedUsers.Count == 0)
            {
                return null;
            }
            return _usersToDisplay.FormatUsersToDisplay(deactivatedUsers);
        }

        private SubscriptionType GetSubscriptionTypeById(int id)
        {
            SubscriptionType getSubscriptionType = TipezeNyumbaUnitOfWork.Repository<SubscriptionType>()
                .Get(u => u.subscriptionID == id);
            return getSubscriptionType;
        }

        private SubscriptionType GetSubscriptionTypeByString(string subscriptionType)
        {
            SubscriptionType getSubscriptionType = TipezeNyumbaUnitOfWork.Repository<SubscriptionType>()
                .Get(u => u.type == subscriptionType);
            return getSubscriptionType;
        }

        private UserRole GetUserRoleByString(string userRoleInWords)
        {
            UserRole getUserRole = TipezeNyumbaUnitOfWork.Repository<UserRole>().Get(u => u.role == userRoleInWords);
            return getUserRole;
        }

        private UserRole GetUserRoleById(int id)
        {
            UserRole getUserRole = TipezeNyumbaUnitOfWork.Repository<UserRole>().Get(u => u.userRoleID == id);
            return getUserRole;
        }
    }
}
