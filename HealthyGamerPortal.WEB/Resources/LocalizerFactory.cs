using Microsoft.Extensions.Localization;
using System;

namespace HealthyGamerPortal.WEB
{
    public class LocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            return new Localizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new Localizer();
        }
    }
}