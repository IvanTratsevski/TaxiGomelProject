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
using TaxiGomelProject.ViewModels.Rates;

namespace TaxiGomelProject.Controllers
{
    public class RatesController : Controller
    {
        private readonly TaxiGomelContext _context;
        CachedRatesService _ratesService;
        List<Rate> _rates;

        public RatesController(TaxiGomelContext context)
        {
            _context = context;
            _ratesService = (CachedRatesService)context.GetService<ICachedService<Rate>>();
            _rates = _ratesService.GetData("rates").ToList();
        }

        // GET: Rates
        public async Task<IActionResult> Index(decimal price, string rateName, int page = 1, SortState sortOrder = SortState.RateNameAsc)
        {
            int pageSize = 10;
            if (!string.IsNullOrEmpty(rateName))
            {
                _rates = _rates.Where(c => c.RateDescription == rateName).ToList();
            }
            if (price != 0)
            {
                _rates = _rates.Where(c => c.RatePrice == price).ToList();
            }
            switch (sortOrder)
            {
                case SortState.RateNameDesc:
                    _rates = _rates.OrderByDescending(s => s.RateDescription).ToList();
                    break;
                case SortState.PriceAsc:
                    _rates = _rates.OrderBy(s => s.RatePrice).ToList();
                    break;
                case SortState.PriceDesc:
                    _rates = _rates.OrderByDescending(s => s.RatePrice).ToList();
                    break;
                default:
                    _rates = _rates.OrderBy(s => s.RateDescription).ToList();
                    break;
            }
            var count = _rates.Count();
            var items = _rates.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            RatesFilterViewModel ratesFilterViewModel = new RatesFilterViewModel(price, rateName);
            RatesSortViewModel ratesSortViewModel = new RatesSortViewModel(sortOrder);
            RatesViewModel viewModel = new RatesViewModel(items, pageViewModel,ratesFilterViewModel,ratesSortViewModel);

            return View(viewModel);
        }

        // GET: Rates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rates == null)
            {
                return NotFound();
            }

            var rate = await _context.Rates
                .FirstOrDefaultAsync(m => m.RateId == id);
            if (rate == null)
            {
                return NotFound();
            }

            return View(rate);
        }

        // GET: Rates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RateId,RateDescription,RatePrice")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rate);
                await _context.SaveChangesAsync();
                _ratesService.AddData("rates");
                _rates = _ratesService.GetData("rates").ToList();
                return RedirectToAction(nameof(Index));
            }
            return View(rate);
        }

        // GET: Rates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rates == null)
            {
                return NotFound();
            }

            var rate = await _context.Rates.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }
            return View(rate);
        }

        // POST: Rates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RateId,RateDescription,RatePrice")] Rate rate)
        {
            if (id != rate.RateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rate);
                    await _context.SaveChangesAsync();
                    _ratesService.AddData("rates");
                    _rates = _ratesService.GetData("rates").ToList();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RateExists(rate.RateId))
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
            return View(rate);
        }

        // GET: Rates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rates == null)
            {
                return NotFound();
            }

            var rate = await _context.Rates
                .FirstOrDefaultAsync(m => m.RateId == id);
            if (rate == null)
            {
                return NotFound();
            }

            return View(rate);
        }

        // POST: Rates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rates == null)
            {
                return Problem("Entity set 'TaxiGomelContext.Rates'  is null.");
            }
            var rate = await _context.Rates.FindAsync(id);
            if (rate != null)
            {
                _context.Rates.Remove(rate);
            }
            
            await _context.SaveChangesAsync();
            _ratesService.AddData("rates");
            _rates = _ratesService.GetData("rates").ToList();
            return RedirectToAction(nameof(Index));
        }

        private bool RateExists(int id)
        {
          return (_context.Rates?.Any(e => e.RateId == id)).GetValueOrDefault();
        }
    }
}
