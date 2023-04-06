using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopGiay.Interfaces;
using ShopGiay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ShopGiay.Context;
using Microsoft.EntityFrameworkCore;
using ShopGiay.Helps;

namespace ShopGiay.Controllers
{
    public class DanhMucController: ControllerBase
    {
        private readonly ILogger<DanhMucController> _logger;
        private readonly IDanhMucRepository _danhMucRepository;


        public DanhMucController(
           ILogger<DanhMucController> logger,
           IDanhMucRepository danhMucRepository
           )
        {
            _logger = logger;
            _danhMucRepository = danhMucRepository;
        }

        [HttpGet("GetList_TaiKhoan")]
        public async Task<IActionResult> GetList_TaiKhoan()
        {
            try
            {
                List<DM_TaiKhoanViewModel> result = await _danhMucRepository.GetList_TaiKhoan();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("ThemMoiTaiKhoan")]
        public async Task<IActionResult> ThemMoiTaiKhoan([FromBody] DM_TaiKhoanViewModel dM_TaiKhoanViewModel)
        {
            try
            {
                dM_TaiKhoanViewModel.MatKhau = PassWorkHasher.MD5Pass(dM_TaiKhoanViewModel.MatKhau);

                var result = await _danhMucRepository.ThemMoiTaiKhoan(dM_TaiKhoanViewModel);
                
                if(result == 0)
                    return Ok(new { flag = false, msg = "Đăng kí không thành công", value = result });

                if (result == -1)
                    return Ok(new { flag = false, msg = "Tên đăng nhập đã tồn tại", value = result });

                return Ok(new { flag = true, msg = "Đăng kí thành công", value = result });
                `
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("CheckDanhNhap")]
        public async Task<IActionResult> CheckLoign([FromBody] DM_TaiKhoanViewModel dM_TaiKhoanViewModel)
        {
            try
            {
                dM_TaiKhoanViewModel.MatKhau = PassWorkHasher.MD5Pass(dM_TaiKhoanViewModel.MatKhau);

                var result = await _danhMucRepository.CheckLoign(dM_TaiKhoanViewModel);

                if (result == 0)
                    return Ok(new { flag = false, msg = "Đăng nhập không thành công", value = result });

                return Ok(new { flag = true, msg = "Đăng nhập thành công", value = result });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
            //tuDepTrai
        }
    }
}
