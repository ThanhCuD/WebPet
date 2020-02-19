using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPet.Models.SQLRepository
{
    public class SQLCustommerRepository : ICustommerRepository
    {
        private readonly AppDbContext context;

        public SQLCustommerRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Custommer Add(Custommer obj)
        {
            context.Add(obj);
            context.SaveChanges();
            return obj;
        }

        public Custommer Delete(int id)
        {
            var cu = context.Custommers.Find(id);
            if (cu != null)
            {
                context.Custommers.Remove(cu);
                context.SaveChanges();
            }
            return cu;
        }

        public IEnumerable<Custommer> GetAll()
        {
            return context.Custommers;
        }

        public Custommer GetById(int Id)
        {
            return context.Custommers.Find(Id);
        }

        public Custommer Update(Custommer obj)
        {
            var cu = context.Custommers.Attach(obj);
            cu.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return obj;
        }
    }
}
