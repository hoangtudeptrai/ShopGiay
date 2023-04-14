using Dapper;
using Microsoft.Extensions.Logging;
using ShopGiay.Context;
using ShopGiay.Interfaces;
using ShopGiay.Models;
using System.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ShopGiay.Repositorys
{
    public class SanPhamRepositorycs : ISanPhamRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<SanPhamRepositorycs> _logger;
        private readonly IHostingEnvironment _env;

        private readonly string uploadpath = "/Files/Upload/FileDinhKem";

        public SanPhamRepositorycs(DapperContext context, ILogger<SanPhamRepositorycs> logger, IHostingEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }

        public async Task<int> ThemSanPham(DM_SanPhamModel obj)
        {
            try
            {
                int ID_Sanpham = 0;
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {

                        #region Thêm bản ghi bảng DM_SanPham
                        string procedureName = "TuDH_ThemMoiSanPham";
                        var parameters = new DynamicParameters();
                        parameters.Add("ThongTinMoTa", obj.ThongTinMoTa, DbType.String, ParameterDirection.Input);

                        parameters.Add("TinhTrangSanPham", obj.TinhTrangSanPham, DbType.String, ParameterDirection.Input);
                        parameters.Add("LoaiKhoaDay", obj.LoaiKhoaDay, DbType.String, ParameterDirection.Input);
                        parameters.Add("ChatLieu", obj.ChatLieu, DbType.String, ParameterDirection.Input);
                        parameters.Add("XuatXu", obj.XuatXu, DbType.String, ParameterDirection.Input);
                        parameters.Add("ChieuCaoCoGiay", obj.ChieuCaoCoGiay, DbType.String, ParameterDirection.Input);
                        parameters.Add("DiaChiGuiHang", obj.DiaChiGuiHang, DbType.String, ParameterDirection.Input);

                        parameters.Add("ID_NhanHieu", obj.ID_NhanHieu, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("TenSanPham", obj.TenSanPham, DbType.String, ParameterDirection.Input);
                        parameters.Add("GioiThieuSanPham", obj.GioiThieuSanPham, DbType.String, ParameterDirection.Input);
                        parameters.Add("GiaSanPham", obj.GiaSanPham, DbType.Int32, ParameterDirection.Input);
                        parameters.Add("PhiVanChuyen", obj.PhiVanChuyen, DbType.Int32, ParameterDirection.Input);
                        int result = await connection.ExecuteScalarAsync<int>
                                        (procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                        ID_Sanpham = result;
                        #endregion

                        #region Thêm bản ghi bảng DM_ChitietSanPham
                        if (result > 0)
                        {
                            foreach (var item in obj.listChiTietSanPham)
                            {
                                string procedureName_BaoGia = "TUDH_Insert_DM_ChiTietSanPham";
                                var parameters_BaoGia = new DynamicParameters();
                                parameters_BaoGia.Add("ID_SanPham", result, DbType.Int32, ParameterDirection.Input);
                                parameters_BaoGia.Add("ID_MauSac", item.ID_MauSac, DbType.Int32, ParameterDirection.Input);
                                parameters_BaoGia.Add("Size", item.Size, DbType.Int32, ParameterDirection.Input);
                                parameters_BaoGia.Add("SoLuong", item.SoLuong, DbType.Int32, ParameterDirection.Input);

                                long them = await connection.ExecuteScalarAsync<long>
                                    (procedureName_BaoGia, parameters_BaoGia, commandType: CommandType.StoredProcedure, transaction: transaction);
                            }
                        }
                        #endregion
                        transaction.Commit();
                    }

                    return ID_Sanpham;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Lỗi Save: " + ex.Message);
                throw new ArgumentException("TuDH_ThemMoiSanPham", ex);
            }
        }

        public async Task<List<ImageViewModel>> UploadFile(IFormFileCollection file, int ID_SanPham)
        {
            try
            {
                List<ImageViewModel> list = new List<ImageViewModel>();

                foreach (var file_upload in file)
                {
                    string fileName = file_upload.FileName;
                    string ticks = DateTime.Now.Ticks.ToString();
                    fileName = fileName.Trim();
                    fileName = fileName.Replace(" ", "_");
                    string extention = fileName.Substring(fileName.LastIndexOf('.') + 1);
                    fileName = fileName.Substring(0, fileName.LastIndexOf('.')).Replace('.', '_').ToLower();
                    string path_date = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
                    string folder_upload = _env.ContentRootPath + uploadpath + "/" + path_date;

                    if (!Directory.Exists(folder_upload))
                        Directory.CreateDirectory(folder_upload);

                    string path_file_upload = folder_upload + @"\" + fileName + ticks + "." + extention;
                    using (var fileStream = new FileStream(path_file_upload, FileMode.Create))
                    {
                        await file_upload.CopyToAsync(fileStream);
                    };

                    ImageViewModel attachment = new ImageViewModel();
                    attachment.URL = "HoSo_FileDinhKem/" + path_date + fileName + ticks + "." + extention;
                    attachment.ID_SanPham = ID_SanPham;
                    list.Add(attachment);
                }

                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError("UploadFile hồ sơ fail " + ex.Message);
                throw new ArgumentException("UploadFile", ex);
            }
        }
        public async Task<bool> LuuThongTinFile(List<ImageViewModel> list)
        {
            try
            {
                var procedureName = "TuDH_ThemAnh";
                int count = 0;
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (ImageViewModel obj in list)
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("ID_SanPham", obj.ID_SanPham, DbType.Int32, ParameterDirection.Input);
                            parameters.Add("URL", obj.URL, DbType.String, ParameterDirection.Input);

                            long result = await connection.ExecuteScalarAsync<long>
                                (procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                            if (result > 0)
                                count++;
                        }
                        transaction.Commit();
                    }
                    if (count == 0)
                        return false;
                    else
                        return count == list.Count;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TuDH_ThemAnh " + ex.Message);
                throw new ArgumentException("TuDH_ThemAnh", ex);
            }
        }
    }
}
