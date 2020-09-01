using HealthyGamerPortal.Common.ViewModels.LocalizationRecords;
using HealthyGamerPortal.WEB.Controllers.Base;
using HealthyGamerPortal.WEB.Interfaces;
using Microsoft.Extensions.Localization;
using Refit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;

namespace HealthyGamerPortal.WEB
{
    public class Localizer : BaseController, IStringLocalizer
    {
        private readonly CultureInfo _culture;
        private readonly List<LocalizationRecordViewModel> _LocalizationData;

        public Localizer()
        {
            _LocalizationData = new List<LocalizationRecordViewModel>();
            InitializeLocalizedStrings(_LocalizationData);
        }

        public Localizer(CultureInfo culture) : this()
        {
            _culture = culture;
        }

        /// <summary>
        /// This looks at the current culture and then translated the text from the cach, puts it in the name key
        /// </summary>
        public LocalizedString this[string name]
        {
            get
            {
                var culture = _culture ?? CultureInfo.CurrentUICulture;
                var translation = _LocalizationData.FirstOrDefault(x => x.LocalizationCulture == culture.Name && x.Key == name)?.Text;

                return new LocalizedString(name, translation ?? name, translation == null);
            }
        }

        /// <summary>
        /// This looks at the current culture and then translated the text from the cach with de naam and argument, puts it in the name key
        /// </summary>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var culture = _culture ?? CultureInfo.DefaultThreadCurrentUICulture;
                var translation = _LocalizationData.FirstOrDefault(x => x.LocalizationCulture == culture.Name && x.Key == name)?.Text;

                if (translation != null)
                {
                    translation = string.Format(translation, arguments);
                }

                return new LocalizedString(name, translation ?? name, translation == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all localisation with a culture
        /// </summary>
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new Localizer(culture);
        }

        /// <summary>
        /// This wil check if the cach Null is get a new localisationrecord from api en stores it in to localizedStrings
        /// </summary>
        private void InitializeLocalizedStrings(List<LocalizationRecordViewModel> localizedStrings)
        {
            localizedStrings.Clear();

            if (CachedCollectionClass.cachedCollection == null)
            {
                // error catch ?
                var api = RestService.For<IHealthyGamerLocalizationRecordsApi>(new HttpClient(new Helpers.AnonymousHttpClientHandler()) { BaseAddress = new Uri(BaseUrl) });
                var result = api.GetManyLocalizationRecords();
                if (result != null)
                {
                    CachedCollectionClass.cachedCollection = result.GetAwaiter().GetResult().Result;
                }
            }

            localizedStrings.AddRange(CachedCollectionClass.cachedCollection);
        }
    }

    /// <summary>
    ///  If cachedCollection is null then this wil be filled with api data from localisation record
    /// </summary>
    public static class CachedCollectionClass
    {
        public static List<LocalizationRecordViewModel> cachedCollection;
    }
}