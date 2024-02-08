namespace Caderninh.io.Domain.Notes
{
    public class Notes
    {
        public Guid Id { get; }

        public Guid NoteCategoryId { get; }

        public string Body { get; set; } = null!;

        public DateTime CreatedAt { get; }

        public DateTime UpdatedAt { get; set; }

        public Notes(Guid noteCategoryId, string body, Guid? id)
        {
            Body = body;
            NoteCategoryId = noteCategoryId;
            Id = id ?? Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }    
}