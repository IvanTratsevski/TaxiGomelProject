using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
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
using TaxiGomelProject.ViewModels.Cars;

namespace TaxiGomelProject.Controllers
{
    public class CarsController : Controller
    {

        private readonly TaxiGomelContext _context;
        CachedCarsService _carsService;
        List<Car> _cars;
        public CarsController(TaxiGomelContext context)
        {
            _context = context;
            _carsService = (CachedCarsService)context.GetService<ICachedService<Car>>();
            _cars = _carsService.GetData("cars").ToList();
        }

        // GET: Cars
        public async Task<IActionResult> Index(int carModelId, DateTime releaseYear, int mileage, DateTime lastTI, string registartionNumber, SortState sortOrder = SortState.ReleaseYearAsc, int page = 1)
        {
            int pageSize = 10;
            if (carModelId != 0)
            {
                _cars = _cars.Where(c => c.CarModelId == carModelId).ToList();
            }
            if (releaseYear.Date != (new DateTime()).Date)
            {
                _cars = _cars.Where(c => c.ReleaseYear == releaseYear).ToList();
            }
            if (mileage != 0)
            {
                _cars = _cars.Where(c => c.Mileage == mileage).ToList();
            }
            if (lastTI.Date != (new DateTime()).Date)
            {
                _cars = _cars.Where(c => c.LastTi == lastTI).ToList();
            }
            if (!string.IsNullOrEmpty(registartionNumber))
            {
                _cars = _cars.Where(c => c.RegistrationNumber == registartionNumber).ToList();
            }
            
            switch (sortOrder)
            {
                case SortState.ReleaseYearDesc:
                    _cars = _cars.OrderByDescending(s => s.ReleaseYear).ToList();
                    break;
                case SortState.MilleageAsc:
                    _cars = _cars.OrderBy(s => s.Mileage).ToList();
                    break;
                case SortState.MilleageDesc:
                    _cars = _cars.OrderByDescending(s => s.Mileage).ToList();
                    break;
                case SortState.CarModelAsc:
                    _cars = _cars.OrderBy(s => s.CarModel.ModelName).ToList();
                    break;
                case SortState.CarModelDesc:
                    _cars = _cars.OrderByDescending(s => s.CarModel.ModelName).ToList();
                    break;
                case SortState.LastTIAsc:
                    _cars = _cars.OrderBy(s => s.LastTi).ToList();
                    break;
                case SortState.LastTIDesc:
                    _cars = _cars.OrderByDescending(s => s.LastTi).ToList();
                    break;
                default:
                    _cars = _cars.OrderBy(s => s.ReleaseYear).ToList();
                    break;
            }
            var count = _cars.Count();
            var items = _cars.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CarsSortViewModel carsSortViewModel = new CarsSortViewModel(sortOrder);
            CarsFilterViewModel carsFilterViewModel = new CarsFilterViewModel(_context.CarModels.ToList(),carModelId,releaseYear,mileage,lastTI,registartionNumber);
            CarsViewModel viewModel = new CarsViewModel(items, pageViewModel,carsFilterViewModel,carsSortViewModel);

            return View(viewModel);
        }

        // GET: Cars/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarModel)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["CarModelId"] = new SelectList(_context.CarModels, "CarModelId", "ModelName");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,RegistrationNumber,CarModelId,CarcaseNumber,EngineNumber,ReleaseYear,Mileage,LastTi,SpecialMarks")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                _carsService.AddData("cars");
                _cars = _carsService.GetData("cars").ToList();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarModelId"] = new SelectList(_context.CarModels, "CarModelId", "CarModelId", car.CarModelId);
            
            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarModelId"] = new SelectList(_context.CarModels, "CarModelId", "ModelName", car.CarModelId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,RegistrationNumber,CarModelId,CarcaseNumber,EngineNumber,ReleaseYear,Mileage,LastTi,SpecialMarks")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                    _carsService.AddData("cars");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _carsService.AddData("cars");
                _cars = _carsService.GetData("cars").ToList();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarModelId"] = new SelectList(_context.CarModels, "CarModelId", "CarModelId", car.CarModelId);
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.CarModel)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'TaxiGomelContext.Cars'  is null.");
            }
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            _carsService.AddData("cars");
            _cars = _carsService.GetData("cars").ToList();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
          return (_context.Cars?.Any(e => e.CarId == id)).GetValueOrDefault();
        }
    }
}
