using System.Collections.Generic;

namespace ShopGiay.Models
{
    public class DM_SanPhamModel
    {
        public int ID_NhanHieu { get; set; }    
        public string TenSanPham { get; set; }
        public string GioiThieuSanPham { get; set; }
        public long GiaSanPham { get; set; }
        public long PhiVanChuyen { get; set; }
        public List<DM_ChiTietSanPham> listChiTietSanPham { get; set; }

        //DM_MoTaSanPham
        public string ThongTinMoTa { get; set; }

        //DM_ThongTinSanPham
        public string TinhTrangSanPham { get; set; }
        public string LoaiKhoaDay { get; set; }
        public string ChatLieu { get; set; }
        public string XuatXu { get; set; }
        public string ChieuCaoCoGiay { get; set; }
        public string DiaChiGuiHang { get; set; }


    }

    public class DM_ChiTietSanPham
    {
        public int ID_MauSac { get; set; }
        public int SoLuong { get; set; }
        public int Size { get; set; }

    }
}
