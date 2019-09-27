using System.Text.RegularExpressions;
using Generic_Repository_and_Unit_of_Work.Models;
using Generic_Repository_and_Unit_of_Work.Unit_of_Work;

namespace TipezeNyumbaService.Models.UserClassesAndMethods.Aunthentication
{
    public class LoginManagement : UnitOfWorkInstance
    {
        private readonly HashPassword _hashPassword = new HashPassword();
        public bool VerifyCredentials(string username, string password)
        {
            User getUser = TipezeNyumbaUnitOfWork.Repository<User>()
                .Get(u => u.phoneNumber == username || u.email == username);
            if (getUser == null)
            {
                return false;
            }
            bool verifyIfPasswordIsCorrect = _hashPassword.IsPasswordCorrect(getUser, password);
            return verifyIfPasswordIsCorrect;
        }

        public bool VerifyAdminCredentials(string username, string password)
        {
            User getUser = TipezeNyumbaUnitOfWork.Repository<User>()
                .Get(u => u.phoneNumber == username || u.email == username && u.UserRole.role.ToLower() == "admin".ToLower());
            if (getUser == null)
            {
                return false;
            }
            bool verifyIfPasswordIsCorrect = _hashPassword.IsPasswordCorrect(getUser, password);
            return verifyIfPasswordIsCorrect;
        }

        public bool IsBase64Encoded(string encodedCode)
        {
            bool checkIfEncoded = Regex.IsMatch(encodedCode, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
            if (checkIfEncoded)
            {
                return true;
            }
            return false;
        }
    }

}
