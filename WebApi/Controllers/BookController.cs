using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.AddControllers
{
    
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        //readonly yapınca uygulama içerisinden değiştirilemez. Sadece constructor içinde set edilebilirler.

        public BookController (BookStoreDbContext context)
        {
            _context = context ; 
        }
        //private  static List<Book> BookList = new List<Book>()
        //{
        //     new Book{
        //             Id=1,
        //             Title="Lean Startup",
        //             GenreId=1, //Personal Growth
        //             PageCount= 200,
        //             PublishDate = new DateTime(2001,06,12)
        //         },
        //         new Book{
        //             Id=2,
        //             Title="Herland",
        //             GenreId=2, //Personal Growth
        //             PageCount= 250,
        //             PublishDate = new DateTime(2010,05,23)
        //         },
        //         new Book{
        //             Id=3,
        //             Title="Dune",
        //             GenreId=2, //Personal Growth
        //             PageCount= 540,
        //             PublishDate = new DateTime(2001,12,21)
        //         }
        //};

        [HttpGet]
       public IActionResult GetBooks()
       {
            //    var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            //    return bookList;
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle(); 
            return Ok(result);
       }

       [HttpGet("{id}")]
       public IActionResult GetById(int id)
       {
        //    var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
        //    return book;
            GetBookByIdQuery query = new GetBookByIdQuery(_context); 
            var result = query.Handle(id);
            return Ok(result); 
       }

        //    [HttpGet]
        //    public Book Get([FromQuery] string id)
        //    {
        //        var book = BookList.Where(book => book.Id ==Convert.ToInt32(id)).SingleOrDefault();
        //        return book;
        //    }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            // var book = _context.Books.SingleOrDefault(x=> x.Title == newBook.Title);
            // if(book != null)
            // {
            //     return BadRequest();
            // }
            // _context.Books.Add(newBook);
            // _context.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            // var book = _context.Books.SingleOrDefault(x=> x.Id==id);
            // if(book is null)
            // {
            //     return BadRequest();
            // }
            // book.GenreId = updatedBook.GenreId !=default ? updatedBook.GenreId : book.GenreId;
            // book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            // book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate :book.PublishDate;
            // book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            // _context.SaveChanges();
            UpdateBookCommand ubc = new UpdateBookCommand(_context);
            try
            {            
                ubc.Model = updatedBook;
                ubc.Handle(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if(book is null)
            {
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}