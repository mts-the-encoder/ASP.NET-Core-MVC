using System.Collections.Generic;
using System.Linq;
using WebSales.Data;
using WebSales.Models.Entities;

namespace WebSales.Services
{
    public class DepartmentService
    {
        private readonly WebSalesContext _context;

        public DepartmentService(WebSalesContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
