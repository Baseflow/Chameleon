using System;
namespace Chameleon.Services.Models
{
    public class Provider
    {
        public Provider()
        {
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool Soon { get; set; }

        public bool Enabled { get; set; }
    }
}
