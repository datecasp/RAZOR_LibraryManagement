using System.Text.RegularExpressions;
using System.Xml.Linq;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Models;
using RAZOR_LibraryManagement.Domain.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
           _bookRepository = bookRepository;
        }

        public async Task<vmBookCreate> CreateBookService(vmBookCreate vmCreateBook)
        {
            var vmBookResult = new vmBookCreate();
            var createBook = new Book
            {
             Title = vmCreateBook.Title,
             Author= vmCreateBook.Author,
             Description= vmCreateBook.Description,
             ImageUrl= vmCreateBook.ImageUrl,
             IsBorrowable= vmCreateBook.IsBorrowable,
             UrlHandle = FormatUrl(vmCreateBook.Title),
             Category = VmCategoryStringToCategory(vmCreateBook.Category)
            };
            try
            {
                var bookResult = await _bookRepository.CreateBook(createBook);
                vmBookResult.Title= bookResult.Title;
                vmBookResult.Author= bookResult.Author;
                vmBookResult.Description= bookResult.Description;
                vmBookResult.ImageUrl= bookResult.ImageUrl;
                vmBookResult.IsBorrowable = bookResult.IsBorrowable;
                vmBookResult.Category = bookResult.Category.Name;
            }
            catch (Exception ex)
            {

            }
            return vmBookResult;
        }

        public async Task<IEnumerable<vmBookIndex>> GetAllBooksService()
        {
            var booksList =new List<Book>();
            var bookIndexList = new List<vmBookIndex>();
            try
            {
                booksList = (await _bookRepository.GetAllBooks()).ToList();
                foreach (var book in booksList)
                {
                    var vwBook = new vmBookIndex
                    {
                        Id = book.BookId,
                        Title = book.Title,
                        Author = book.Author,
                        IsBorrowable= book.IsBorrowable
                    };

                    bookIndexList.Add(vwBook);
                }
            }
            catch(Exception ex)
            {

            }
            return bookIndexList;        }

        public async Task<vmBookDetails> GetBookByIdService(int id)
        {
            var vmBook = new vmBookDetails();
            try
            {
                var book = await _bookRepository.GetBookById(id);
                if(book != null)
                {
                    vmBook.Title = book.Title;
                    vmBook.Author = book.Author;
                    vmBook.Description = book.Description;
                    vmBook.isBorrowable = book.IsBorrowable;
                    vmBook.Id = id;
                    vmBook.ImageUrl = book.ImageUrl;
                }
            }
            catch(Exception ex)
            {

            }
            return vmBook;
        }


        #region Private methods

        //Replace white spaces with dashes for readability in url 
        private static string FormatUrl(string url)
        {
            var result = Regex.Replace(url, " ", "-");
            return result;
        }

        private vmCategoryIndex CategoryToVmCategory(Category category)
        {
            var result = new vmCategoryIndex
            {
                Name= category.Name,
                IsActive= category.IsActive
            };

            return result;
        }

        private Category VmCategoryToCategory(vmCategoryIndex vmCategory)
        {
            var result = new Category
            {
                Name = vmCategory.Name,
                IsActive = vmCategory.IsActive
            };

            return result;
        }

        private Category VmCategoryStringToCategory(string vmCategory)
        {
            var result = new Category
            {
                Name = vmCategory,
                IsActive = true
            };

            return result;
        }
        #endregion
    }
}
