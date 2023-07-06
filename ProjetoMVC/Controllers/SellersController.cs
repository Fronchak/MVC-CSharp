using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Models;
using ProjetoMVC.Models.ViewModels;
using ProjetoMVC.Services;
using ProjetoMVC.Services.Exceptions;
using System.Diagnostics;

namespace ProjetoMVC.Controllers
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
            List<Seller> sellers = _sellerService.FindAll();
            return View(sellers);
        }

        public IActionResult Create()
        {
            SellerFormViewModel viewModel = new SellerFormViewModel();
            IEnumerable<Department> departments = _departmentService.FindAll();
            viewModel.Departments = departments;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if(!ModelState.IsValid)
            {
                IEnumerable<Department> departments = _departmentService.FindAll();
                SellerFormViewModel viewModel = new SellerFormViewModel()
                {
                    Departments = departments,
                    Seller = seller
                };
                return View(viewModel);
            }
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Seller not found"
                });
            }
            Seller seller = _sellerService.FindById(id.Value);
            if(seller == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Seller not found"
                });
            }
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Seller not found"
                });
            }
            Seller seller = _sellerService.FindById(id.Value);
            if(seller == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Seller not found"
                });
            }
            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Seller seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Seller not found"
                });
            }
            IEnumerable<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel()
            {
                Departments = departments,
                Seller = seller
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if(!ModelState.IsValid)
            {
                IEnumerable<Department> departments = _departmentService.FindAll();
                SellerFormViewModel viewModel = new SellerFormViewModel()
                {
                    Departments = departments,
                    Seller = seller
                };
                return View(viewModel);
            }
            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Ids do not match"
                });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = e.Message
                });
            }
            catch(DbConcurrecyException e)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = e.Message
                });
            }

        }

        public IActionResult Error(string message)
        {
            ErrorViewModel viewModel = new ErrorViewModel();
            viewModel.Message = message;
            viewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(viewModel);
        }
    }
}
