using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightManager.Data;
using FlightManager.Data.Models;

namespace FlightManager.Controllers
{
    public class PassangersController : Controller
    {
        private readonly AppDbContext _context;

        public PassangersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Passangers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Passangers.Include(p => p.Reservation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Passangers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = await _context.Passangers
                .Include(p => p.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passanger == null)
            {
                return NotFound();
            }

            return View(passanger);
        }

        // GET: Passangers/Create
        public IActionResult Create(int reservationId)
        {
            Reservation reservation = _context.Reservations.Find(reservationId);
            int passangersCount = _context.Passangers.Count(x => x.ReservationId == reservation.Id);
            ViewData["PassangersCount"] = passangersCount+1;
            ViewData["PassangersTotalCount"] = reservation.PassangersCount;
            ViewData["ReservationId"] = new SelectList(new int[] { reservationId });
            return View();
        }

        // POST: Passangers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,NationalId,PhoneNumber,Nationality,TypeOfTicket,ReservationId")] Passanger passanger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passanger);
                await _context.SaveChangesAsync();

                Reservation reservation = _context.Reservations.Find(passanger.ReservationId);
                int reservationId = reservation.Id;
                int passangersCount = _context.Passangers.ToList().Count(x => x.ReservationId == passanger.ReservationId);
                ViewData["PassangersCount"] = passangersCount;
                ViewData["ReservationId"] = new SelectList(new int[] { passanger.ReservationId });
                if (passangersCount==reservation.PassangersCount)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Create), new { reservationId });
                }
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", passanger.ReservationId);
            return View(passanger);
        }

        // GET: Passangers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = await _context.Passangers.FindAsync(id);
            if (passanger == null)
            {
                return NotFound();
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", passanger.ReservationId);
            return View(passanger);
        }

        // POST: Passangers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,NationalId,PhoneNumber,Nationality,TypeOfTicket,ReservationId")] Passanger passanger)
        {
            if (id != passanger.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passanger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassangerExists(passanger.Id))
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
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", passanger.ReservationId);
            return View(passanger);
        }

        // GET: Passangers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = await _context.Passangers
                .Include(p => p.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passanger == null)
            {
                return NotFound();
            }

            return View(passanger);
        }

        // POST: Passangers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passanger = await _context.Passangers.FindAsync(id);
            _context.Passangers.Remove(passanger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassangerExists(int id)
        {
            return _context.Passangers.Any(e => e.Id == id);
        }
    }
}
