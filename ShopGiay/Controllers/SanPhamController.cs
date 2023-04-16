using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopGiay.Helps;
using ShopGiay.Interfaces;
using ShopGiay.Repositorys;
using System.Threading.Tasks;
using System;
using ShopGiay.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShopGiay.Controllers
{
    public class SanPhamController : ControllerBase
    {
        private readonly ILogger<SanPhamController> _logger;
        private readonly ISanPhamRepository _sanPhamRepository;


        public SanPhamController(
           ILogger<SanPhamController> logger,
           ISanPhamRepository sanPhamRepository
           )
        {
            _logger = logger;
            _sanPhamRepository = sanPhamRepository;
        }

        [HttpPost("ThemSanPham")]
        public async Task<IActionResult> ThemSanPham([FromBody] DM_SanPhamModel obj)
        {
            try
            {
                var result = await _sanPhamRepository.ThemSanPham(obj);

                return Ok(new { value= result, flag = true});

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("save_attachment")]
        public async Task<IActionResult> UploadAttachment(int ID_SanPham)
        {
            try
            {
                var file = HttpContext.Request.Form.Files;
                if (file.Count == 0)
                    return Ok(new { flag = false, msg = "Không tìm thấy thông tin file !" });

                foreach (var item in file)
                {
                    if (item.Length > 250 * 1024 * 1024)
                        return Ok(new { flag = false, msg = "File không được vượt quá 250MB !" });
                }


                var list = await _sanPhamRepository.UploadFile(file, ID_SanPham);

                var result_savefile = await _sanPhamRepository.LuuThongTinFile(list);

                return Ok(new { flag = result_savefile, msg = result_savefile ? "Lưu thông tin file thành công" : "Lưu thông tin file thất bại !" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetList_SanPham")]
        public async Task<IActionResult> GetList_SanPham()
        {
            try
            {
                List<TrangChuSanPhamViewModel> result = await _sanPhamRepository.GetList_SanPham();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet("ThongTinChiTietSanPham")]
        public async Task<IActionResult> ThongTinChiTietSanPham(int ID_SanPham)
        {
            try
            {

                var result = await _sanPhamRepository.ThongTinChiTietSanPham(ID_SanPham);


                return Ok(new {
                    flag = true, msg = "", 
                    thongtincoban = JsonConvert.SerializeObject(result.Tables[0]),
                    mausac_size = JsonConvert.SerializeObject(result.Tables[1]), 
                    img = JsonConvert.SerializeObject(result.Tables[2]), 
                    xuatsugiay = JsonConvert.SerializeObject(result.Tables[3]), 
                    danhgiasanpham = JsonConvert.SerializeObject(result.Tables[4])
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
