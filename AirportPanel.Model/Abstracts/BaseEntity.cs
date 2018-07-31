namespace AirportPanel.Abstracts
{
	using System;

	public abstract class BaseModel
    {
		public virtual DateTime? CreatedOn { get; set; }

		public virtual Guid CreatedBy { get; set; }

		public virtual DateTime? MofidiedOn { get; set; }

		public virtual Guid MofidiedBy { get; set; }
	}

	public abstract class BaseModel<Guid> : BaseModel
	{
		public virtual Guid Id { get; set; }
	}

	public abstract class Entity : BaseModel<Guid>
	{
	}

	public abstract class Lookup: BaseModel<int>
	{
		public virtual string Name { get; set; }

		public virtual string Discription { get; set; }
	}
}
