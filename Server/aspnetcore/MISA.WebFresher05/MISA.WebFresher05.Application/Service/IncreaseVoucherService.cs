using AutoMapper;
using MISA.WebFresher05.Domain;
using MISA.WebFresher05.Domain.Resources;

namespace MISA.WebFresher05.Application
{
    public class IncreaseVoucherService : BaseService<IncreaseVoucher, IncreaseVoucherDto, IncreaseVoucherCreateDto, IncreaseVoucherUpdateDto>, IIncreaseVoucherService
    {
        private readonly IIncreaseVoucherRepository _increaseVoucherRepository;
        private readonly IDetailIncreaseVoucherRepository _detailIncreaseVoucherRepository;

        private readonly IDetailIncreaseVoucherService _detailIncreaseVoucherService;
        private readonly IFixedAssetService _fixedAssetService;

        private readonly IIncreaseVoucherManager _increaseVoucherManager;


        public IncreaseVoucherService(IIncreaseVoucherRepository increaseVoucherRepository, IMapper mapper, IUnitOfWork unitOfWork, IDetailIncreaseVoucherRepository detailIncreaseVoucherRepository, IDetailIncreaseVoucherService detailIncreaseVoucherService, IFixedAssetService fixedAssetService, IIncreaseVoucherManager increaseVoucherManager) : base(increaseVoucherRepository, mapper, unitOfWork)
        {
            _increaseVoucherRepository = increaseVoucherRepository;
            _detailIncreaseVoucherRepository = detailIncreaseVoucherRepository;
            _detailIncreaseVoucherService = detailIncreaseVoucherService;
            _fixedAssetService = fixedAssetService;
            _increaseVoucherManager = increaseVoucherManager;
        }

        /// <summary>
        /// Chuyển đổi dữ liệu từ DTO sang Entity
        /// </summary>
        /// <param name="increaseVoucherCreateDto"></param>
        /// <returns></returns>
        /// Created by: vtahoang (23/08/2023) 
        public override async Task<IncreaseVoucher> MapCreateDtoToEntity(IncreaseVoucherCreateDto increaseVoucherCreateDto)
        {
            var increaseVoucher = _mapper.Map<IncreaseVoucher>(increaseVoucherCreateDto);

            // Kiểm tra mã tài sản đã tồn tại chưa
            await _increaseVoucherManager.CheckCodeExitsAsync(increaseVoucher.ref_no ?? "");

            increaseVoucher.ref_id = Guid.NewGuid();

            // Thêm thông tin tạo 
            increaseVoucher.modified_date = DateTime.Now;
            increaseVoucher.modified_by = "";
            increaseVoucher.created_date = DateTime.Now;
            increaseVoucher.created_by = "";

            return increaseVoucher;
        }

        /// <summary>
        /// Chuyển đổi dữ liệu từ DTO sang Entity
        /// </summary>
        /// <param name="increaseVoucherUpdateDto"></param>
        /// <returns></returns>
        /// Created by: vtahoang (23/08/2023) 
        public override async Task<IncreaseVoucher> MapUpdateDtoToEntity(IncreaseVoucherUpdateDto increaseVoucherUpdateDto)
        {
            var increaseVoucher = _mapper.Map<IncreaseVoucher>(increaseVoucherUpdateDto);

            // Kiểm tra mã tài sản đã tồn tại chưa
            await _increaseVoucherManager.CheckCodeExitsForUpdateAsync(increaseVoucher.ref_no ?? "", increaseVoucher.ref_id);

            // Thêm thông tin sửa 
            increaseVoucher.modified_date = DateTime.Now;
            increaseVoucher.modified_by = "";

            return increaseVoucher;
        }

