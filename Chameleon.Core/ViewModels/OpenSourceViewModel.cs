using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Chameleon.Core.Helpers;
using Chameleon.Services.Services;
using MediaManager;
using MediaManager.Media;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace Chameleon.Core.ViewModels
{
    public class OpenSourceViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        public OpenSourceViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            _userDialogs = userDialogs ?? throw new ArgumentNullException(nameof(userDialogs));

        }

        private List<OpenSourceButton> _openSourceButtons = new List<OpenSourceButton>();
        public List<OpenSourceButton> OpenSourceButtons
        {
            get => _openSourceButtons;
            set => SetProperty(ref _openSourceButtons, value);
        }


        public override async Task Initialize()
        {




        }

        public Task<IList<OpenSourceButton>> OpenSourceButtonsObject()
        {
            IList<OpenSourceButton> openSourceButtonsItem;
            if (openSourceButtonsItem != null)
            {
                foreach (var item in openSourceButtonsItem)
                {
                    var urlMediaManager = "https://github.com/martijn00/XamarinMediaManager";
                    var title = Title;
                    var command = OpenBrowserCommand;
                    var mediaManger = urlMediaManager + title + command;
                }
            }


            return Task.FromResult<OpenSourceButton>(openSourceButtonsItem);
        }


        //public Task<IList<OpenSourceButton>> OpenSourceButtonsObject()
        //{
        //    IList<OpenSourceButton> openSourceButtonsItem;
        //    foreach (var item in openSourceButtonsItem)
        //    {

        //    }
        //    var mediaManager = "https://github.com/martijn00/XamarinMediaManager";
        //    var title = OpenSourceButton.;

        //    return Task.FromResult<OpenSourceButton>(urls);
        //}

        //public Task<IList<OpenSourceButton>> OpenSourceButtonsObject()
        //{
        //    var myUrl = CommonValues.URL + "images/Uploads/e0111.png";

        //    var mediaManager = "https://github.com/martijn00/XamarinMediaManager";
        //    var title = OpenSourceButton.;




        //    "https://github.com/martijn00/XamarinMediaManager", "https://docs.microsoft.com/en-us/xamarin/xamarin-forms/"


        //    IList<string> urls = new List<string>();

        //    string[] url = { "https://github.com/martijn00/XamarinMediaManager", "https://docs.microsoft.com/en-us/xamarin/xamarin-forms/" };

        //    foreach (string strings in url)
        //    {
        //        urls.Add(strings);
        //    }

            //for (int i = 0; i < urls.Count; i++) { 
                
            //    //  // Inflate the tile
            //    var tile = LayoutInflater.Inflate(Resource.Layout.Tile, null);

            //    //    // Set its attributes
            //    //    tile.FindViewById<TextView>(Resource.Id.projectName).Text = currentProject;

            //    //  // Add the tile
            //    //  projectScrollView.AddView(tile);
                //}

                return Task.FromResult(urls);
        }
    }

        
}


       

            



