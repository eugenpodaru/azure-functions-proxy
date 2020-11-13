namespace Devlight.Azure.Functions.Proxy
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using static Resources;

    [StorageAccount(StorageAccountConnectionString)]
    public class ProxyFunctions
    {
        private readonly IProxyService _service;

        public ProxyFunctions(IProxyService service) => _service = service ?? throw new ArgumentNullException(nameof(service));

        [FunctionName(nameof(ProxyTrigger))]
        public async Task<HttpResponseMessage> ProxyTrigger(
            [HttpTrigger(AuthorizationLevel.Function, Route = "proxy/{name}/{version}/{**path}")] HttpRequestMessage request,
            string path,
            [Table(ServicesTable, "{name}", "{version}")] ServiceEntry service)
        {
            var uri = new Uri(new Uri(service.Host), path);

            request.Headers.Host = uri.Authority;
            request.RequestUri = uri;

            return await _service.SendAsync(request).ConfigureAwait(false);
        }
    }
}
