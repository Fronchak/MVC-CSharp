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
        public async Task<IActionResult> Index()
        {
            List<Seller> sellers = await _sellerService.FindAllAsync();
            return View(sellers);
        }

        public async Task<IActionResult> Create()
        {
            SellerFormViewModel viewModel = new SellerFormViewModel();
            IEnumerable<Department> departments = await _departmentService.FindAllAsync();
            viewModel.Departments = departments;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            /*
            if(!ModelState.IsValid)
            {
                IEnumerable<Department> departments = await _departmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel()
                {
                    Departments = departments,
                    Seller = seller
                };
                return View(viewModel);
            }
            */
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Seller not found"
                });
            }
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = e.Message
                });
            }

        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Seller not found"
                });
            }
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if(seller == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Seller not found"
                });
            }
            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Seller seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new
                {
                    message = "Seller not found"
                });
            }
            IEnumerable<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel()
            {
                Departments = departments,
                Seller = seller
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if(!ModelState.IsValid)
            {
                IEnumerable<Department> departments = await _departmentService.FindAllAsync();
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
                await _sellerService.UpdateAsync(seller);
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
