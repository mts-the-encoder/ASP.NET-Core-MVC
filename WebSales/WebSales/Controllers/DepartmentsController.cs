using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSales.Models.Entities;

namespace WebSales.Controllers
{
    public class DepartmentsController : Controller
    {
        public ActionResult Index()
        {
            List<Department> list = new List<Department>();
            list.Add(new Department { Id = 1, Name = "Electronics"});
            list.Add(new Department { Id = 2, Name = "Fashion"});
            return View(list);
        }
    }
}
