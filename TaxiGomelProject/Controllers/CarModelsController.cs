using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using TaxiGomelProject.Services;
using TaxiGomelProject.ViewModels.CarModels;

namespace TaxiGomelProject.Controllers
{
    public class CarModelsController : Controller
    {
        private readonly TaxiGomelContext _context;
        CachedCarModelsService _carModelsService;
        List<CarModel> _carModels;

        public CarModelsController(TaxiGomelContext context)
        {
            _context = context;
            _carModelsService = (CachedCarModelsService)context.GetService<ICachedService<CarModel>>();
            _carModels = _carModelsService.GetData("models").ToList();
        }

        // GET: CarModels
        public async Task<IActionResult> Index(decimal price, string carModelName, int page=1, SortState sortOrder = SortState.CarModelNameAsc)
        {
            int pageSize = 10;
            if (!string.IsNullOrEmpty(carModelName))
            {
                _carModels = _carModels.Where(c => c.ModelName == carModelName).ToList();
            }
            if (price != 0)
            {
                _carModels = _carModels.Where(c => c.Price == price).ToList();
            }
            switch (sortOrder)
            {
                case SortState.CarModelNameDesc:
                    _carModels = _carModels.OrderByDescending(s => s.ModelName).ToList();
                    break;
                case SortState.PriceAsc:
                    _carModels = _carModels.OrderBy(s => s.Price).ToList();
                    break;
                case SortState.PriceDesc:
                    _carModels = _carModels.OrderByDescending(s => s.Price).ToList();
                    break;
                case SortState.SpecificationAsc:
                    _carModels = _carModels.OrderBy(s => s.Specifications).ToList();
                    break;
                case SortState.SpecificationDesc:
                    _carModels = _carModels.OrderByDescending(s => s.Specifications).ToList();
                    break;
                default:
                    _carModels = _carModels.OrderBy(s => s.ModelName).ToList();
                    break;
            }
            var count = _carModels.Count();
            var items = _carModels.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            CarModelsFilterViewModel carModelsFilterViewModel = new CarModelsFilterViewModel(price, carModelName);
            CarModelsSortViewModel carModelsSortViewModel = new CarModelsSortViewModel(sortOrder);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CarModelsViewModel viewModel = new CarModelsViewModel(items, pageViewModel,carModelsFilterViewModel,carModelsSortViewModel);

            return View(viewModel);
        }

        // GET: CarModels/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarModels == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels
                .FirstOrDefaultAsync(m => m.CarModelId == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: CarModels/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarModelId,ModelName,TechStats,Price,Specifications")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carModel);

                await _context.SaveChangesAsync();
                _carModelsService.AddData("models");
                _carModels = _carModelsService.GetData("models").ToList();
                return RedirectToAction(nameof(Index));

            }
            return View(carModel);
        }

        // GET: CarModels/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarModels == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            return View(carModel);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarModelId,ModelName,TechStats,Price,Specifications")] CarModel carModel)
        {
            if (id != carModel.CarModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                    _carModelsService.AddData("models");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.CarModelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _carModelsService.AddData("models");
                _carModels = _carModelsService.GetData("models").ToList();

                return RedirectToAction(nameof(Index));
            }
            return View(carModel);
        }

        // GET: CarModels/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarModels == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModels
                .FirstOrDefaultAsync(m => m.CarModelId == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarModels == null)
            {
                return Problem("Entity set 'TaxiGomelContext.CarModels'  is null.");
            }
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel != null)
            {
                _context.CarModels.Remove(carModel);
            }
            
            await _context.SaveChangesAsync();
            _carModelsService.AddData("models");
            _carModels = _carModelsService.GetData("models").ToList();
            return RedirectToAction(nameof(Index));
        }

        private bool CarModelExists(int id)
        {
          return (_context.CarModels?.Any(e => e.CarModelId == id)).GetValueOrDefault();
        }
    }
}
