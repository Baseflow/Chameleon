using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chameleon.Services;
using MvvmCross.IoC;
using MvvmCross.Plugin.JsonLocalization;

namespace Chameleon.Core
{
    public class TextProviderBuilder : MvxTextProviderBuilder
    {
        public string CurrentLocalization = AppSettings.DefaultAppLanguage;

        public TextProviderBuilder(string language = null) : base(AppSettings.TextProviderNamespace, "Resources", new MvxEmbeddedJsonDictionaryTextProvider(true))
        {
            if (!string.IsNullOrEmpty(language))
                CurrentLocalization = language;
        }

        public override void LoadResources(string whichLocalizationFolder)
        {
            if (!string.IsNullOrEmpty(whichLocalizationFolder))
            {
                CurrentLocalization = whichLocalizationFolder;
            }
            base.LoadResources(CurrentLocalization);
        }

        protected override IDictionary<string, string> ResourceFiles
        {
            get
            {
                var dictionary = GetType().GetTypeInfo()
                                     .Assembly
                                     .CreatableTypes()
                                     .Where(t => t.Name.EndsWith("ViewModel"))
                                     .ToDictionary(t => t.Name, t => t.Name);

                dictionary.Add("Shared", "Shared");

                return dictionary;
            }
        }
    }
}
