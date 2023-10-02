using AutoMapper;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher05.Domain.Resources;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Base service có thể thực hiện các thao tác cơ bản với dữ liệu như thêm, sửa, xóa
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TEntityCreateDto"></typeparam>
    /// <typeparam name="TEntityUpdateDto"></typeparam>
    /// Created by: vtahoang (19/07/2023)
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadOnlyService<TEntity, TEntityDto>, IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IUnitOfWork _uow;

        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
            _uow = unitOfWork;
        }

        /// <summary>
        /// Tạo bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Bản ghi tạo</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public virtual async Task CreateAsync(TEntityCreateDto entityCreateDto)
        {
            var entity = await MapCreateDtoToEntity(entityCreateDto);

            if (entity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.modified_date = DateTime.Now;
                baseAuditEntity.modified_by = "";
                baseAuditEntity.created_date = DateTime.Now;
                baseAuditEntity.created_by = "";
            }

            // Mở transaction
            await _uow.BeginTransactionAsync();

            try
            {
                // Thực hiện thêm bản ghi
                await _baseRepository.CreateAsync(entity);

                // Cập nhật lại mã tự động
                if (entity is IHasCode hasCodeEntity)
                {
                    await _baseRepository.UpdateCodeAsync(hasCodeEntity.GetCode());
                }
                await _uow.CommitAsync();
            }
            catch (Exception)
            {
                await _uow.RollBackAsync();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Bản ghi cập nhật</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public virtual async Task UpdateAsync(TEntityUpdateDto entityUpdateDto)
        {
            var entity = await MapUpdateDtoToEntity(entityUpdateDto);

            if (entity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.modified_date = DateTime.Now;
                baseAuditEntity.modified_by = "";
            }

            await _baseRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Xoá bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi cần xoá</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);

            await _baseRepository.DeleteAsync(entity);
        }

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id bản ghi cần xoá</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                if (ids.Count == 0)
                {
                    throw new Exception(ResourcesVI.NullArrayError);
                }

                var entities = await _baseRepository.GetListByIdsAsync(ids);

                if (entities.ToList().Count < ids.Count)
                {
                    throw new Exception(ResourcesVI.CantDelete);
                }

                await _baseRepository.DeleteManyAsync(entities);

                await _uow.CommitAsync();
            }
            catch (Exception)
            {
                await _uow.RollBackAsync();
                throw;
            }
        }

        /// <summary>
        /// Chuyển từ create dto sang entity
        /// </summary>
        /// <param name="entityCreateDto">Dữ liệu dto</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public abstract Task<TEntity> MapCreateDtoToEntity(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Chuyển từ update dto sang entity
        /// </summary>
        /// <param name="entityUpdateDto">Dữ liệu dto</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public abstract Task<TEntity> MapUpdateDtoToEntity(TEntityUpdateDto entityUpdateDto);

    }
}
