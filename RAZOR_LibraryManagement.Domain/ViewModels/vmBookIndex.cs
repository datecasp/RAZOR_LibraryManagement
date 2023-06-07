﻿using System.ComponentModel.DataAnnotations;

namespace RAZOR_LibraryManagement.Domain.ViewModels
{
    public class vmBookIndex
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
    }
}
