using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ATOZBussinessLayer.APIServices.APILocationServices
{
    public static class LocationService
    {
        static async Task<double> GetDrivingDistance(double lat1, double lon1, double lat2, double lon2)
        {
            string url = $"https://router.project-osrm.org/route/v1/driving/{lon1},{lat1};{lon2},{lat2}?overview=false";

            using var client = new HttpClient();
            var response = await client.GetStringAsync(url);
            using var doc = JsonDocument.Parse(response);

            double distance = doc.RootElement
                .GetProperty("routes")[0]
                .GetProperty("distance")
                .GetDouble();

            return distance / 1000; // بالكيلومتر
        }

    }
}
