using AutoMapper;
using MISA.WebFresher05.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher05.Application
{
    /// <summary>
    /// Base service chỉ đọc
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// Created by: vtahoang (19/07/2023) 
    public abstract class BaseReadOnlyService<TEntity, TEntityDto> : IBaseReadOnlyService<TEntityDto>
    {
        protected readonly IBaseReadOnlyRepository<TEntity> _readOnlyRepository;
        protected readonly IMapper _mapper;

        protected BaseReadOnlyService(IBaseReadOnlyRepository<TEntity> readOnlyRepository, IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: vtahoang (19/07/2023) 
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var entities = await _readOnlyRepository.GetAllAsync();

            var entitieDtos = _mapper.Map<IEnumerable<TEntityDto>>(entities);

            return entitieDtos;
        }

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await _readOnlyRepository.GetAsync(id);

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        /// <summary>
        /// Tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        public async Task<TEntityDto?> FindAsync(Guid id)
        {
            var entity = await _readOnlyRepository.FindAsync(id);

            var entityDto = _mapper.Map<TEntityDto?>(entity);

            return entityDto;
        }

        /// <summary>
        /// Lấy tổng số bản ghi.
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (23/07/2023) 
        public async Task<int> GetTotalRecordsAsync()
        {
            var totalRecords = await _readOnlyRepository.GetTotalRecordsAsync();

            return totalRecords;
        }

        /// <summary>
        /// Lấy bản ghi theo danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: vtahoang (22/08/2023) 
        public async Task<IEnumerable<TEntityDto>> GetListByIdsAsync(List<Guid> ids)
        {
            var entities = await _readOnlyRepository.GetListByIdsAsync(ids);
            var entityDtos = _mapper.Map<IEnumerable<TEntityDto>>(entities);

            return entityDtos;
        }


        /// <summary>
        /// Lấy tổng số bản ghi theo bộ lọc
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns>Tổng số bản ghi</returns>
        /// Created by: vtahoang (31/07/2023) 
        public async Task<int> GetTotalRecordsFilterAsync(FilterObject filterObject)
        {
            var totalRecords = await _readOnlyRepository.GetTotalRecordsFilterAsync(filterObject);

            return totalRecords;
        }

        /// <summary>
        /// Danh sách bản ghi theo bộ lọc
        /// </summary>
        /// <param name="filterObject">Bộ lọc</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: vtahoang (31/07/2023) 
        public async Task<IEnumerable<TEntityDto>> GetFilterAsync(FilterObject filterObject)
        {
            var entities = await _readOnlyRepository.GetFilterAsync(filterObject);

            var entityDtos = _mapper.Map<IEnumerable<TEntityDto>>(entities);

            return entityDtos;
        }
    }
}
