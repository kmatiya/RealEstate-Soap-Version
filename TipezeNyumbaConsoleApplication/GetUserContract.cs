using Generic_Repository_and_Unit_of_Work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TipezeNyumbaService.Models;

namespace TipezeNyumbaService
{
    public class GetUserContract
    {

        public List<User> UserObject { get; set; }
    }
}
