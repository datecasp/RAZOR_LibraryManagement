﻿using System.Text.RegularExpressions;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<vmNotification> CreateBookService(vmBookCreate vmCreateBook)
        {
            var vmNotification = new vmNotification();

            var createBook = new BookModel
            {
             Title = vmCreateBook.Title,
             Author= vmCreateBook.Author,
             Description= vmCreateBook.Description,
             ImageUrl= vmCreateBook.ImageUrl,
             IsBorrowable= vmCreateBook.IsBorrowable,
             UrlHandle = FormatUrl(vmCreateBook.Title),
             CategoryId = vmCreateBook.CategoryId 
            };
            try
            {
                var bookResult = await _unitOfWork.BookRepository.CreateBook(createBook);
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

        public async Task<IEnumerable<vmBookIndex>> GetAllBooksService()
        {
            var booksList =new List<BookModel>();
            var bookIndexList = new List<vmBookIndex>();
            try
            {
                booksList = (await _unitOfWork.BookRepository.GetAllBooks()).ToList();
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
                var book = await _unitOfWork.BookRepository.GetBookById(id);
                var categoryName = _unitOfWork.CategoryRepository.GetCategoryById(book.CategoryId).Result.Name;
                if(book != null)
                {
                    vmBook.Title = book.Title;
                    vmBook.Author = book.Author;
                    vmBook.Description = book.Description;
                    vmBook.isBorrowable = book.IsBorrowable;
                    vmBook.Id = id;
                    vmBook.ImageUrl = book.ImageUrl;
                    vmBook.Category = categoryName;
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
            var result = Regex.Replace(url, " ", "-").ToLower();
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
