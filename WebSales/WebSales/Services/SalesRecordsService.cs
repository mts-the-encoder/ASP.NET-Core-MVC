using System;
using System.Collections.Generic;
using System.Data.Entity;
using WebSales.Data;
using WebSales.Models.Entities;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;


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

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate,DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecords select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}

