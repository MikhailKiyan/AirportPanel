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
	public class FlightController : ODataController, IDisposable
	{
		private readonly AirportPanelDataDbContext context;

		readonly UnitOfWork uow;

		public FlightController(AirportPanelDataDbContext context) {
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

		#region IDisposable Implement

		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					// TODO: dispose managed state (managed objects).
					
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.
				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~FlightController() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose() {
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
