using System;
namespace Chameleon.Services.Media
{
    public interface IPlaylist
    {
        Guid Id { get; set; }
        string Title { get; set; }
    }
}
