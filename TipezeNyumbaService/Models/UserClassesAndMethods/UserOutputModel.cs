using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipezeNyumbaService.Models.UserClassesAndMethods
{
    public class UserOutputModel
    {
        public int userID { get; set; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public string email { set; get; }
        public string phoneNumber { get; set; }
        public System.DateTime dateTimeCreated { set; get; }
        public string accountState { set; get; }
        public string userRoleForUser { get; set; }
        public string userSubscriptionType { get; set; }
    }
}
