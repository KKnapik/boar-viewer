using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Web.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private static async Task<HttpResponseMessage> CallApi(string baseUri, string requestUri)
        {
            using var client = new HttpClient {BaseAddress = new Uri(baseUri)};
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetAsync(requestUri);
        }

        private static dynamic DeserializeResult(HttpResponseMessage res)
        {
            var result = res.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject(result);
        }

        public async Task<IActionResult> ShowFood()
        {
            try
            {
                var boarList = new List<string>();
                var apiAppName = Environment.GetEnvironmentVariable("APIFOOD_URL");
                var res = await CallApi($"http://{apiAppName}", "/food");
                if (!res.IsSuccessStatusCode) return View("ApiProblemView");

                foreach (JObject obj in DeserializeResult(res))
                {
                    boarList.Add(obj["name"].ToString());
                }

                return View(boarList);

            }
            catch (Exception ex)
            {
                return View("ExceptionView", ex);
            }
        }

        public async Task<IActionResult> ShowBoars()
        {
            try
            {
                var foodList = new List<string>();
                var apiAppName = Environment.GetEnvironmentVariable("APIBOAR_URL");
                var res = await CallApi($"http://{apiAppName}", "/boar");
                if (!res.IsSuccessStatusCode) return View("ApiProblemView");

                foreach (JObject obj in DeserializeResult(res))
                {
                    foodList.Add(obj["name"].ToString());
                }
                return View(foodList);

            }
            catch (Exception ex)
            {
                return View("ExceptionView", ex);
            }
        }
    }
}