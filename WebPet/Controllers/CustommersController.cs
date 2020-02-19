using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPet.Models;

namespace WebPet.Controllers
{
    public class CustommersController : Controller
    {
        private readonly AppDbContext _context;

        public CustommersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Custommers
        public async Task<IActionResult> Index(string sortOrder,
                                            string currentFilter,
                                            string searchString,
                                            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PhoneSortParm"] = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewData["CurrentFilter"] = searchString;

            var custs = _context.Custommers.Select(_=>_);
            if (!String.IsNullOrEmpty(searchString))
            {
                custs = custs.Where(s => s.FName.Contains(searchString)
                                       || s.PhoneNo.Contains(searchString));
            }
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            switch (sortOrder)
            {
                case "name_desc":
                    custs = custs.OrderByDescending(s => s.FName);
                    break;
                case "phone":
                    custs = custs.OrderBy(s => s.PhoneNo);
                    break;
                case "phone_desc":
                    custs = custs.OrderByDescending(s => s.PhoneNo);
                    break;
                default:
                    custs = custs.OrderBy(s => s.FName);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Custommer>.CreateAsync(custs.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Custommers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custommer = await _context.Custommers
                .FirstOrDefaultAsync(m => m.CustID == id);
            if (custommer == null)
            {
                return NotFound();
            }

            return View(custommer);
        }

        // GET: Custommers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Custommers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNo,FName,YearOld,Email,Address")] Custommer custommer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    custommer.CustID = custommer.PhoneNo;
                    _context.Add(custommer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return View(custommer);
        }

        // GET: Custommers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custommer = await _context.Custommers.FindAsync(id);
            if (custommer == null)
            {
                return NotFound();
            }
            return View(custommer);
        }

        // POST: Custommers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustID,PhoneNo,FName,YearOld,Email,Address")] Custommer custommer)
        {
            if (id != custommer.CustID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(custommer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustommerExists(custommer.CustID))
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
            return View(custommer);
        }

        // GET: Custommers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custommer = await _context.Custommers
                .FirstOrDefaultAsync(m => m.CustID == id);
            if (custommer == null)
            {
                return NotFound();
            }

            return View(custommer);
        }

        // POST: Custommers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var custommer = await _context.Custommers.FindAsync(id);
            _context.Custommers.Remove(custommer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustommerExists(string id)
        {
            return _context.Custommers.Any(e => e.CustID == id);
        }
    }
}
