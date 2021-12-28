using csharp_package.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace csharp_package.Controllers
{
    public class HomeController : Controller
    {
        private string p_token = "your-payro-token";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public ActionResult Payment()
        {
            Payro.PayroIPG payro = new Payro.PayroIPG(p_token, true);
            Payro.PayroCreated? payroCreatedResponse = payro.payment("https://localhost:7186/Home/Confirm", "123", 20000, "ali", "ali@yahoo.com", "09121111111", "");
            if (payroCreatedResponse == null)
            {
                Payro.PayroMessage error = payro.getError();
                return View(error);
            }
            else
            {
                return Redirect(payroCreatedResponse.link);
            }
            
        }

        [HttpPost]
        public ActionResult Confirm(IFormCollection collection)
        {
            if (collection.ContainsKey("id"))
            {
                Payro.PayroIPG payro = new Payro.PayroIPG(p_token, true);
                Payro.PayroPaymentInfo? payroVerifyResponse = payro.verify(collection["id"], "123");
                if (payroVerifyResponse == null)
                {
                    Payro.PayroMessage error = payro.getError();
                    return View(error);
                }
                else
                {
                    Payro.PayroMessage message = new Payro.PayroMessage();
                    message.error_code = "Status: " + payroVerifyResponse.status;
                    message.error_message = "Payment Track ID: " + payroVerifyResponse.payment.track_id;
                    return View(message);
                }
            }
            else
            {
                Payro.PayroMessage message = new Payro.PayroMessage();
                message.error_code = "55";
                message.error_message = "Not Valid!";
                return View(message);
            }
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