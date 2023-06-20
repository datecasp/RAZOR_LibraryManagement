using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
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

        public async Task<IEnumerable<BookUserModel>> GetBooksOfUser(int userId)
        {
            var booksList = await _lM_DbContext.BookUsers
                .Where(bu => bu.UserId == userId)
                .ToListAsync();
            var result = _mapper.Map<List<BookUserModel>>(booksList);
            return result;
        }
    }
}
