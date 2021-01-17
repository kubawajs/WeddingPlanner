using AutoMapper;
using System;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain.Abstractions;
using WeddingPlanner.Infrastructure.Dto.Abstractions;
using WeddingPlanner.Infrastructure.Models;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public abstract class BaseOutfitService<TDto, TModel, TRepository> : IOutfitService<TDto>
        where TDto : BaseOutfitDto
        where TModel : BaseOutfit
        where TRepository : IOutfitRepository<TModel>
    {
        protected readonly IOutfitRepository<TModel> Repository;
        protected readonly IMapper Mapper;

        public BaseOutfitService(
            IOutfitRepository<TModel> repository,
            IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<OutfitResponse<TDto>> CreateOutfitAsync(TDto model)
        {
            try
            {
                var outfit = Mapper.Map<TModel>(model);
                if(outfit == null)
                {
                    throw new Exception($"{typeof(TDto).Name} cannot be null.");
                }

                await Repository.CreateAsync(outfit);
                return CreateSuccessResponse($"{typeof(TDto).Name} successfully created", model);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An error occured during {typeof(TDto).Name} creation: {ex.Message}");
            }
        }

        public virtual async Task<OutfitResponse<TDto>> GetOutfitAsync(int id)
        {
            try
            {
                var outfit = await Repository.GetAsync(id);
                if (outfit == null)
                {
                    return CreateErrorResponse($"Specified {typeof(TDto).Name} was not found. Id: {id}");
                }

                var model = Mapper.Map<TDto>(outfit);
                return CreateSuccessResponse($"{typeof(TDto).Name} successfully retrieved", model);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An error occured during {typeof(TDto).Name} creation: {ex.Message}");
            }
        }

        public virtual async Task<OutfitResponse<TDto>> UpdateOutfitAsync(TDto model)
        {
            try
            {
                var outfit = Mapper.Map<TModel>(model);
                await Repository.UpdateAsync(outfit);
                return CreateSuccessResponse($"{typeof(TDto).Name} outfit successfully updated", model);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An error occured during {typeof(TDto).Name} outfit update: {ex.Message}");
            }
        }

        protected virtual OutfitResponse<TDto> CreateSuccessResponse(string message, TDto item) 
            => new OutfitResponse<TDto>(BaseApiResponse<TDto>.CreateSuccessResponse(message, item));

        protected virtual OutfitResponse<TDto> CreateErrorResponse(string message)
            => new OutfitResponse<TDto>(BaseApiResponse<TDto>.CreateErrorResponse(message));
    }
}
