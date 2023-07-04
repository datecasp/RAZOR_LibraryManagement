using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class BookUserRepository : IBookUserRepository
    {
        private readonly LM_DbContext _lM_DbContext;
        private readonly IMapper _mapper;

        public BookUserRepository(LM_DbContext lM_DbContext, IMapper mapper)
        {
            _lM_DbContext = lM_DbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookUserModel>> GetAll()
        {
            var booksList = _lM_DbContext.BookUsers;
            var result = _mapper.Map<List<BookUserModel>>(booksList);

            return result;
        }

        public async Task<BookUserModel> GetBookUserById(int id)
        {
            var bookUser = _lM_DbContext.BookUsers.FindAsync(id).Result;
            var result = _mapper.Map<BookUserModel>(bookUser);

            return result;
        }

        public async Task<IEnumerable<BookUserModel>> GetBooksOfUser(int userId)
        {
            var booksList = await _lM_DbContext.BookUsers
                .Where(bu => bu.UserId == userId)
                .ToListAsync();
            var result = _mapper.Map<List<BookUserModel>>(booksList);
            return result;
        }

        public async Task<BookUserModel> AddBookToUser(BookUserModel bookUserModel, int maxBooks)
        {
            var result = new BookUserModel();
            var bookUser = _mapper.Map<BookUser>(bookUserModel);
            if (CheckMaxNumOfBooksForUser(bookUser.UserId).Result < maxBooks)
            {
                var bookAdded = _lM_DbContext.BookUsers.Add(bookUser);

                if (bookAdded.Entity != null)
                {
                    result = _mapper.Map<BookUserModel>(bookAdded.Entity);
                }
            }
            return result;
        }

        public async Task<IEnumerable<int>> GetBorrowerUsers()
        {
            return _lM_DbContext.BookUsers.Where(bu => bu.IsActualUser).Select(bu => bu.UserId);
        }

        public async Task<IEnumerable<int>> GetBorrowedBooks()
        {
            return _lM_DbContext.BookUsers.Where(bu => bu.IsActualUser).Select(bu => bu.BookId);
        }

        public async Task<IEnumerable<int>> GetNotBorrowedBooks()
        {
            return _lM_DbContext.BookUsers.Where(bu => bu.IsActualUser != false).Select(bu => bu.BookId);
        }

        public async Task<BookUserModel> UpdateBookUser(BookUserModel bu)
        {
            var result = new BookUserModel();
            var bookUser = _mapper.Map<BookUser>(bu);

            var bookUpdated = _lM_DbContext.BookUsers.Update(bookUser);

            if (bookUpdated.Entity != null)
            {
                result = _mapper.Map<BookUserModel>(bookUpdated.Entity);
            }

            return result;
        }

        #region private method

        private async Task<int> CheckMaxNumOfBooksForUser(int userId)
        {
            return _lM_DbContext.BookUsers
                .Where(bu => bu.UserId == userId && (bu.IsActualUser))
                .Count();
        }

        #endregion
    }
}
