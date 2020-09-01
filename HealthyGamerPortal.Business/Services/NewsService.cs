using AutoMapper;
using HealthyGamerPortal.Business.Interfaces;
using HealthyGamerPortal.Common.ViewModels.News;
using HealthyGamerPortal.Data;
using HealthyGamerPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Business.Services
{
    public class NewsService : INewsService
    {
        private readonly IMapper _mapper;
        private readonly HealthyGamerPortalDbContext _healthyGamerPortalDbContext;

        /// <summary>
        /// Create a new instance of the <see cref="NewsService"/> with the needed dependencies.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/> interface, for automatically mapping different objects to each other.</param>
        /// <param name="healthyGamerPortalDbContext"></param>
        public NewsService(IMapper mapper, HealthyGamerPortalDbContext healthyGamerPortalDbContext)
        {
            _mapper = mapper;
            _healthyGamerPortalDbContext = healthyGamerPortalDbContext;
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{NewsItemViewModel}"/> of short messages
        /// </summary>
        /// <returns> <see cref="IEnumerable{NewsItemViewModel}"/></returns>
        public async Task<IEnumerable<NewsItemViewModel>> GetManyShortNews()
        {
            var newsItemList = await _healthyGamerPortalDbContext.NewsItem.ToListAsync();
            var manyNewsList = new List<NewsItemViewModel>();
            foreach (var newsItem in newsItemList)
            {
                if (newsItem.Item.Length > 50)
                {
                    newsItem.Item = newsItem.Item.Substring(0, 50);
                }
                manyNewsList.Add(_mapper.Map<NewsItemViewModel>(newsItem));
            }
            return manyNewsList;
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{NewsItemViewModel}"/>
        /// </summary>
        /// <returns><see cref="IEnumerable{NewsItemViewModel}"/></returns>
        public async Task<IEnumerable<NewsItemViewModel>> GetManyNews()
        {
            var newsItemList = await _healthyGamerPortalDbContext.NewsItem.ToListAsync();
            var manyNewsList = new List<NewsItemViewModel>();
            foreach (var newsItem in newsItemList)
            {
                manyNewsList.Add(_mapper.Map<NewsItemViewModel>(newsItem));
            }
            return manyNewsList;
        }

        /// <summary>
        /// Gets a single <see cref="NewsItemViewModel"/>
        /// </summary>
        /// <param name="NewsitemId"></param>
        /// <returns> <see cref="NewsItemViewModel"/></returns>
        public async Task<NewsItemViewModel> GetSingleNewsById(Guid NewsitemId)
        {
            return _mapper.Map<NewsItemViewModel>(await _healthyGamerPortalDbContext.NewsItem.FirstAsync(I => I.Id == NewsitemId));
        }

        /// <summary>
        /// Creates Single NewsItem in Database from <see cref="CreateNewsItemViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns>see cref="bool"/></returns>
        public async Task<bool> CreateSingleNews(CreateNewsItemViewModel model)
        {
            var newsItem = _mapper.Map<CreateNewsItemViewModel, NewsItem>(model);
            _healthyGamerPortalDbContext.NewsItem.Add(newsItem);
            await _healthyGamerPortalDbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Edits Single NewsItem in Database from <see cref="EditNewsItemViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        public async Task<bool> EditSingleNews(EditNewsItemViewModel model)
        {
            if (await _healthyGamerPortalDbContext.NewsItem.AsNoTracking().FirstOrDefaultAsync(i => i.Id == model.Id) != null)
            {
                var newsItem = _mapper.Map<EditNewsItemViewModel, NewsItem>(model);
                _healthyGamerPortalDbContext.Update(newsItem);
                await _healthyGamerPortalDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes Single NewsItem From Database by <see cref="DeleteNewsItemViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/></returns>
        public async Task<bool> DeleteSingleNews(DeleteNewsItemViewModel model)
        {
            var existingNewsItem = await _healthyGamerPortalDbContext.NewsItem.AsNoTracking().FirstOrDefaultAsync(i => i.Id == model.Id);
            if (existingNewsItem != null)
            {
                _healthyGamerPortalDbContext.Remove(existingNewsItem);
                await _healthyGamerPortalDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
