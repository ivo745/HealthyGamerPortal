using HealthyGamerPortal.Common.ViewModels.News;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Business.Interfaces
{
    public interface INewsService
    {
        /// <summary>
        /// Gets a <see cref="IEnumerable{NewsItemViewModel}"/> of short messages
        /// </summary>
        /// <returns> <see cref="IEnumerable{NewsItemViewModel}"/></returns>
        Task<IEnumerable<NewsItemViewModel>> GetManyShortNews();

        /// <summary>
        /// Gets a <see cref="IEnumerable{NewsItemViewModel}"/>
        /// </summary>
        /// <returns><see cref="IEnumerable{NewsItemViewModel}"/></returns>
        Task<IEnumerable<NewsItemViewModel>> GetManyNews();

        /// <summary>
        /// Gets a single <see cref="NewsItemViewModel"/>
        /// </summary>
        /// <param name="NewsitemId"></param>
        /// <returns> <see cref="NewsItemViewModel"/></returns>
        Task<NewsItemViewModel> GetSingleNewsById(Guid id);

        /// <summary>
        /// Creates Single NewsItem in Database from <see cref="CreateNewsItemViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns>see cref="bool"/></returns>
        Task<bool> CreateSingleNews(CreateNewsItemViewModel model);

        /// <summary>
        /// Edits Single NewsItem in Database from <see cref="EditNewsItemViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> EditSingleNews(EditNewsItemViewModel model);

        /// <summary>
        /// Deletes Single NewsItem From Database by <see cref="DeleteNewsItemViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> DeleteSingleNews(DeleteNewsItemViewModel model);
    }
}
