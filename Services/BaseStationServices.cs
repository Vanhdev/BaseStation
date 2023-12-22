using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using BaseStation.Models;
using Newtonsoft.Json;
using System.Dynamic;

namespace BaseStation.Services
{
    public class BaseStationServices
    {
        public async Task<ExpandoObject> GetLocation(CellInfor cell)
        {
            using HttpClient client = new();
            try
            {
                var queryParameters = new Dictionary<string, string>
                    {
                        { "mnc", cell.GetMnc().ToString() },
                        { "mcc", cell.GetMcc().ToString() },
                        { "lac", cell.GetLac().ToString() },
                        { "cid", cell.GetCid().ToString() }
                    };

                var queryString = string.Join("&", queryParameters
                    .Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));

                string baseUrl = "https://api.findcellid.com/api/look_up";
                string urlWithParams = $"{baseUrl}?{queryString}";

                HttpResponseMessage response = await client.GetAsync(urlWithParams);
                response.EnsureSuccessStatusCode();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string body = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<ExpandoObject>(body);

                    return result;
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                string message = ex.Message;
                return null;
            }
        }
    }
}
