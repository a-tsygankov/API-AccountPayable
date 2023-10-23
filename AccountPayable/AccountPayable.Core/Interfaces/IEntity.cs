namespace AccountPayable.Core.Interfaces
{
    public interface IEntity
	{
		public long Id { get; }

        //public long UpdateCounter { get; set; } // to resolve conflicts with concurrent modifications
    }
}

