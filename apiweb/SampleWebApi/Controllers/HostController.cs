using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return Ok("Hello World!");
        }

        [HttpGet("name")]
        public IActionResult Name()
        {
            return Ok(DisplayLocalHostName());
        }

        [HttpGet("ip")]
        public IActionResult Ip()
        {
            return Ok(DoGetHostAddresses());
        }

        public string DisplayLocalHostName()
        {
            try
            {
               return Dns.GetHostName();
            }
            catch (SocketException e)
            {
                var builder = new StringBuilder();
                builder.Append("SocketException caught!!!");
                builder.Append("Source : " + e.Source);
                builder.Append("Message : " + e.Message);

                return builder.ToString();
            }
            catch (Exception e)
            {
                var builder = new StringBuilder();
                builder.Append("Exception caught!!!");
                builder.Append("Source : " + e.Source);
                builder.Append("Message : " + e.Message);

                return builder.ToString();
            }
        }

        public string DoGetHostAddresses()
        {
            try
            {
                var ipList = Dns.GetHostAddresses(Dns.GetHostName()).Select(address => address.ToString()).ToList();
                return string.Join(" ; ", ipList);
            }
            catch (SocketException e)
            {
                var builder = new StringBuilder();
                builder.Append("SocketException caught!!!");
                builder.Append("Source : " + e.Source);
                builder.Append("Message : " + e.Message);

                return builder.ToString();
            }
            catch (Exception e)
            {
                var builder = new StringBuilder();
                builder.Append("Exception caught!!!");
                builder.Append("Source : " + e.Source);
                builder.Append("Message : " + e.Message);

                return builder.ToString();
            }
        }
    }
}
