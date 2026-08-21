using Core.Concretes.DTOs.Follower;

namespace Core.Abstracts.IServices
{
    public interface IFollowerService
    {
        Task CreateAsync(CreateFollowerDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<FollowerDto>> GetFollowersAsync(Guid userId);
        Task<IEnumerable<FollowerDto>> GetFollowingsAsync(Guid userId);
    }
}
