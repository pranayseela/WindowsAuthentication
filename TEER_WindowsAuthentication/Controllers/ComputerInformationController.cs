using System;
using System.Net;
using System.Net.Sockets;
using System.Web.Mvc;
using System.Management;


namespace TEER_WindowsAuthentication.Controllers
{
    public class ComputerInformationController : Controller
    {
        // GET: ComputerInformation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewComputerInformation()
        {
            ViewBag.Message = "Your View Computer Information page.";
            ViewBag.ipAddress = GetLocalIPAddress();
            ViewBag.GetComputerName = GetComputerName();
            ViewBag.GetMACAddress = GetMACAddress();
            return View();
        }
        private dynamic GetMACAddress()
        {
            //throw new NotImplementedException();
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            string MACAddress = String.Empty;
            foreach (ManagementObject mo in moc)
            {
                if (MACAddress == String.Empty)
                {
                    if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
                }
                mo.Dispose();
            }

            MACAddress = MACAddress.Replace(":", "");
            return MACAddress;
        }
        private dynamic GetComputerName()
        {
            //throw new NotImplementedException();
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            String info = String.Empty;
            foreach (ManagementObject mo in moc)
            {
                info = (string)mo["Name"];
                //mo.Properties["Name"].Value.ToString();
                //break;
            }
            return info;
        }
        private dynamic GetLocalIPAddress()
        {
            //throw new NotImplementedException();
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