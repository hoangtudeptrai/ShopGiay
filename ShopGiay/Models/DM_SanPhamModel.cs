using System.Collections.Generic;

namespace ShopGiay.Models
{
    public class DM_SanPhamModel
    {
        public string TenSanPham { get; set; }
        public string GioiThieuSanPham { get; set; }
        public long GiaSanPham { get; set; }
        public long PhiVanChuyen { get; set; }
        public List<DM_ChiTietSanPham> listChiTietSanPham { get; set; }

    }

    public class DM_ChiTietSanPham
    {
        public int ID_MauSac { get; set; }
        public int SoLuong { get; set; }
        public int Size { get; set; }

    }
}
