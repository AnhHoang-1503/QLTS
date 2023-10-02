using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher05.Application;
using MISA.WebFresher05.Controllers.Base;
using MISA.WebFresher05.Domain;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher05.Controllers
{
    /// <summary>
    /// Controller cho tài sản
    /// </summary>
    /// Created by: vtahoang (19/07/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetController : BaseController<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {
        private readonly IFixedAssetService _fixedAssetService;

        public FixedAssetController(IFixedAssetService fixedAssetService) : base(fixedAssetService)
        {
            _fixedAssetService = fixedAssetService;
        }

        /// <summary>
        /// Trả về mã code mặc định
        /// </summary>
        /// <returns>Mã tài sản mặc định</returns>
        /// Created by: vtahoang (19/07/2023) 
        [HttpGet("default-code")]
        public async Task<IActionResult> DefaultCodeAsync()
        {
            var defaultCode = await _fixedAssetService.DefaultCodeAsync();

            return StatusCode(StatusCodes.Status200OK, defaultCode);
        }

        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <param name="filterJson">Bộ lọc</param>
        /// <returns></returns>
        /// Created by: vtahoang (01/08/2023) 
        [HttpGet("excel")]
        public async Task<IActionResult> ExportToExcel([FromQuery] string filterJson, [FromQuery] string excelOptionsJson)
        {
            var filterObject = JsonSerializer.Deserialize<FilterObject>(filterJson);
            var excelOptions = JsonSerializer.Deserialize<ExcelOptions>(excelOptionsJson);

            if (filterObject == null || excelOptions == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }


            var fileBytes = await _fixedAssetService.ExportToExcel(filterObject, excelOptions);
            var excelName = excelOptions.FileName;
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        /// <summary>
        /// Nhập dữ liệu từ file excel
        /// </summary>
        /// <param name="file">File excel</param>
        /// <returns></returns>
        /// Created by: vtahoang (01/08/2023) 
        [HttpPost("excel")]
        public async Task<IActionResult> ImportFromExcel(IFormFile file)
        {
            using var stream = new MemoryStream();

            await file.CopyToAsync(stream);

            var fileBytes = stream.ToArray();

            await _fixedAssetService.ImportFromExcel(fileBytes);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// File mẫu nhập liệu
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (01/08/2023) 
        [HttpGet("excel/example")]
        public async Task<IActionResult> ExampleFileExcel()
        {
            var fileBytes = await _fixedAssetService.ExampleFileExcel();
            var excelName = "Mẫu nhập liệu.xlsx";

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        /// <summary>
        /// Lấy tổng tài sản chưa ghi tăng theo bộ lọc
        /// </summary>
        /// <param name="filterJson">Bộ lọc</param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023) 
        [HttpGet("total-records/unincrease")]
        public async Task<IActionResult> GetTotalUnincrease([FromQuery] string filterJson)
        {
            var filterObject = JsonSerializer.Deserialize<FilterObject>(filterJson);

            if (filterObject != null)
            {
                var totalRecords = await _fixedAssetService.GetTotalUnIncrease(filterObject);

                return StatusCode(StatusCodes.Status200OK, totalRecords);
            }
            else return StatusCode(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Lấy tài sản chưa ghi tăng theo bộ lọc
        /// </summary>
        /// <param name="filterJson">Bộ lọc</param>
        /// <returns></returns>
        /// Created by: vtahoang (27/08/2023) 
        [HttpGet("filter/unincrease")]
        public async Task<IActionResult> GetFixedAssetsUnincrease([FromQuery] string filterJson)
        {
            var filterObject = JsonSerializer.Deserialize<FilterObject>(filterJson);

            if (filterObject != null)
            {
                var totalRecords = await _fixedAssetService.GetFixedAssetsUnIncrease(filterObject);

                return StatusCode(StatusCodes.Status200OK, totalRecords);
            }
            else return StatusCode(StatusCodes.Status404NotFound);
        }
    }
}
