using System.Text.RegularExpressions;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<vmNotification> CreateBookService(BookModel bookModel)
        {
            var vmNotification = new vmNotification();
            bookModel.UrlHandle = FormatUrl(bookModel.Title);
            try
            {
                var bookResult = await _unitOfWork.BookRepository.CreateBook(bookModel);
                _unitOfWork.Save();
                if (bookResult != null)
                {
                    vmNotification.Type = Lang.Notification.NotificationType.Success;
                    vmNotification.Message = "Book created successfully";
                    return vmNotification;
                }
            }
            catch (Exception ex)
            {
                vmNotification.Type = Lang.Notification.NotificationType.Error;
                vmNotification.Message = "Exception thrown! " + ex.Message;
                return vmNotification;
            }
            vmNotification.Type = Lang.Notification.NotificationType.Error;
            vmNotification.Message = "Hmmm something went wrong here....";
            return vmNotification;
        }

        public async Task<IEnumerable<BookModel>> GetAllBooksService()
        {
            var booksList = new List<BookModel>();
            try
            {
                booksList = (await _unitOfWork.BookRepository.GetAllBooks()).ToList();
            }
            catch (Exception ex)
            {

            }
            return booksList;
        }

        public async Task<BookModel> GetBookByIdService(int id)
        {
            var book = new BookModel();
            try
            {
                book = await _unitOfWork.BookRepository.GetBookById(id);
            }
            catch (Exception ex)
            {

            }
            return book;
        }


        #region Private methods

        //Replace white spaces with dashes for readability in url 
        private static string FormatUrl(string url)
        {
            var result = Regex.Replace(url, " ", "-").ToLower();
            return result;
        }

        #endregion
    }
}
