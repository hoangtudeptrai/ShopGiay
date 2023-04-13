using Microsoft.AspNetCore.Http;
using ShopGiay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopGiay.Interfaces
{
    public interface ISanPhamRepository
    {
        public Task<int> ThemSanPham( DM_SanPhamModel obj);
        public Task<List<ImageViewModel>> UploadFile(IFormFileCollection file, int ID_SanPham);
        public Task<bool> LuuThongTinFile(List<ImageViewModel> list);


    }
}
