using Generic_Repository_and_Unit_of_Work.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_Repository_and_Unit_of_Work.Unit_of_Work
{
    public class GenericUoW : IGenericUoW
    {
        private readonly DbContext entities = null;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public GenericUoW(DbContext entities)
        {
            this.entities = entities;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }

            IRepository<T> repo = new GenericRepository<T>(entities);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public void SaveChanges()
        {
            entities.SaveChanges();
        }
    }
}
