﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebSales.Models.Entities;
using WebSales.Models.ViewModels;
using WebSales.Services;
using WebSales.Services.Exceptions;

namespace WebSales.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var deparments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller,Departments = deparments };
                return View(viewModel);
            }

            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), 
                    new { message = "Id not provided" });

            var obj = _sellerService.FindById(id.Value);
            
            if (obj == null)
                return RedirectToAction(nameof(Error),
                    new { message = "Id not found" });
            
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _sellerService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error),
                    new
                    { message = e.Message });
            }
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error),
                    new { message = "Id not provided" });

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error),
                    new { message = "Id not found" });

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error),
                    new { message = "Id not found provided" });

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error),
                    new { message = "Id not found" });

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var deparments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = deparments };
                return View(viewModel);
            }

            if (id != seller.Id)
                return RedirectToAction(nameof(Error),
                    new { message = "Id mismatch" });

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error),
                    new { message = e.Message });
            }
        }

        public IActionResult Error(string msg)
        {
            var viewModel = new ErrorViewModel
            {
                Message = msg,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
