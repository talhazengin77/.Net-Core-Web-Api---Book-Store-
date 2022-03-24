using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using System.Collections.Generic;
using WebApi.Common;
using System;

namespace WebApi.BookOperations.GetById
{
    public class GetBookByIdQuery
    {
        //private readonly olarak tanımlarsak sadece constructor içinden ayarlanır.
        private readonly BookStoreDbContext _dbContext;
        public GetBookByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BooksViewModel Handle(int id)
        {
            var book = _dbContext.Books.Where(x => x.Id==id).SingleOrDefault();
            BooksViewModel bvm = new BooksViewModel();
            bvm.Title = book.Title;
            bvm.GenreId = ((GenreEnum)book.GenreId).ToString();
            bvm.PageCount = book.PageCount;
            bvm.PublishDate =book.PublishDate.Date.ToString("dd//MM/yyyy");
            return bvm;
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