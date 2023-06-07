﻿using RAZOR_LibraryManagement.Domain.Models;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
    }
}
