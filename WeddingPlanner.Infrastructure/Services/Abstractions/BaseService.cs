using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using WeddingPlanner.Core.Domain.Abstractions;
using WeddingPlanner.Infrastructure.Dto.Abstractions;
using WeddingPlanner.Infrastructure.Models.Abstractions;
using WeddingPlanner.Infrastructure.Repository.Abstractions;

namespace WeddingPlanner.Infrastructure.Services.Abstractions
{
    public abstract class BaseService<TDto, TModel, TRepository, TResponse> : IBaseService<TDto, TResponse>
        where TDto : IDto
        where TModel : BaseModel
        where TRepository : IBaseRepository<TModel>
        where TResponse : BaseApiResponse<TDto>
    {
        protected readonly TRepository Repository;
        protected readonly IMapper Mapper;

        public BaseService(
            TRepository repository,
            IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<TResponse> CreateAsync(TDto dtoModel)
        {
            try
            {
                var model = Mapper.Map<TModel>(dtoModel);
                if(model == null)
                {
                    throw new Exception($"{typeof(TDto).Name} cannot be null.");
                }

                await Repository.CreateAsync(model);
                return CreateSuccessResponse($"{typeof(TDto).Name} successfully created", dtoModel);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse($"An error occured during {typeof(TDto).Name} creation: {ex.Message}");
            }
        }

        public virtual async Task<TResponse> GetAsync(int id)
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

        public virtual async Task<TResponse> UpdateAsync(TDto model)
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

        protected abstract TResponse CreateSuccessResponse(string message, TDto item);
        protected abstract TResponse CreateErrorResponse(string message);
    }
}
