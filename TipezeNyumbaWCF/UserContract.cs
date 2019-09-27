using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TipezeNyumbaWCF
{
    [DataContract]
    public class UserContract
    {
        [DataMember]
        public int userID { get; set; }
        [DataMember]
        public string username { get; set; }

    }
}
