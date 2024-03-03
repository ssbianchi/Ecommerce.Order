using AutoMapper;
using Ecommerce.Order.CrossCutting.Database;
using Ecommerce.Order.CrossCutting.Entity;
using Ecommerce.Order.CrossCutting.Enumeration;

namespace Ecommerce.Order.Application.Shared
{
    public class AbstractService
    {
        protected readonly IMapper _mapper;
        protected AbstractService(IMapper mapper)
        {
            _mapper = mapper;
        }
        protected async Task<M> SaveUpdateDelete<M>(M entity, IRepository<M> repository)
                where M : OperationEntity<int>
        {
            // Save / Change
            if ((entity.Id == 0 && entity.OperationId != (int)OperationEnum.Delete) ||
                entity.OperationId == (int)OperationEnum.HasChanges)
            {
                if (entity.Id == 0)
                    await repository.Save(entity);
                else
                    await repository.Update(entity);
            }

            // Delete
            else if (entity.Id > 0 && entity.OperationId == (int)OperationEnum.Delete)
            {
                await repository.Delete(entity);
            }

            return entity;
        }
        protected async Task<T> SaveUpdateDeleteDto<T, M>(T itemDto, IRepository<M> repository)
                where T : OperationEntity<int>
                where M : Entity<int>
        {
            M entity;

            // Save / Change
            if ((itemDto.Id == 0 && itemDto.OperationId != (int)OperationEnum.Delete) ||
                itemDto.OperationId == (int)OperationEnum.HasChanges)
            {
                entity = _mapper.Map<M>(itemDto);

                if (itemDto.Id == 0)
                    await repository.Save(entity);
                else
                    await repository.Update(entity);
            }

            // Delete
            else if (itemDto.Id > 0 && itemDto.OperationId == (int)OperationEnum.Delete)
            {
                entity = _mapper.Map<M>(itemDto);
                await repository.Delete(entity);
            }

            // Do nothing
            else
            {
                return itemDto;
            }

            return _mapper.Map<T>(entity);
        }
        protected async Task<List<T>> SaveRangeUpdateDeleteDto<T, M>(List<T> listDto, IRepository<M> repository)
                where T : OperationEntity<int>
                where M : Entity<int>
        {
            List<M> entity;

            // Save / Change
            if (listDto.Where(a => a.Id == 0 && a.OperationId != (int)OperationEnum.Delete ||
                a.OperationId == (int)OperationEnum.HasChanges).Count() > 0)
            {
                entity = _mapper.Map<List<M>>(listDto);

                if (listDto.Where(a => a.Id == 0).Count() > 0)
                    await repository.SaveRange(entity);
                else
                    await repository.UpdateRange(entity);
            }

            // Delete
            else if (listDto.Where(a => a.Id > 0 && a.OperationId == (int)OperationEnum.Delete).Count() > 0)
            {
                entity = _mapper.Map<List<M>>(listDto);
                await repository.DeleteRange(entity);
            }

            // Do nothing
            else
            {
                return listDto;
            }

            return _mapper.Map<List<T>>(entity);
        }
    }
}
