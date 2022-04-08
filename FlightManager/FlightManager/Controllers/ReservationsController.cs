using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightManager.Data;
using FlightManager.Data.Models;
using Microsoft.AspNetCore.Authorization;
using FlightManager.ViewModels.Reservations;
using Microsoft.AspNetCore.Identity;

namespace FlightManager.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> userManager;

        public ReservationsController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Reservations
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Reservations.Include(r => r.Flight);
            return View(await appDbContext.ToListAsync());
        }

        public IActionResult NotAvailableFlights()
        {
            return this.View();
        }
        [Authorize]
        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            if (!_context.Flights.Any(x => x.LiftOff > DateTime.UtcNow))
            {
                return RedirectToAction(nameof(NotAvailableFlights));
            }
            ViewData["FlightId"] = new SelectList(_context.Flights.Where(x => x.LiftOff > DateTime.Now), "Id", "UniqueNumber");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FlightId,Email,PassangersCount")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                var reservationId = _context.Reservations.OrderByDescending(x => x.Id).FirstOrDefault().Id;

                return RedirectToAction(nameof(Create), "Passangers", new { reservationId });

            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "UniqueNumber", reservation.FlightId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            EditReservationViewModel model = new EditReservationViewModel()
            {
                Id = reservation.Id,
                PassangersCount = reservation.PassangersCount,
                IsConfirmed = reservation.IsConfirmed,
                Email = reservation.Email,
                UserConfirmedAccount=reservation.User!=null?$"{reservation.User.FirstName} {reservation.User.LastName}":"n/a"
            };

            Flight flight = await _context.Flights.FindAsync(reservation.FlightId);

            model.Flight = new FlightReservationViewModel()
            {
                Id = flight.Id,
                FlightFrom = flight.FlightFrom,
                FlightTo = flight.FlightTo,
                LiftOff = flight.LiftOff,
                LandOn = flight.LandOn,
                PilotName = flight.PilotName,
                TypeOfPlane = flight.TypeOfPlane,
                UniqueNumber = flight.UniqueNumber
            };

            model.Passangers = reservation
                .Passangers
                .Select(x => new PassangerReservationViewModel()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = string.IsNullOrWhiteSpace(x.PhoneNumber)? "n/a" : x.PhoneNumber,
                    TypeOfTicket = x.TypeOfTicket
                })
                .ToList();
            return View(model);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IsConfirmed,Email")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }
            Reservation editReservation = await _context.Reservations.FindAsync(id);
            editReservation.IsConfirmed = reservation.IsConfirmed;
            editReservation.User = await userManager.GetUserAsync(this.User);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(editReservation.Id))
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
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", reservation.FlightId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
