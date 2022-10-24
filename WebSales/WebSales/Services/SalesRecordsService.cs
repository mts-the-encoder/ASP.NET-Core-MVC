using System;
using System.Collections.Generic;
using System.Data.Entity;
using WebSales.Data;
using WebSales.Models.Entities;
using System.Linq;


namespace WebSales.Services
{
    public class SalesRecordsService
    {
        private readonly WebSalesContext _context;

        public SalesRecordsService(WebSalesContext context)
        {
            _context = context;
        }

        public List<SalesRecord> FindByDate(DateTime? minDate, DateTime? maxDate)
        {
            var res = from obj in _context.SalesRecords select obj;

            if (minDate.HasValue)
            {
                res = res.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                res = res.Where(x => x.Date <= maxDate.Value);
            }

            return res
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToList();
        }

    }
}
