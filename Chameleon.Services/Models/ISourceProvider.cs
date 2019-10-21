namespace Chameleon.Services.Models
{
    public interface ISourceProvider
    {
        string Title { get; set; }

        string Description { get; set; }

        string ImageUrl { get; set; }

        bool Soon { get; set; }

        bool Enabled { get; set; }

        bool CanEdit { get; }
    }
}