        /// <summary>
        /// Tạo chứng từ ghi tăng mới
        /// </summary>
        /// <param name="increaseVoucherCreateDto">Chứng từ ghi tăng</param>
        /// <returns></returns>
        /// <exception cref="Domain.InvalidDataException"></exception>
        /// Created by: vtahoang (23/08/2023) 
        public override async Task CreateAsync(IncreaseVoucherCreateDto increaseVoucherCreateDto)
        {
            var voucher = await MapCreateDtoToEntity(increaseVoucherCreateDto);

            var listDetails = new List<DetailIncreaseVoucher>();

            var listFixedAssetUpdateDtos = new List<FixedAssetUpdateDto>();

            if (increaseVoucherCreateDto.Details == null)
            {
                throw new Domain.InvalidDataException(ResourcesVI.VoucherNullAssetsError);

            }

            foreach (var detail in increaseVoucherCreateDto.Details)
            {
                var detailIncreaseVoucher = _mapper.Map<DetailIncreaseVoucher>(detail.FixedAsset);

                detailIncreaseVoucher.ref_id = voucher.ref_id;
                detailIncreaseVoucher.ref_no = voucher.ref_no;
                detailIncreaseVoucher.ref_detail_id = Guid.NewGuid();

                if (detail.FixedAsset == null)
                {
                    throw new Domain.InvalidDataException(ResourcesVI.VoucherNullAssetsError);
                }

                listFixedAssetUpdateDtos.Add(detail.FixedAsset);
                listDetails.Add(detailIncreaseVoucher);

            }

            // Mở transaction
            await _uow.BeginTransactionAsync();

            try
            {
                // Cập nhật bảng sinh mã tự động
                await _increaseVoucherRepository.UpdateCodeAsync(voucher.GetCode());

                // Thêm chứng từ ghi tăng
                await _increaseVoucherRepository.CreateAsync(voucher);

                // Thêm chi tiết ghi tăng
                await _detailIncreaseVoucherService.InsertMany(listDetails);

                // Cập nhật tài sản
                await _fixedAssetService.UpdateManyAsync(listFixedAssetUpdateDtos);

                await _uow.CommitAsync();
            }
            catch (Exception)
            {
                await _uow.RollBackAsync();
                throw;
            }

        }

        /// <summary>
        /// Cập nhật chứng từ ghi tăng
        /// </summary>
        /// <param name="increaseVoucherUpdateDto">Chứng từ cập nhật</param>
        /// <returns></returns>
        /// <exception cref="Domain.InvalidDataException"></exception>
        public override async Task UpdateAsync(IncreaseVoucherUpdateDto increaseVoucherUpdateDto)
        {
            var voucher = await MapUpdateDtoToEntity(increaseVoucherUpdateDto);


            // Tách chi tiết ghi tăng thành 3 danh sách: thêm, sửa, xóa
            var listDetailsAdd = new List<DetailIncreaseVoucher>();
            var listDetailsUpdate = new List<DetailIncreaseVoucher>();
            var listDetailsDelete = new List<DetailIncreaseVoucher>();
            // Danh sách tài sản sửa 
            var listAssets = new List<FixedAssetUpdateDto>();

            if (increaseVoucherUpdateDto.Details != null)
            {
                foreach (var detail in increaseVoucherUpdateDto.Details)
                {
                    var detailIncreaseVoucher = _mapper.Map<DetailIncreaseVoucher>(detail.FixedAsset);

                    detailIncreaseVoucher.ref_id = voucher.ref_id;
                    detailIncreaseVoucher.ref_no = voucher.ref_no;
                    detailIncreaseVoucher.ref_detail_id = Guid.NewGuid();

                    if (detail.FixedAsset == null)
                    {
                        throw new Domain.InvalidDataException(ResourcesVI.VoucherNullAssetsError);
                    }

                    if (detail.EditMode != EditMode.Delete)
                    {
                        listAssets.Add(detail.FixedAsset);
                    }

                    switch (detail.EditMode)
                    {
                        case EditMode.Add:
                            listDetailsAdd.Add(detailIncreaseVoucher);
                            break;
                        case EditMode.Edit:
                            listDetailsUpdate.Add(detailIncreaseVoucher);
                            break;
                        case EditMode.Delete:
                            listDetailsDelete.Add(detailIncreaseVoucher);
                            break;
                    }
                }
            }
            else
            {
                throw new Domain.InvalidDataException(ResourcesVI.VoucherNullAssetsError);
            }

            // Mở transaction
            await _uow.BeginTransactionAsync();

            try
            {
                // Cập nhật chứng từ ghi tăng
                await _increaseVoucherRepository.UpdateAsync(voucher);

                // Thêm chi tiết ghi tăng
                if (listDetailsAdd.Count > 0)
                {
                    await _detailIncreaseVoucherRepository.CreateManyAsync(listDetailsAdd);
                }

                // Cập nhật chi tiết ghi tăng
                if (listDetailsUpdate.Count > 0)
                {
                    await _detailIncreaseVoucherRepository.UpdateManyAsyncByFixedAssetId(listDetailsUpdate);
                }

                // Xoá chi tiết ghi tăng
                if (listDetailsDelete.Count > 0)
                {
                    await _detailIncreaseVoucherService.DeleteManyByFixedAssetIdAsync(listDetailsDelete);
                }

                // Cập nhật tài sản
                await _fixedAssetService.UpdateManyAsync(listAssets);

                await _uow.CommitAsync();
            }
            catch (Exception)
            {
                await _uow.RollBackAsync();
                throw;
            }

        }

