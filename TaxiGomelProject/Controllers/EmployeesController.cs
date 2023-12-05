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
using TaxiGomelProject.ViewModels.Employees;

namespace TaxiGomelProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly TaxiGomelContext _context;
        CachedEmployeesService _employeesService;
        List<Employee> _employees;

        public EmployeesController(TaxiGomelContext context)
        {
            _context = context;
            _employeesService = (CachedEmployeesService)context.GetService<ICachedService<Employee>>();
            _employees = _employeesService.GetData("employees").ToList();
        }

        // GET: Employees
        public async Task<IActionResult> Index(int positionId, int age, int experience, string firstName, string lastName, int page = 1, SortState sortOrder = SortState.FirstNameAsc)
        {
            int pageSize = 10;
            if (positionId != 0)
            {
                _employees = _employees.Where(c => c.PositionId == positionId).ToList();
            }
            if (age != 0)
            {
                _employees = _employees.Where(c => c.Age == age).ToList();
            }
            if (experience != 0)
            {
                _employees = _employees.Where(c => c.Experience == experience).ToList();
            }
            if (!string.IsNullOrEmpty(firstName))
            {
                _employees = _employees.Where(c => c.FirstName == firstName).ToList();
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                _employees = _employees.Where(c => c.LastName == lastName).ToList();
            }
            switch (sortOrder)
            {
                case SortState.FirstNameDesc:
                    _employees = _employees.OrderByDescending(s => s.FirstName).ToList();
                    break;
                case SortState.LastNameAsc:
                    _employees = _employees.OrderBy(s => s.LastName).ToList();
                    break;
                case SortState.LastNameDesc:
                    _employees = _employees.OrderByDescending(s => s.LastName).ToList();
                    break;
                case SortState.AgeAsc:
                    _employees = _employees.OrderBy(s => s.Age).ToList();
                    break;
                case SortState.AgeDesc:
                    _employees = _employees.OrderByDescending(s => s.Age).ToList();
                    break;
                case SortState.ExperienceAsc:
                    _employees = _employees.OrderBy(s => s.Experience).ToList();
                    break;
                case SortState.ExperienceDesc:
                    _employees = _employees.OrderByDescending(s => s.Experience).ToList();
                    break;
                case SortState.PositionAsc:
                    _employees = _employees.OrderBy(s => s.Position.PositionName).ToList();
                    break;
                case SortState.PositionDesc:
                    _employees = _employees.OrderByDescending(s => s.Position.PositionName).ToList();
                    break;
                default:
                    _employees = _employees.OrderBy(s => s.FirstName).ToList();
                    break;
            }
            var count = _employees.Count();
            var items = _employees.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            EmployeesFilterViewModel employeesFilterViewModel = new EmployeesFilterViewModel(_context.Positions.ToList(), positionId,age,experience,firstName,lastName);
            EmployeesSortViewModel employeesSortViewModel = new EmployeesSortViewModel(sortOrder);
            EmployeesViewModel viewModel = new EmployeesViewModel(items, pageViewModel, employeesFilterViewModel, employeesSortViewModel);

            return View(viewModel);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,Age,PositionId,Experience")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                _employeesService.AddData("employees");
                _employees = _employeesService.GetData("employees").ToList();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName", employee.PositionId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName", employee.PositionId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,Age,PositionId,Experience")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    _employeesService.AddData("employees");
                    _employees = _employeesService.GetData("employees").ToList();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionName", employee.PositionId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'TaxiGomelContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            _employeesService.AddData("employees");
            _employees = _employeesService.GetData("employees").ToList();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
