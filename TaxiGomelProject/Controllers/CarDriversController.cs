using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using TaxiGomelProject.Services;
using TaxiGomelProject.ViewModels.CarDrivers;

namespace TaxiGomelProject.Controllers
{
    public class CarDriversController : Controller
    {
        private readonly TaxiGomelContext _context;
        CachedCarDriversService _carDriversService;
        List<CarDriver> _carDrivers;

        public CarDriversController(TaxiGomelContext context)
        {
            _context = context;
            _carDriversService = (CachedCarDriversService)context.GetService<ICachedService<CarDriver>>();
            _carDrivers = _carDriversService.GetData("carDrivers").ToList();
        }

        // GET: CarDrivers
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var count = _carDrivers.Count();
            var items = _carDrivers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CarDriversViewModel viewModel = new CarDriversViewModel(items, pageViewModel);

            return View(viewModel);
        }

        // GET: CarDrivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarDrivers == null)
            {
                return NotFound();
            }

            var carDriver = await _context.CarDrivers
                .Include(c => c.Car)
                .Include(c => c.Driver)
                .FirstOrDefaultAsync(m => m.CarDriverId == id);
            if (carDriver == null)
            {
                return NotFound();
            }

            return View(carDriver);
        }

        // GET: CarDrivers/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarcaseNumber");
            ViewData["DriverId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            return View();
        }

        // POST: CarDrivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarDriverId,CarId,DriverId")] CarDriver carDriver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carDriver);
                await _context.SaveChangesAsync();
                _carDriversService.AddData("carDrivers");
                _carDrivers = _carDriversService.GetData("carDrivers").ToList();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarcaseNumber", carDriver.CarId);
            ViewData["DriverId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", carDriver.DriverId);
            return View(carDriver);
        }

        // GET: CarDrivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarDrivers == null)
            {
                return NotFound();
            }

            var carDriver = await _context.CarDrivers.FindAsync(id);
            if (carDriver == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarcaseNumber", carDriver.CarId);
            ViewData["DriverId"] = new SelectList(_context.Employees.Where(e => e.PositionId == 1), "EmployeeId", "FirstName", carDriver.DriverId);
            return View(carDriver);
        }

        // POST: CarDrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarDriverId,CarId,DriverId")] CarDriver carDriver)
        {
            if (id != carDriver.CarDriverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carDriver);
                    await _context.SaveChangesAsync();
                    _carDriversService.AddData("carDrivers");
                    _carDrivers = _carDriversService.GetData("carDrivers").ToList();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarDriverExists(carDriver.CarDriverId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarcaseNumber", carDriver.CarId);
            ViewData["DriverId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", carDriver.DriverId);
            return View(carDriver);
        }

        // GET: CarDrivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarDrivers == null)
            {
                return NotFound();
            }

            var carDriver = await _context.CarDrivers
                .Include(c => c.Car)
                .Include(c => c.Driver)
                .FirstOrDefaultAsync(m => m.CarDriverId == id);
            if (carDriver == null)
            {
                return NotFound();
            }

            return View(carDriver);
        }

        // POST: CarDrivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarDrivers == null)
            {
                return Problem("Entity set 'TaxiGomelContext.CarDrivers'  is null.");
            }
            var carDriver = await _context.CarDrivers.FindAsync(id);
            if (carDriver != null)
            {
                _context.CarDrivers.Remove(carDriver);
            }
            
            await _context.SaveChangesAsync();
            _carDriversService.AddData("carDrivers");
            _carDrivers = _carDriversService.GetData("carDrivers").ToList();
            return RedirectToAction(nameof(Index));
        }

        private bool CarDriverExists(int id)
        {
          return (_context.CarDrivers?.Any(e => e.CarDriverId == id)).GetValueOrDefault();
        }
    }
}
