using AutoMapper;
using MISA.WebFresher05.Domain;

namespace MISA.WebFresher05.Application
{
    public class DetailIncreaseVoucherService : BaseService<DetailIncreaseVoucher, DetailIncreaseVoucherDto, DetailIncreaseVoucherCreateDto, DetailIncreaseVoucherUpdateDto>, IDetailIncreaseVoucherService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDetailIncreaseVoucherRepository _detailIncreaseVoucherRepository;


        /// <summary>
        /// 
        /// </summary>
        private readonly IDetailIncreaseVoucherManager _detailIncreaseVoucherManager;
        public DetailIncreaseVoucherService(IDetailIncreaseVoucherRepository detailIncreaseVoucherRepository, IMapper mapper, IUnitOfWork unitOfWork, IDetailIncreaseVoucherManager detailIncreaseVoucherManager) : base(detailIncreaseVoucherRepository, mapper, unitOfWork)
        {
            _detailIncreaseVoucherRepository = detailIncreaseVoucherRepository;
            _detailIncreaseVoucherManager = detailIncreaseVoucherManager;
        }

        /// <summary>
        /// Xoá nhiều chi tiết ghi tăng theo mã tài sản
        /// </summary>
        /// <param name="detailIncreaseVouchers">Danh sách chi tiết xoá</param>
        /// <returns></returns>
        /// Created by: vtahoang (05/09/2023) 
        public async Task DeleteManyByFixedAssetIdAsync(IEnumerable<DetailIncreaseVoucher> detailIncreaseVouchers)
        {
            // Xoá chi tiết tài sản
            await _detailIncreaseVoucherRepository.DeleteManyAsync(detailIncreaseVouchers, "fixed_asset_id");
        }

        /// <summary>
        /// Xoá nhiều chi tiết ghi tăng theo chứng từ
        /// </summary>
        /// <param name="detailIncreaseVouchers">Danh sách chi tiết xoá</param>
        /// <returns></returns>
        /// Created by: vtahoang (05/09/2023) 
        public async Task DeleteManyByMasterIdAsync(IEnumerable<DetailIncreaseVoucher> detailIncreaseVouchers)
        {
            // Xoá chi tiết tài sản
            await _detailIncreaseVoucherRepository.DeleteManyAsync(detailIncreaseVouchers, "ref_id");
        }

        /// <summary>
        /// Lấy chi tiết ghi tăng theo chứng từ
        /// </summary>
        /// <param name="masterId">Định danh chứng từ</param>
        /// <returns>Chi tiết</returns>
        /// Created by: vtahoang (24/08/2023) 
        public async Task<IEnumerable<DetailIncreaseVoucherDto>> GetDetailIncreaseVouchersByMasterId(Guid masterId)
        {
            var detailIncreaseVouchers = await _detailIncreaseVoucherRepository.GetDetailIncreaseVouchersByMasterId(masterId);

            var detailIncreaseVoucherDtos = _mapper.Map<IEnumerable<DetailIncreaseVoucherDto>>(detailIncreaseVouchers);

            return detailIncreaseVoucherDtos;
        }

        /// <summary>
        /// Tạo nhiều chi tiết ghi tăng
        /// </summary>
        /// <param name="detailIncreaseVouchers">Danh sách chi tiết ghi tăng</param>
        /// <returns></returns>
        /// Created by: vtahoang (29/08/2023) 
        public async Task InsertMany(IEnumerable<DetailIncreaseVoucher> detailIncreaseVouchers)
        {
            var fixedAssetIds = detailIncreaseVouchers.Select(x => x.fixed_asset_id).ToList();

            await _detailIncreaseVoucherManager.CheckExistListFixedAsset(fixedAssetIds);

            await _detailIncreaseVoucherRepository.CreateManyAsync(detailIncreaseVouchers);
        }

        /// <summary>
        /// Chuyển đổi chi tiết ghi tăng từ DTO sang Entity
        /// </summary>
        /// <param name="detailIncreaseVoucherCreateDto">Dto</param>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        public override Task<DetailIncreaseVoucher> MapCreateDtoToEntity(DetailIncreaseVoucherCreateDto detailIncreaseVoucherCreateDto)
        {
            var detailIncreaseVoucher = _mapper.Map<DetailIncreaseVoucher>(detailIncreaseVoucherCreateDto);

            return Task.FromResult(detailIncreaseVoucher);
        }

        /// <summary>
        /// Chuyển đổi từ DTO sang Entity
        /// </summary>
        /// <param name="detailIncreaseVoucherUpdateDto"></param>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        public override Task<DetailIncreaseVoucher> MapUpdateDtoToEntity(DetailIncreaseVoucherUpdateDto detailIncreaseVoucherUpdateDto)
        {
            var detailIncreaseVoucher = _mapper.Map<DetailIncreaseVoucher>(detailIncreaseVoucherUpdateDto);

            return Task.FromResult(detailIncreaseVoucher);
        }

    }
}
