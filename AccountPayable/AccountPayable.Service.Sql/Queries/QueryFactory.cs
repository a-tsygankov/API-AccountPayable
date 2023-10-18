using System;
using Genesis.Ensure;

namespace AccountPayable.Sql.Queries
{
	public static class QueryFactory
	{
		public static ICrudQueries Get(Type type)
		{
            Ensure.ArgumentNotNull(type, nameof(type));

			switch(type.Name)
			{
				case "Bill": return 
			}

		}
	}
}

