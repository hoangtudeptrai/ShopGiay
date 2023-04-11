using Microsoft.Extensions.Logging;
using ShopGiay.Context;
using ShopGiay.Interfaces;

namespace ShopGiay.Repositorys
{
    public class SanPhamRepositorycs : ISanPhamRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<SanPhamRepositorycs> _logger;
        private readonly string uploadpath = "/Files/Upload/FileDinhKem";

        public SanPhamRepositorycs(DapperContext context, ILogger<SanPhamRepositorycs> logger)
        {
            _context = context;
            _logger = logger;

        }
    }
}
