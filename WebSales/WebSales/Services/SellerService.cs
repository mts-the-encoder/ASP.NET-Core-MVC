using System.Collections.Generic;
using System.Linq;
using WebSales.Data;
using WebSales.Models.Entities;

namespace WebSales.Services
{
    public class SellerService
    {
        private readonly WebSalesContext _context;

        public SellerService(WebSalesContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
