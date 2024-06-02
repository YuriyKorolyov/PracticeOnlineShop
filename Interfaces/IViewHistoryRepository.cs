using MyApp.Dto;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IViewHistoryRepository
    {
        IQueryable<ViewHistory> GetViewHistoryByUserId(int userId);
        Task<ViewHistory> GetViewHistoryByIdAsync(int viewId);
        Task<int> GetCountViewHistoryOfAProductAsync(int prodId);
        Task<bool> ViewHistoryExistsAsync(int reviewId);
        Task<bool> CreateViewHistoryAsync(ViewHistory viewHistory);
        Task<bool> UpdateViewHistoryAsync(ViewHistory viewHistory);
        Task<bool> DeleteViewHistoryAsync(List<ViewHistory> viewHistories);
        Task<bool> SaveAsync();
    }
}
