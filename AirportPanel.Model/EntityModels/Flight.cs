namespace AirportPanel.Model.EntityModels
{
	using AirportPanel.Abstracts;
	using AirportPanel.Model.Enums;
	using System;

	public class Flight : BaseModel
    {
		public virtual FlightType? FlightType { get; set; }

		public virtual DateTime? ArrivalOn { get; set; }

		public virtual DateTime? DepartureOn { get; set; }

		public virtual string Number { get; set; }

		public virtual FlightStatus Status { get; set; }
		
		public virtual string Test { get; set; }
	}
}
