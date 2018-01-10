using System;
using System.Net;
using System.Net.Sockets;
using System.Web.Mvc;

namespace TEER_WindowsAuthentication.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewRegister()
        {
            string ipAddress = GetLocalIPAddress();
            ViewBag.ipAddress = ipAddress;
            return View();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}