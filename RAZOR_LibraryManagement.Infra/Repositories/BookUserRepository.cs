using AutoMapper;
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
