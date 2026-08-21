using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs.Follower;
using Core.Concretes.Entities;

namespace Business.Services
{
    public class FollowerService : IFollowerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public FollowerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateAsync(CreateFollowerDto dto)
        {
            var follower = mapper.Map<Follower>(dto);
            await unitOfWork.FollowerRepository.CreateOneAsync(follower);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await unitOfWork.FollowerRepository.DeleteOneAsync(id);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<FollowerDto>> GetFollowersAsync(Guid userId)
        {
            var followers = await unitOfWork.FollowerRepository.FindManyWithOrderedAsync(x => x.FollowedAt, false, x => x.FollowedMemberId.Equals(userId));
            return mapper.Map<IEnumerable<FollowerDto>>(followers);
        }

        public async Task<IEnumerable<FollowerDto>> GetFollowingsAsync(Guid userId)
        {
            var following = await unitOfWork.FollowerRepository.FindManyWithOrderedAsync(x => x.FollowedAt, false, x => x.FollowerMemberId.Equals(userId));
            return mapper.Map<IEnumerable<FollowerDto>>(following);
        }
    }
}
