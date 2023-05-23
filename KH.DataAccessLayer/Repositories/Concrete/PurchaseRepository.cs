using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Concrete
{
    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
    {
        private static ElnurhContext _context;
        DbSet<Purchase> Table;
        public PurchaseRepository(ElnurhContext context) : base(context)
        {
            _context = context;
            Table = _context.Set<Purchase>();
        }

        public new void Create(Purchase entity)
        {
            Table.Add(entity);
        }

        public new void Delete(Purchase entity)
        {
            Table.Remove(entity);
        }

        public new Purchase Get(int id)
        {
            return Table.FirstOrDefault(x => x.Id == id);
        }

        public new IQueryable<Purchase> GetAll() => Table.AsQueryable();

        public new void Update(Purchase entity)
        {
            Table.Update(entity);
        }
    }
}
