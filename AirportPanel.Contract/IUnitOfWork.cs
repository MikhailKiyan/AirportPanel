namespace AirportPanel.Contract
{
	using AirportPanel.Model.EntityModels;

	public interface IUnitOfWork
    {
		IRepository<Flight> Flights { get; }









		bool SaveChanges();
    }
}
