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
using TaxiGomelProject.ViewModels.CarMechanics;

namespace TaxiGomelProject.Controllers
{
    public class CarMechanicsController : Controller
    {
        private readonly TaxiGomelContext _context;
        CachedCarMechanicsService _carMechanicsService;
        List<CarMechanic> _carMechanics;

        public CarMechanicsController(TaxiGomelContext context)
        {
            _context = context;
            _carMechanicsService = (CachedCarMechanicsService)context.GetService<ICachedService<CarMechanic>>();
            _carMechanics = _carMechanicsService.GetData("carMechanics").ToList();
        }

        // GET: CarMechanics
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var count = _carMechanics.Count();
            var items = _carMechanics.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CarMechanicsViewModel viewModel = new CarMechanicsViewModel(items, pageViewModel);

            return View(viewModel);
        }

        // GET: CarMechanics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarMechanics == null)
            {
                return NotFound();
            }

            var carMechanic = await _context.CarMechanics
                .Include(c => c.Car)
                .Include(c => c.Mechanic)
                .FirstOrDefaultAsync(m => m.CarMechanicId == id);
            if (carMechanic == null)
            {
                return NotFound();
            }

            return View(carMechanic);
        }

        // GET: CarMechanics/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarcaseNumber");
            ViewData["MechanicId"] = new SelectList(_context.Employees.Where(e => e.PositionId == 2), "EmployeeId", "FirstName");
            return View();
        }

        // POST: CarMechanics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarMechanicId,CarId,MechanicId")] CarMechanic carMechanic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carMechanic);
                await _context.SaveChangesAsync();
                _carMechanicsService.AddData("carMechanics");
                _carMechanics = _carMechanicsService.GetData("carMechanics").ToList();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarcaseNumber", carMechanic.CarId);
            ViewData["MechanicId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", carMechanic.MechanicId);
            return View(carMechanic);
        }

        // GET: CarMechanics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarMechanics == null)
            {
                return NotFound();
            }

            var carMechanic = await _context.CarMechanics.FindAsync(id);
            if (carMechanic == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarcaseNumber", carMechanic.CarId);
            ViewData["MechanicId"] = new SelectList(_context.Employees.Where(e => e.PositionId == 2), "EmployeeId", "FirstName", carMechanic.MechanicId);
            return View(carMechanic);
        }

        // POST: CarMechanics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarMechanicId,CarId,MechanicId")] CarMechanic carMechanic)
        {
            if (id != carMechanic.CarMechanicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carMechanic);
                    await _context.SaveChangesAsync();
                    _carMechanicsService.AddData("carMechanics");
                    _carMechanics = _carMechanicsService.GetData("carMechanics").ToList();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarMechanicExists(carMechanic.CarMechanicId))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarcaseNumber", carMechanic.CarId);
            ViewData["MechanicId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", carMechanic.MechanicId);
            return View(carMechanic);
        }

        // GET: CarMechanics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarMechanics == null)
            {
                return NotFound();
            }

            var carMechanic = await _context.CarMechanics
                .Include(c => c.Car)
                .Include(c => c.Mechanic)
                .FirstOrDefaultAsync(m => m.CarMechanicId == id);
            if (carMechanic == null)
            {
                return NotFound();
            }

            return View(carMechanic);
        }

        // POST: CarMechanics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarMechanics == null)
            {
                return Problem("Entity set 'TaxiGomelContext.CarMechanics'  is null.");
            }
            var carMechanic = await _context.CarMechanics.FindAsync(id);
            if (carMechanic != null)
            {
                _context.CarMechanics.Remove(carMechanic);
            }
            
            await _context.SaveChangesAsync();
            _carMechanicsService.AddData("carMechanics");
            _carMechanics = _carMechanicsService.GetData("carMechanics").ToList();
            return RedirectToAction(nameof(Index));
        }

        private bool CarMechanicExists(int id)
        {
          return (_context.CarMechanics?.Any(e => e.CarMechanicId == id)).GetValueOrDefault();
        }
    }
}
