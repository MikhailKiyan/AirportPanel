namespace AirportPanel.Utility.DB
{
	using System;
	//using System.Data.Entity;

	using AirportPanel.DAL;
	using AirportPanel.Contract;
	using AirportPanel.Model.EntityModels;
	using Microsoft.EntityFrameworkCore;

	public class UnitOfWork : IUnitOfWork
    {
		protected readonly DbContext context;

		public UnitOfWork() {
			this.context = new AirportPanelDataDbContext();
		}
		
		public UnitOfWork(DbContext context) {
			this.context = context;
		}

		#region Repositories

		public IRepository<Flight> Flights => this.flightRepository
			?? (this.flightRepository = new Repository<Flight>(this.context));

		protected IRepository<Flight> flightRepository;

		public IRepository<FlightStatus> FlightStatuses => this.flightStatusRepository
			?? (this.flightStatusRepository = new Repository<FlightStatus>(this.context));

		protected IRepository<FlightStatus> flightStatusRepository;

		public bool SaveChanges() {
			throw new NotImplementedException();
		}

		#endregion

	}
}
