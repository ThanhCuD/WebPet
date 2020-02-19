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
    public class AnimalsController : Controller
    {
        private readonly AppDbContext _context;

        public AnimalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index(string sortOrder,string  searchString)
        {
            ViewData["currentFilter"] = searchString;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["BreedSortParm"] = sortOrder == "breed" ? "breed_desc" : "breed";
            ViewData["TypeSortParm"] = sortOrder == "type" ? "type_desc" : "type";
            var appDbContext = _context.Animals.Include(a => a.Owner);
            var ani = appDbContext.Select(_ => _);
            if (!string.IsNullOrEmpty(searchString))
            {
                ani = ani.Where(_ => _.AnimalName.Contains(searchString) ||
                            _.Breed.Contains(searchString) ||
                            _.TypeOfAnimal.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ani = ani.OrderByDescending(s => s.AnimalName);
                    break;
                case "breed_desc":
                    ani = ani.OrderByDescending(s => s.Breed);
                    break;
                case "type_desc":
                    ani = ani.OrderByDescending(s => s.TypeOfAnimal);
                    break;
                case "type":
                    ani = ani.OrderBy(s => s.TypeOfAnimal);
                    break;
                case "breed":
                    ani = ani.OrderBy(s => s.Breed);
                    break;
                default:
                    ani = ani.OrderBy(s => s.AnimalName);
                    break;
            }
                    return View(await ani.ToListAsync());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.AniID == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["OwnerID"] = new SelectList(_context.Custommers, "CustID", "FName");
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AniID,AnimalName,Breed,TypeOfAnimal,YearOld,OwnerID")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                animal.AniID = Guid.NewGuid();
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerID"] = new SelectList(_context.Custommers, "CustID", "CustID", animal.OwnerID);
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["OwnerID"] = new SelectList(_context.Custommers, "CustID", "CustID", animal.OwnerID);
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AniID,AnimalName,Breed,TypeOfAnimal,YearOld,OwnerID")] Animal animal)
        {
            if (id != animal.AniID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AniID))
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
            ViewData["OwnerID"] = new SelectList(_context.Custommers, "CustID", "CustID", animal.OwnerID);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.AniID == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var animal = await _context.Animals.FindAsync(id);
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(Guid id)
        {
            return _context.Animals.Any(e => e.AniID == id);
        }
    }
}
