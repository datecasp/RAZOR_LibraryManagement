namespace RAZOR_LibraryManagement.Models.Models
{
    public class BookUserModel
    {
        public int BookUserId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        //Indicates if this user book relation is actual or past
        public bool IsActualUser { get; set; }
        //Date when user borrow the book
        public DateTime BorrowDate { get; set; }
    }
}
