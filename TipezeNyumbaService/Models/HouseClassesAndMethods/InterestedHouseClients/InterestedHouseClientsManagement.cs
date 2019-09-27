using Generic_Repository_and_Unit_of_Work.Unit_of_Work;
using System;
using TipezeNyumbaService.Interfaces.HouseInterfaces.HouseInterestedClientsInterfaces;
using System.Collections.Generic;
using System.Linq;
using TipezeNyumbaService.Models.UserClassesAndMethods;

namespace TipezeNyumbaService.Models.HouseClassesAndMethods.InterestedHouseClients
{
    public class InterestedHouseClientsManagement :UnitOfWorkInstance,IHouseInterestedClients, IHouseInterestedClientsRepository
    {
        readonly AccountStateManagement _fieldStateManagement = new AccountStateManagement();
        public VariableValidation variableValidation = new VariableValidation();
        private readonly UsersToDisplay _usersToDisplay = new UsersToDisplay(); 
        public bool AddInterestedClient(int houseId, int userId, string clientState)
        {
            int activatedState = _fieldStateManagement.GetActivatedState().fieldStateID;
            InterestedClient checkIfClientAlreadyExist = TipezeNyumbaUnitOfWork.Repository<InterestedClient>()
                .Get(u => u.houseID == houseId && u.userID == userId);
            if (checkIfClientAlreadyExist != null)
            {
                if (checkIfClientAlreadyExist.status == activatedState)
                {
                    return true;
                }
                checkIfClientAlreadyExist.status = activatedState;
                TipezeNyumbaUnitOfWork.Repository<InterestedClient>().Attach(checkIfClientAlreadyExist);
                TipezeNyumbaUnitOfWork.SaveChanges();
                return true;
            }
            ClientState checkClientState = GetClientStateByName(clientState);
            if (checkClientState == null)
            {
                return false;
            }
            InterestedClient newInterestedClient = new InterestedClient()
            {
                userID = userId,
                houseID = houseId,
                interestedClientState = checkClientState.stateID,
                status = _fieldStateManagement.GetActivatedState().fieldStateID,
                dateCreated = DateTime.Now
            };
            TipezeNyumbaUnitOfWork.Repository<InterestedClient>().Add(newInterestedClient);
            TipezeNyumbaUnitOfWork.SaveChanges();
            return true;
        }
        public List<UserOutputModel> GetInterestedHouseClients(int houseId)
        {
            List<InterestedClient> getInterestedClients = TipezeNyumbaUnitOfWork.Repository<InterestedClient>()
                .GetAll(u => u.houseID == houseId).ToList();
            if (getInterestedClients.Count == 0)
            {
                return null;
            }
            List<User> interestedClients = new List<User>();
            foreach (InterestedClient eachClient in getInterestedClients)
            {
                User tempUser = eachClient.User;
                interestedClients.Add(tempUser);
            }
            return _usersToDisplay.FormatUsersToDisplay(interestedClients);
        }
        private ClientState GetClientStateByName(string clientState)
        {
            ClientState getClientState = TipezeNyumbaUnitOfWork.Repository<ClientState>()
                .Get(u => u.clientStatus.ToLower() == clientState.ToLower());
            return getClientState;
        }
    }
}
