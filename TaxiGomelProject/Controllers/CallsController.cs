using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
using TaxiGomelProject.ViewModels.Calls;

namespace TaxiGomelProject.Controllers
{
    public class CallsController : Controller
    {
        private readonly TaxiGomelContext _context;
        CachedCallsService _callsService;
        List<Call> _calls;
        public CallsController(TaxiGomelContext context)
        {
            _context = context;
            _callsService = (CachedCallsService)context.GetService<ICachedService<Call>>();
            _calls = _callsService.GetData("calls").ToList();
        }

        // GET: Calls
        public async Task<IActionResult> Index(int carId, int employeeId, int rateId, DateTime callTime, string telephone, string endPosition, string startPosition,int page = 1, SortState sortOrder = SortState.CallTimeAsc)
        {
            int pageSize = 10;
            if (carId != 0)
            {
                _calls = _calls.Where(c => c.CarId == carId).ToList();
            }
            if (employeeId != 0)
            {
                _calls = _calls.Where(c => c.DispatcherId == employeeId).ToList();
            }
            if (rateId != 0)
            {
                _calls = _calls.Where(c => c.RateId == rateId).ToList();
            }
            if (callTime.Date != (new DateTime()).Date)
            {
                _calls = _calls.Where(c => c.CallTime == callTime).ToList();
            }
            if (!string.IsNullOrEmpty(telephone))
            {
                _calls = _calls.Where(c => c.Telephone == telephone).ToList();
            }
            if (!string.IsNullOrEmpty(startPosition))
            {
                _calls = _calls.Where(c => c.StartPosition == startPosition).ToList();
            }
            if (!string.IsNullOrEmpty(endPosition))
            {
                _calls = _calls.Where(c => c.EndPosition == endPosition).ToList();
            }
            switch (sortOrder)
            {
                case SortState.CallTimeDesc:
                    _calls = _calls.OrderByDescending(s => s.CallTime).ToList();
                    break;
                case SortState.CarAsc:
                    _calls = _calls.OrderBy(s => s.CarId).ToList();
                    break;
                case SortState.CarDesc:
                    _calls = _calls.OrderByDescending(s => s.CarId).ToList();
                    break;
                case SortState.RateAsc:
                    _calls = _calls.OrderBy(s => s.Rate.RateDescription).ToList();
                    break;
                case SortState.RateDesc:
                    _calls = _calls.OrderByDescending(s => s.Rate.RateDescription).ToList();
                    break;
                case SortState.DispatcherAsc:
                    _calls = _calls.OrderBy(s => s.Dispatcher.FirstName).ToList();
                    break;
                case SortState.DispatcherDesc:
                    _calls = _calls.OrderByDescending(s => s.Dispatcher.FirstName).ToList();
                    break;
                default:
                    _calls = _calls.OrderBy(s => s.CallTime).ToList();
                    break;
            }
            var count = _calls.Count();
            var items = _calls.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CallsFilterViewModel callsFilterViewModel = new CallsFilterViewModel(_context.Cars.ToList(), _context.Employees.Where(e => e.PositionId == 3).ToList(), _context.Rates.ToList(), carId, employeeId, rateId, callTime, telephone, startPosition, endPosition);
            CallsSortViewModel callsSortViewModel = new CallsSortViewModel(sortOrder);
            CallsViewModel viewModel = new CallsViewModel(items, pageViewModel, callsFilterViewModel, callsSortViewModel);

            return View(viewModel);
        }

        // GET: Calls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Calls == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .Include(c => c.Car)
                .Include(c => c.Dispatcher)
                .Include(c => c.Rate)
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // GET: Calls/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "RegistartionNumber");
            ViewData["DispatcherId"] = new SelectList(_context.Employees.Where(e => e.PositionId == 3), "EmployeeId", "FirstName");
            ViewData["RateId"] = new SelectList(_context.Rates, "RateId", "RateDescription");
            return View();
        }

        // POST: Calls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CallId,CallTime,Telephone,StartPosition,EndPosition,RateId,CarId,DispatcherId")] Call call)
        {
            if (ModelState.IsValid)
            {
                _context.Add(call);
                await _context.SaveChangesAsync();
                _callsService.AddData("calls");
                _calls = _callsService.GetData("calls").ToList();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "RegistartionNumber", call.CarId);
            ViewData["DispatcherId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", call.DispatcherId);
            ViewData["RateId"] = new SelectList(_context.Rates, "RateId", "RateDescription", call.RateId);
            return View(call);
        }

        // GET: Calls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Calls == null)
            {
                return NotFound();
            }

            var call = await _context.Calls.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "RegistartionNumber", call.CarId);
            ViewData["DispatcherId"] = new SelectList(_context.Employees.Where(e => e.PositionId == 3), "EmployeeId", "FirstName", call.DispatcherId);
            ViewData["RateId"] = new SelectList(_context.Rates, "RateId", "RateDescription", call.RateId);
            return View(call);
        }

        // POST: Calls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CallId,CallTime,Telephone,StartPosition,EndPosition,RateId,CarId,DispatcherId")] Call call)
        {
            if (id != call.CallId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(call);
                    await _context.SaveChangesAsync();
                    _callsService.AddData("calls");
                    _calls = _callsService.GetData("calls").ToList();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallExists(call.CallId))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "RegistartionNumber", call.CarId);
            ViewData["DispatcherId"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", call.DispatcherId);
            ViewData["RateId"] = new SelectList(_context.Rates, "RateId", "RateDescription", call.RateId);
            return View(call);
        }

        // GET: Calls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Calls == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .Include(c => c.Car)
                .Include(c => c.Dispatcher)
                .Include(c => c.Rate)
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // POST: Calls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Calls == null)
            {
                return Problem("Entity set 'TaxiGomelContext.Calls'  is null.");
            }
            var call = await _context.Calls.FindAsync(id);
            if (call != null)
            {
                _context.Calls.Remove(call);
            }

            await _context.SaveChangesAsync();
            _callsService.AddData("calls");
            _calls = _callsService.GetData("calls").ToList();
            return RedirectToAction(nameof(Index));
        }

        private bool CallExists(int id)
        {
            return (_context.Calls?.Any(e => e.CallId == id)).GetValueOrDefault();
        }
    }
}
