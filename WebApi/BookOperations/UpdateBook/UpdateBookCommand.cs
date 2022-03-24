using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using System.Collections.Generic;
using WebApi.Common;
using System;

namespace WebApi.BookOperations.UpdateBook
{

    public class UpdateBookCommand
    {
        public UpdateBookModel Model {get;set;}

        //private readonly olarak tanımlarsak sadece constructor içinden ayarlanır.
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id)
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id==id);
            if(book is null)
            {
                throw new InvalidOperationException("Değişiklik yapmak istediğiniz kitap bulunmuyor.");
            }
            book.GenreId = Model.GenreId !=default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate :book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }


    }

    public class UpdateBookModel 
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get ; set ; }
        public DateTime PublishDate { get; set; }
    }

}