using Microsoft.AspNetCore.Http;
using ShopGiay.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ShopGiay.Interfaces
{
    public interface ISanPhamRepository
    {
        public Task<int> ThemSanPham( DM_SanPhamModel obj);
        public Task<List<ImageViewModel>> UploadFile(IFormFileCollection file, int ID_SanPham);
        public Task<bool> LuuThongTinFile(List<ImageViewModel> list);
        public Task<List<TrangChuSanPhamViewModel>> GetList_SanPham();

        public Task<DataSet> ThongTinChiTietSanPham(int ID_SanPham);





    }
}
