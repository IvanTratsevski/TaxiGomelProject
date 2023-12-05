using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TaxiGomelProject.Data;
using TaxiGomelProject.Models;
using TaxiGomelProject.Services;
using TaxiGomelProject.ViewModels.Positions;

namespace TaxiGomelProject.Controllers
{
    public class PositionsController : Controller
    {
        private readonly TaxiGomelContext _context;
        CachedPositionsService _positionsService;
        List<Position> _positions;

        public PositionsController(TaxiGomelContext context)
        {
            _context = context;
            _positionsService = (CachedPositionsService)context.GetService<ICachedService<Position>>();
            _positions = _positionsService.GetData("positions").ToList();
        }

        // GET: Positions
        public async Task<IActionResult> Index(decimal salary, string positionName, int page = 1, SortState sortOrder = SortState.PositionNameAsc)
        {
            int pageSize = 10;
            if (!string.IsNullOrEmpty(positionName))
            {
                _positions = _positions.Where(c => c.PositionName == positionName).ToList();
            }
            if (salary != 0)
            {
                _positions = _positions.Where(c => c.Salary == salary).ToList();
            }
            switch (sortOrder)
            {
                case SortState.PositionNameDesc:
                    _positions = _positions.OrderByDescending(s => s.PositionName).ToList();
                    break;
                case SortState.SalaryAsc:
                    _positions = _positions.OrderBy(s => s.Salary).ToList();
                    break;
                case SortState.SalaryDesc:
                    _positions = _positions.OrderByDescending(s => s.Salary).ToList();
                    break;
                default:
                    _positions = _positions.OrderBy(s => s.PositionName).ToList();
                    break;
            }
            var count = _positions.Count();
            var items = _positions.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PositionsFilterViewModel positionsFilterViewModel = new PositionsFilterViewModel(salary, positionName);
            PositionsSortViewModel positionsSortViewModel = new PositionsSortViewModel(sortOrder);
            PositionsViewModel viewModel = new PositionsViewModel(items, pageViewModel,positionsFilterViewModel,positionsSortViewModel);

            return View(viewModel);
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .FirstOrDefaultAsync(m => m.PositionId == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PositionId,PositionName,Salary")] Position position)
        {
            if (ModelState.IsValid)
            {
                _context.Add(position);
                await _context.SaveChangesAsync();
                _positionsService.AddData("positions");
                _positions = _positionsService.GetData("positions").ToList();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PositionId,PositionName,Salary")] Position position)
        {
            if (id != position.PositionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                    _positionsService.AddData("positions");
                    _positions = _positionsService.GetData("positions").ToList();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.PositionId))
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
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .FirstOrDefaultAsync(m => m.PositionId == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Positions == null)
            {
                return Problem("Entity set 'TaxiGomelContext.Positions'  is null.");
            }
            var position = await _context.Positions.FindAsync(id);
            if (position != null)
            {
                _context.Positions.Remove(position);
            }
            
            await _context.SaveChangesAsync();
            _positionsService.AddData("positions");
            _positions = _positionsService.GetData("positions").ToList();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
          return (_context.Positions?.Any(e => e.PositionId == id)).GetValueOrDefault();
        }
    }
}
