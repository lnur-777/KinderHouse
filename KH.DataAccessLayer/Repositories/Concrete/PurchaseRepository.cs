using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KH.DataAccessLayer.Repositories.Concrete
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ElnurhContext _context;
        public PurchaseRepository(ElnurhContext context)
        {
            _context = context;
        }
        public IQueryable<Purchase> GetPurchases() => _context.Purchases.AsQueryable();
    }
}
