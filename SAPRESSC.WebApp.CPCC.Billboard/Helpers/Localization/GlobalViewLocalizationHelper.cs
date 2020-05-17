using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Web.Manager.Controllers.Management;
using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Controllers.Management;

namespace SmartSolucionesCuba.SAPRESSC.WebApp.CPCC.Billboard.Helpers.Localization
{
    public class GlobalViewLocalizationHelper : IStringLocalizer<PresentationsController>, IStringLocalizer<ProductionsController>, IStringLocalizer<PersonalitiesController>, IStringLocalizer<EventsController>
    {
        private readonly IHtmlLocalizer htmlLocalizer;
        private readonly IStringLocalizer stringLocalizer;

        public GlobalViewLocalizationHelper(IHtmlLocalizerFactory htmlFactory, IStringLocalizerFactory stringFactory)
        {
            htmlLocalizer = htmlFactory.Create(typeof(SharedResources));
            stringLocalizer = stringFactory.Create(typeof(SharedResources));
        }

        public LocalizedString this[string name] => stringLocalizer[name];

        public LocalizedString this[string name, params object[] arguments] => stringLocalizer[name, arguments];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return stringLocalizer.GetAllStrings(includeParentCultures);
        }

        public string Localize(string key, IViewLocalizer viewLocalizer = null, params object[] arguments)
        {
            if (viewLocalizer != null)
            {
                var result = viewLocalizer[key, arguments];

                if (!result.IsResourceNotFound) return result.Value;
            }

            return htmlLocalizer[key, arguments].Value;
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return stringLocalizer.WithCulture(culture);
        }
    }
}
