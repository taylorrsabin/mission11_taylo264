using System;
using System.Linq;
using mission11.Models;

namespace mission11.Models.ViewModels
{
	public class BooksViewModel
	{
        public IQueryable<Books> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}

