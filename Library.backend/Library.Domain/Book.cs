using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain
{
    public class Book
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public int Isbn  { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; } 
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public DateTime TimeTaken { get; set; }
        public DateTime? TimeToReturn { get; set; }
    }
}
