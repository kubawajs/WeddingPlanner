using System.Threading.Tasks;
using WeddingPlanner.Api.Models.Abstractions;
using WeddingPlanner.Core.Services;
using WeddingPlanner.Infrastructure.Dto.Abstractions;

namespace WeddingPlanner.Api.Services.Abstractions
{
    public interface IBaseService<TDto, TResponse> : IService
        where TDto : IDto
        where TResponse : BaseApiResponse<TDto>
    {
        Task<TResponse> CreateAsync(TDto model);
        Task<TResponse> UpdateAsync(TDto model);
        Task<TResponse> GetAsync(int id);
    }
}
