namespace AirportPanel.Model.Abstracts
{
	using System.ComponentModel.DataAnnotations;

	public abstract class ViewModel
	{
	}

	public abstract class ViewModel<TEntity> : ViewModel
	{
		[Display(Name = "Id:")]
		public virtual TEntity Id { get; set; }
	}
}
