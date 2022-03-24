using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using System.Collections.Generic;
using WebApi.Common;
using System;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        //private readonly olarak tanımlarsak sadece constructor içinden ayarlanır.
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach(var book in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    GenreId = ((GenreEnum)book.GenreId).ToString(),
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.Date.ToString("dd//MM/yyyy")

                });
            }
           return vm;
        }
    }

    public class BooksViewModel //Kontrollü bir şekilde kullanılması için burada tanımladık.
    //ViewModel sadece UI ' a dönmek için kullanıyoruz.
    {
        public string Title {get;set;}
        public int PageCount{get;set;}
        public string PublishDate { get; set; }
        public string GenreId { get; set; }
    }
}