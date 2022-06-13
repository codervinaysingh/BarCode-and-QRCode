using BarcodeGenrater.Helper;
using BarcodeGenrater.Models;
using BarcodeLib;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace BarcodeGenrater.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QRCodeGenrater _qRCodeGenrater = new QRCodeGenrater();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
    public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string inputValue)
        {
           
            Response response = new Response();
            response.QrCode = _qRCodeGenrater.CreateQRCode(inputValue);
            bool isNumeric = decimal.TryParse(inputValue,out decimal result);
            if (isNumeric)
            {                           
                response.Data = _qRCodeGenrater.CreateBarCode(inputValue);
            }
            else
            {
                response.Status = "Only Integer Value";
                
            }
            return View(response);


        }
      
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}