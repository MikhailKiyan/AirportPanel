namespace AirportPanel.DAL
{
	using Microsoft.EntityFrameworkCore;

	using AirportPanel.Model.EntityModels;
	
	public class AirportPanelDbContext: DbContext
    {
		public AirportPanelDbContext(DbContextOptions<AirportPanelDbContext> options)
			: base(options) {
		}

		public virtual DbSet<Flight> Flights { get; set; }

		public virtual DbSet<FlightStatus> FlightStatuses { get; set; }


	}
}
