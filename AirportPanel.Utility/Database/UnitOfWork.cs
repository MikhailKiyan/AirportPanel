namespace AirportPanel.Utility.DB
{
	using System;
	//using System.Data.Entity;

	using AirportPanel.Contract;
	using AirportPanel.Model.EntityModels;

	public class UnitOfWork
    {
		//protected readonly DbContext context;

		protected IRepository<Flight> flightRepository;


		public UnitOfWork() {

		}

		/*
		public UnitOfWork(DbContext context) {

		}*/

	}
}
