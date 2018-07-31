namespace AirportPanel.Model.ViewModels
{
	using System.ComponentModel.DataAnnotations;

	using AirportPanel.Model.Abstracts;

	public class FlightStatusVM : ViewModel<int>
    {
		[Display(Name = "Status:")]
		public virtual string Name { get; set; }

		[Display(Name = "Discription:")]
		public virtual string Discription { get; set; }
    }
}
