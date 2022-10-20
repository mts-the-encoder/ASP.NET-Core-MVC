using System.Collections.Generic;
using WebSales.Models.Entities;

namespace WebSales.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }

    }
}
