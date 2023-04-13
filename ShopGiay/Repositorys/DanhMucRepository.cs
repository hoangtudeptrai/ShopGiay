using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using ShopGiay.Context;
using ShopGiay.Interfaces;
using ShopGiay.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ShopGiay.Repositorys
{
    public class DanhMucRepository : IDanhMucRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<DanhMucRepository> _logger;
        private readonly string uploadpath = "/Files/Upload/FileDinhKem";

        public DanhMucRepository(DapperContext context, ILogger<DanhMucRepository> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<int> CheckLoign(string TaiKhoan, string MatKhau)
        {
            try
            {
                var procedureName = "TuDH_DM_TaiKhoan_CheckDangNhap";
                var parameters = new DynamicParameters();
                using (var connection = _context.CreateConnection())
                {
                    parameters.Add("TaiKhoan",TaiKhoan, DbType.String, ParameterDirection.Input);
                    parameters.Add("MatKhau", MatKhau, DbType.String, ParameterDirection.Input);
                    var result = await connection.ExecuteScalarAsync<int>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TuDH_DM_TaiKhoan_CheckDangNhap", ex);
                throw new ArgumentException("TuDH_DM_TaiKhoan_CheckDangNhap", ex);
            }
        }

        public async Task<int> DoiMatKhau(string TaiKhoan, string MatKhauCu, string MatKhauMoi, int ID_TaiKhoan)
        {
            try
            {
                var procedureName = "TuDH_DoiMatKhau";
                var parameters = new DynamicParameters();
                using (var connection = _context.CreateConnection())
                {
                    parameters.Add("ID_TaiKhoan", ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("TaiKhoan", TaiKhoan, DbType.String, ParameterDirection.Input);
                    parameters.Add("MatKhauCu", MatKhauCu, DbType.String, ParameterDirection.Input);
                    parameters.Add("MatKhauMoi", MatKhauMoi, DbType.String, ParameterDirection.Input);
                    var result = await connection.ExecuteScalarAsync<int>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TuDH_Insert_TaiKhoan", ex);
                throw new ArgumentException("TuDH_Insert_TaiKhoan", ex);
            }
        }

        public async Task<List<DM_TaiKhoanViewModel>> GetList_TaiKhoan()
        {
            try
            {
                var procedureName = "TuDH_DM_TaiKhoan_GetList";
                var parameters = new DynamicParameters();
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<DM_TaiKhoanViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 150);

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TuDH_DM_TaiKhoan_GetList", ex);
                throw new ArgumentException("TuDH_DM_TaiKhoan_GetList", ex);
            }
        }

        public async Task<int> ThemMoiTaiKhoan(DM_TaiKhoanViewModel dM_TaiKhoanViewModel)
        {
            try
            {
                var procedureName = "TuDH_Insert_TaiKhoan";
                var parameters = new DynamicParameters();
                using (var connection = _context.CreateConnection())
                {
                    parameters.Add("TaiKhoan", dM_TaiKhoanViewModel.TaiKhoan, DbType.String, ParameterDirection.Input);
                    parameters.Add("MatKhau", dM_TaiKhoanViewModel.MatKhau, DbType.String, ParameterDirection.Input);
                    parameters.Add("Ho", dM_TaiKhoanViewModel.Ho, DbType.String, ParameterDirection.Input);
                    parameters.Add("Ten", dM_TaiKhoanViewModel.Ten, DbType.String, ParameterDirection.Input);
                    parameters.Add("Email", dM_TaiKhoanViewModel.Email, DbType.String, ParameterDirection.Input);
                    parameters.Add("SoDienThoai", dM_TaiKhoanViewModel.SDT, DbType.String, ParameterDirection.Input);
                    parameters.Add("DiaChi", dM_TaiKhoanViewModel.DiaChi, DbType.String, ParameterDirection.Input);
                    parameters.Add("Role", dM_TaiKhoanViewModel.Role, DbType.Int32, ParameterDirection.Input);
                    var result = await connection.ExecuteScalarAsync<int>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TuDH_Insert_TaiKhoan", ex);
                throw new ArgumentException("TuDH_Insert_TaiKhoan", ex);
            }
        }

    }
}
