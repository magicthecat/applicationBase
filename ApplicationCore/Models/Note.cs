namespace ApplicationCore.Models
{
    public class Note
    {
        public Guid Id { get; private set; }

        public string Content { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public Note(string content)
        {
            Id = Guid.NewGuid();
            Content = content ?? throw new ArgumentNullException(nameof(content));
            CreatedAt = DateTime.UtcNow;
        }
        public void UpdateContent(string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Note content cannot be empty or whitespace.");

            Content = newContent;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}