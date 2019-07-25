using System;
namespace Chameleon.Services.Media
{
    public class Playlist : IPlaylist
    {
        public Playlist()
        {
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
