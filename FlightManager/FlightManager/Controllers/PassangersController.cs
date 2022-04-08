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
using FlightManager.Services;

namespace FlightManager.Controllers
{
    public class PassangersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPassangersService passangersService;

        public PassangersController(AppDbContext context, IPassangersService passangersService)
        {
            _context = context;
            this.passangersService = passangersService;
        }

        // GET: Passangers
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var appDbContext = passangersService.GetPassangersAsync();
            return View(await appDbContext);
        }

        // GET: Passangers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = await passangersService.GetPassangerByIdAsync((int)id);
            if (passanger == null)
            {
                return NotFound();
            }

            return View(passanger);
        }

        // GET: Passangers/Create
        public async Task<IActionResult> Create(int reservationId)
        {
            Reservation reservation = await passangersService.FindPassangersReservation(reservationId);
            int passangersCount = passangersService.PassangersCountAsync(reservationId);
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
                await passangersService.CreatePassangerASync(passanger);

                Reservation reservation = await passangersService.GetReservationByIdAsync(passanger.ReservationId);
                int reservationId = reservation.Id;
                int passangersCount = passangersService.PassangersCountAsync(reservationId);
                ViewData["PassangersCount"] = passangersCount;
                ViewData["ReservationId"] = new SelectList(new int[] { passanger.ReservationId });
                if (passangersCount==reservation.PassangersCount)
                {
                    return RedirectToAction(nameof(Index),"Home");
                }
                else
                {
                    return RedirectToAction(nameof(Create), new { reservationId });
                }
            }
            ViewData["ReservationId"] = new SelectList(await passangersService.GetReservationsAsync(), "Id", "Id", passanger.ReservationId);
            return View(passanger);
        }
        [Authorize]
        // GET: Passangers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = await passangersService.FindPassangersByIdAsync((int)id);
            if (passanger == null)
            {
                return NotFound();
            }
            ViewData["ReservationId"] = new SelectList(await passangersService.GetReservationsAsync(), "Id", "Id", passanger.ReservationId);
            return View(passanger);
        }

        // POST: Passangers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
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
                    await passangersService.UpdatePassangerAsync(passanger);
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
            ViewData["ReservationId"] = new SelectList(await passangersService.GetReservationsAsync(), "Id", "Id", passanger.ReservationId);
            return View(passanger);
        }

        [Authorize]
        // GET: Passangers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = await passangersService.GetPassangerByIdAsync((int)id);
            if (passanger == null)
            {
                return NotFound();
            }

            return View(passanger);
        }
        [Authorize]
        // POST: Passangers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passanger = await passangersService.FindPassangersByIdAsync(id);
            await passangersService.DeletePassangerConfirmedAsync(passanger);
            return RedirectToAction(nameof(Index));
        }

        private bool PassangerExists(int id)
        {
            return passangersService.CheckPassangerExist(id);
        }
    }
}
