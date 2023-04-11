using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopGiay.Interfaces;

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
    }
}
