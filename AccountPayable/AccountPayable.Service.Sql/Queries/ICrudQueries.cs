using System;
namespace AccountPayable.Sql.Queries
{
	public interface ICrudQueries
	{
        public string GetAll();
        public string GetById();
        public string Add();
        public string Update();
        public string Delete();

    }
}

