using Generic_Repository_and_Unit_of_Work.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_Repository_and_Unit_of_Work.Unit_of_Work
{
    public interface IGenericUoW
    {
        IRepository<T> Repository<T>() where T : class;

        void SaveChanges();
    }
}
