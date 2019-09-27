using Generic_Repository_and_Unit_of_Work.Unit_of_Work;

namespace TipezeNyumbaService.Models
{
    public class AccountStateManagement: UnitOfWorkInstance
    {
        public FieldState GetAcccountStateByString(string stateInWords)
        {
            FieldState getFieldState = TipezeNyumbaUnitOfWork.Repository<FieldState>().Get(u => u.state.ToLower() == stateInWords.ToLower());
            return getFieldState;
        }
        public FieldState GetAcccountStateById(int stateID)
        {
            FieldState getFieldState = TipezeNyumbaUnitOfWork.Repository<FieldState>().Get(u => u.fieldStateID == stateID);
            return getFieldState;
        }

        public FieldState GetActivatedState()
        {
            FieldState getFieldState = TipezeNyumbaUnitOfWork.Repository<FieldState>().Get(u => u.state == "Activated");
            return getFieldState;
        }

        public FieldState GetDeactivatedState()
        {
            FieldState getFieldState = TipezeNyumbaUnitOfWork.Repository<FieldState>().Get(u => u.state == "Deactivated");
            return getFieldState;
        }
    }
}