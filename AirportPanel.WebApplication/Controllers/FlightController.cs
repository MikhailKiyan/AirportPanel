using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirportPanel.DAL;
using AirportPanel.Model.EntityModels;

namespace AirportPanel.WebApplication.Controllers
{
	public class FlightController : Controller
	{
		private readonly AirportPanelDataDbContext _context;

		public FlightController(AirportPanelDataDbContext context) {
			_context = context;
		}

		// GET: Flight
		public async Task<IActionResult> Index() {
			return View(await _context.Flights.ToListAsync());
		}

		// GET: Flight/Details/5
		public async Task<IActionResult> Details(Guid? id) {
			if (id == null) {
				return NotFound();
			}

			var flight = await _context.Flights
				.FirstOrDefaultAsync(m => m.Id == id);

			if (flight == null) {
				return NotFound();
			}

			return View(flight);
		}

		// GET: Flight/Create
		public IActionResult Create() {
			return View();
		}

		// POST: Flight/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("FlightType,ArrivalOn,DepartureOn,Number,Id,CreatedOn,CreatedBy,MofidiedOn,MofidiedBy")] Flight flight) {
			if (ModelState.IsValid) {
				flight.Id = Guid.NewGuid();
				_context.Add(flight);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(flight);
		}

		// GET: Flight/Edit/5
		public async Task<IActionResult> Edit(Guid? id) {
			if (id == null) {
				return NotFound();
			}

			var flight = await _context.Flights.FindAsync(id);

			if (flight == null) {
				return NotFound();
			}

			return View(flight);
		}

		// POST: Flight/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id,
			[Bind("FlightType,ArrivalOn,DepartureOn,Number,Id,CreatedOn,CreatedBy,MofidiedOn,MofidiedBy")] Flight flight) {
			if (id != flight.Id) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				try {
					_context.Update(flight);
					await _context.SaveChangesAsync();
				} catch (DbUpdateConcurrencyException) {
					if (!FlightExists(flight.Id)) {
						return NotFound();
					} else {
						throw;
					}
				}

				return RedirectToAction(nameof(Index));
			}

			return View(flight);
		}

		// GET: Flight/Delete/5
		public async Task<IActionResult> Delete(Guid? id) {
			if (id == null) {
				return NotFound();
			}

			var flight = await _context.Flights
				.FirstOrDefaultAsync(m => m.Id == id);

			if (flight == null) {
				return NotFound();
			}

			return View(flight);
		}

		// POST: Flight/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id) {
			var flight = await _context.Flights.FindAsync(id);
			_context.Flights.Remove(flight);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool FlightExists(Guid id) {
			return _context.Flights.Any(e => e.Id == id);
		}
	}
}
