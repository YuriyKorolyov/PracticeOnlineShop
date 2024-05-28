using MyApp.Dto;
using MyApp.Models;

namespace MyApp.Interfaces
{
    public interface IViewHistoryRepository
    {
        Task<IEnumerable<ViewHistory>> GetViewHistoryByUserIdAsync(int userId);
        Task<ViewHistory> GetViewHistoryByIdAsync(int viewId);
        Task<int> GetCountViewHistoryOfAProductAsync(int prodId);
        Task<bool> ViewHistoryExistsAsync(int reviewId);
        Task<bool> CreateViewHistoryAsync(ViewHistory viewHistory);
        Task<bool> UpdateViewHistoryAsync(ViewHistory viewHistory);
        Task<bool> DeleteViewHistoryAsync(List<ViewHistory> viewHistories);
        Task<bool> SaveAsync();
    }
}
