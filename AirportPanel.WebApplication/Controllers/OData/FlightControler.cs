using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AirportPanel.DAL;
using AirportPanel.Model.EntityModels;
using AirportPanel.Utility.DB;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirportPanel.WebApplication.ODataControllers
{
	public class FlightsController : ODataController
	{
		private readonly AirportPanelDataDbContext context;

		readonly UnitOfWork uow;

		public FlightsController(AirportPanelDataDbContext context) {
			this.context = context;
		}

		[EnableQuery]
		public IActionResult Get() {
			return Ok(this.context.Flights);
		}
		[EnableQuery]
		public IActionResult Get([FromODataUri] Guid key) {
			IQueryable<Flight> result = this.context.Flights.Where(p => p.Id == key);
			return Ok(this.context.Flights.FirstOrDefault(item => item.Id == key));
		}

		/*
		[EnableQuery]
		public IActionResult Post([FromBody]Flight flight) {
			this.context.Set<Flight>().Add(flight);
			this.context.SaveChanges();
			return Created(flight);
		}*/

		
		public async Task<IActionResult> Post([FromBody]Flight flight) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}
			this.context.Flights.Add(flight);
			await this.context.SaveChangesAsync();
			return Created(flight);
		}

		public async Task<IActionResult> Patch([FromODataUri] Guid key, Delta<Flight> flight) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			var entity = await this.context.Flights.FindAsync(key);

			if (entity == null) {
				return NotFound();
			}

			flight.Patch(entity);
			try {
				await this.context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!ProductExists(key)) {
					return NotFound();
				} else {
					throw;
				}
			}
			return Updated(entity);
		}

		public async Task<IActionResult> Put([FromODataUri] Guid key, Flight flight) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if (key != flight.Id) {
				return BadRequest();
			}

			this.context.Entry(flight).State = EntityState.Modified;
			try {
				await this.context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!ProductExists(key)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return Updated(flight);
		}

		public async Task<IActionResult> Delete([FromODataUri] Guid key) {
			var flight = await this.context.Flights.FindAsync(key);
			if (flight == null) {
				return NotFound();
			}
			this.context.Flights.Remove(flight);
			await this.context.SaveChangesAsync();
			return StatusCode((int)HttpStatusCode.NoContent);
		}








		private bool ProductExists(Guid key) {
			return this.context.Flights.Any(p => p.Id == key);
		}

	}
}
