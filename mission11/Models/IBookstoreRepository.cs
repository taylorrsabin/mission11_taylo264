using System;
using System.Linq;

namespace mission11.Models
{
	public interface IBookstoreRepository
	{
        IQueryable<Books> Books { get; }
    }
}

