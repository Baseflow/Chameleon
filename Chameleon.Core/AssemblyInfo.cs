using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: Xamarin.Forms.XmlnsPrefix("http://baseflow.com/chameleon", "chameleon")]
[assembly: Xamarin.Forms.XmlnsDefinition("http://baseflow.com/chameleon", nameof(Chameleon.Core))]
[assembly: Xamarin.Forms.XmlnsDefinition("http://baseflow.com/chameleon", nameof(Chameleon.Core.Views))]
[assembly: Xamarin.Forms.XmlnsDefinition("http://baseflow.com/chameleon", nameof(Chameleon.Core.ViewModels))]

[assembly: Xamarin.Forms.XmlnsPrefix("http://baseflow.com/mediamanager", "mm")]
[assembly: Xamarin.Forms.XmlnsDefinition("http://baseflow.com/mediamanager", nameof(MediaManager.Forms))]
[assembly: Xamarin.Forms.XmlnsDefinition("http://baseflow.com/mediamanager", nameof(MediaManager.Forms.Xaml))]

[assembly: Xamarin.Forms.XmlnsPrefix("http://mvvmcross.com/bind", "mvx")]
[assembly: Xamarin.Forms.XmlnsDefinition("http://mvvmcross.com/bind", nameof(MvvmCross.Forms.Bindings))]
[assembly: Xamarin.Forms.XmlnsDefinition("http://mvvmcross.com/bind", nameof(MvvmCross.Forms.Converters))]
[assembly: Xamarin.Forms.XmlnsDefinition("http://mvvmcross.com/bind", nameof(MvvmCross.Forms.Views))]
