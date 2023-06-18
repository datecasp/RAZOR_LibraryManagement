namespace RAZOR_LibraryManagement.Models.Entities
{
    public class BookUser
    {
        public int BookUserId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        //Indicates if this user book relation is actual or past
        public bool IsActualUser { get; set; }
        //Date when user borrows the book
        public DateTime BorrowDate { get; set; }

        //Nav properties
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