        /// <summary>
        /// Lấy chi tiết ghi tăng
        /// </summary>
        /// <param name="refId">Định danh chứng từ</param>
        /// <returns>Danh sách chi tiết</returns>
        /// Created by: vtahoang (24/08/2023) 
        public async Task<IEnumerable<DetailIncreaseVoucherDto>> GetDetailsAsync(Guid refId)
        {
            var details = await _detailIncreaseVoucherService.GetDetailIncreaseVouchersByMasterId(refId);

            return details;
        }

        /// <summary>
        /// Lấy mã sinh mặc định
        /// </summary>
        /// <returns></returns>
        /// Created by: vtahoang (24/08/2023) 
        public async Task<string?> GetDefaultCode()
        {
            return await _increaseVoucherRepository.DefaultCodeAsync();
        }

        /// <summary>
        /// Xoá chứng từ ghi tăng
        /// </summary>
        /// <param name="id">Id chứng từ</param>
        /// <returns></returns>
        public override async Task DeleteAsync(Guid id)
        {
            var voucher = await _increaseVoucherRepository.FindAsync(id);

            if (voucher == null)
            {
                throw new NotFoundException(ResourcesVI.NotFoundVoucher);
            }

            // Mở transaction
            await _uow.BeginTransactionAsync();
            try
            {
                // Xoá chi tiết chứng từ
                var details = new List<DetailIncreaseVoucher>();

                var detail = new DetailIncreaseVoucher
                {
                    ref_id = voucher.ref_id
                };

                details.Add(detail);

                await _detailIncreaseVoucherService.DeleteManyByMasterIdAsync(details);

                // Xoá chứng từ
                await _increaseVoucherRepository.DeleteAsync(voucher);

                await _uow.CommitAsync();
            }
            catch
            {
                await _uow.RollBackAsync();
                throw;
            }

        }

        /// <summary>
        /// Xoá nhiều chứng từ  ghi tăng
        /// </summary>
        /// <param name="ids">Danh sách chứng từ</param>
        /// <returns></returns>
        /// Created by: vtahoang (11/09/2023) 
        public override async Task DeleteManyAsync(List<Guid> ids)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                if (ids.Count == 0)
                {
                    throw new Exception(ResourcesVI.NullArrayError);
                }

                // Lấy danh sách chứng từ
                var vouchers = await _increaseVoucherRepository.GetListByIdsAsync(ids);

                if (vouchers.ToList().Count < ids.Count)
                {
                    throw new Exception(ResourcesVI.CantDelete);
                }

                // Xoá chi tiết chứng từ
                var details = new List<DetailIncreaseVoucher>();
                foreach (var voucher in vouchers)
                {
                    var detail = new DetailIncreaseVoucher
                    {
                        ref_id = voucher.ref_id
                    };
                    details.Add(detail);
                }
                await _detailIncreaseVoucherService.DeleteManyByMasterIdAsync(details);

                // Xoá chứng từ
                await _increaseVoucherRepository.DeleteManyAsync(vouchers);

                await _uow.CommitAsync();
            }
            catch (Exception)
            {
                await _uow.RollBackAsync();
                throw;
            }
        }
    }
}
