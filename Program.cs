using System.Security.Cryptography.X509Certificates;

namespace MyCertAuthClient
{
    class Program
    {
        private const string ApiUrl = "https://localhost:7008/WeatherForecast";

        static async Task Main(string[] args)
        {
            var cert = new X509Certificate2(@"C:\Users\Lee\Dev\Learning\ChatGPT\ClientCertificateAuth\ExternalResources\client.pfx", "CaseyPo0h", X509KeyStorageFlags.MachineKeySet);

            Console.WriteLine($"(cert == null): {cert == null}");
            Console.WriteLine($"(cert)........: {cert}");

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert);
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator; // For testing purposes only!

            using var client = new HttpClient(handler);
            try
            {
                var response = await client.GetAsync(ApiUrl);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {response.StatusCode}");
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
