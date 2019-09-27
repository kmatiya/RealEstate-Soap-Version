using Generic_Repository_and_Unit_of_Work.Unit_of_Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipezeNyumbaService.Models;

namespace Generic_Repository_and_Unit_of_Work.Unit_of_Work
{
    public class UnitOfWorkInstance
    {
        internal readonly GenericUoW TipezeNyumbaUnitOfWork = new GenericUoW(new FindAHouseEntities());
    }

}
