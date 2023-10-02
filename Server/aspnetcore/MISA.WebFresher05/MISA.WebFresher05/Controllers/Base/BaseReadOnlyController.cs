using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher05.Application;
using MISA.WebFresher05.Domain;
using System.Text.Json;

namespace MISA.WebFresher05
{
    /// <summary>
    /// Base controller cho các controller chỉ đọc
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// Created by: vtahoang (19/07/2023) 
    public abstract class BaseReadOnlyController<TEntityDto> : ControllerBase
    {
        private readonly IBaseReadOnlyService<TEntityDto> _baseReadOnlyService;

        protected BaseReadOnlyController(IBaseReadOnlyService<TEntityDto> baseReadOnlyService)
        {
            _baseReadOnlyService = baseReadOnlyService;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _baseReadOnlyService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// Created by: vtahoang (19/07/2023) 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _baseReadOnlyService.GetAsync(id);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (24/07/2023) 
        [HttpGet("total-records")]
        public virtual async Task<IActionResult> GetTotalRecordsAsync()
        {
            var totalRecords = await _baseReadOnlyService.GetTotalRecordsAsync();

            return StatusCode(StatusCodes.Status200OK, totalRecords);
        }

        /// <summary>
        /// Lấy danh sách bản ghi theo ids
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns></returns>
        /// Created by: vtahoang (22/08/2023) 
        [HttpGet("list-id")]
        public virtual async Task<IActionResult> GetListByIds([FromQuery] string idsJson)
        {
            try
            {
                var ids = JsonSerializer.Deserialize<List<Guid>>(idsJson);
                if (ids != null)
                {
                    var result = await _baseReadOnlyService.GetListByIdsAsync(ids);
                    return StatusCode(StatusCodes.Status200OK, result);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Không tìm thấy dữ liệu");

            }
            catch
            {
                return StatusCode(StatusCodes.Status404NotFound, "Không tìm thấy dữ liệu");
            }
        }

        /// <summary>
        /// Tổng số bản ghi theo bộ lọc
        /// </summary>
        /// <param name="filterJson">Bộ lọc dạng json</param>
        /// <returns></returns>
        /// Created by: vtahoang (31/07/2023) 
        [HttpGet("total-records/filter")]
        public async Task<IActionResult> GetTotalRecordsFilterAsync([FromQuery] string filterJson)
        {
            var filterObject = JsonSerializer.Deserialize<FilterObject>(filterJson);

            if (filterObject != null)
            {
                var totalRecords = await _baseReadOnlyService.GetTotalRecordsFilterAsync(filterObject);

                return StatusCode(StatusCodes.Status200OK, totalRecords);
            }
            else return StatusCode(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Danh sách bản ghi theo bộ lọc
        /// </summary>
        /// <param name="filterJson">Bộ lọc dạng json</param>
        /// <returns></returns>
        /// Created by: vtahoang (31/07/2023) 
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync([FromQuery] string filterJson)
        {
            var filterObject = JsonSerializer.Deserialize<FilterObject>(filterJson);

            if (filterObject != null)
            {
                var entityDtos = await _baseReadOnlyService.GetFilterAsync(filterObject);
                return StatusCode(StatusCodes.Status200OK, entityDtos);
            }
            else return StatusCode(StatusCodes.Status404NotFound);
        }

    }
}
