using HealthyGamerPortal.Common.ViewModels.Guild;
using HealthyGamerPortal.Common.ViewModels.News;
using System.Collections.Generic;

namespace HealthyGamerPortal.Common.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<NewsItemShortViewModel> NewsItemShortList { get; set; }
        public List<GuildItemViewModel> GuildItemList { get; set; }
    }
}