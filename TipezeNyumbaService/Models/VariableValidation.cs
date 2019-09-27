using TipezeNyumbaService.Interfaces;

namespace TipezeNyumbaService.Models
{
    public class VariableValidation :IVariableValidation
    {
        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
