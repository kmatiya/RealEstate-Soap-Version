using Generic_Repository_and_Unit_of_Work.Models;
using Generic_Repository_and_Unit_of_Work.Unit_of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TipezeNyumbaWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TipezeNyumbaService" in both code and config file together.
    public class TipezeNyumbaService : ITipezeNyumbaService
    {
        public string GetMessage()
        {
            return "Hello Mr Matiya";
        }

    }
}
