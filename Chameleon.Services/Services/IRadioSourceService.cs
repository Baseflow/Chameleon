using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chameleon.Services.Models;
using Refit;

namespace Chameleon.Services.Services
{
    public interface IRadioSourceService
    {
        [Get("/stations/bycountry/{country}")]
        Task<IList<RadioSource>> GetRadioSourceByCountry(string country);
    }
}
