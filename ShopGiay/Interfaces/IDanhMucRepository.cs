using ShopGiay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopGiay.Interfaces
{
    public interface IDanhMucRepository
    {
        public Task<List<DM_TaiKhoanViewModel>> GetList_TaiKhoan();
        public Task<int> ThemMoiTaiKhoan(DM_TaiKhoanViewModel dM_TaiKhoanViewModel);
        public Task<int> CheckLoign(DM_TaiKhoanViewModel dM_TaiKhoanViewModel);

    }
}
