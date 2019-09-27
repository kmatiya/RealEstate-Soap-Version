using Generic_Repository_and_Unit_of_Work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TipezeNyumbaWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITipezeNyumbaService" in both code and config file together.
    [ServiceContract]
    public interface ITipezeNyumbaService
    {
        [OperationContract]
        string GetMessage();
    }
}
