namespace AirportPanel.DAL
{
	using Microsoft.EntityFrameworkCore;

	using AirportPanel.Model.EntityModels;
	
	public class AirportPanelDataDbContext : DbContext
    {
		public AirportPanelDataDbContext(DbContextOptions<AirportPanelDataDbContext> options = null)
			: base(options) {
		}

		public virtual DbSet<Flight> Flights { get; set; }

		public virtual DbSet<FlightStatus> FlightStatuses { get; set; }
	}
}
