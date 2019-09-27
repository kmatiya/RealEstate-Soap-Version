using TipezeNyumbaService.Models;

namespace TipezeNyumbaService.Interfaces.UsersInterfaces
{
    public interface ITokenManagement
    {
        string GenerateToken(User userToAssignToken);
        bool ValidateToken(string tokenFromClient);
    }
}
