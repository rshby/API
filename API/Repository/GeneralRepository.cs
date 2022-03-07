using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;

        // Constructor
        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        // Detele Data
        public int Delete(Key key)
        {
            var hasilData = entities.Find(key);
            myContext.Remove(hasilData);
            var result = myContext.SaveChanges();
            return result;
        }

        // Get All Data
        public IEnumerable<Entity> GET()
        {
            return entities.ToList();
        }

        // Get Data By Id
        public Entity Get(Key key)
        {
            return entities.Find(key);
        }

        // Insert Data
        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        // Update Data By Id
        public int Update(Entity entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}