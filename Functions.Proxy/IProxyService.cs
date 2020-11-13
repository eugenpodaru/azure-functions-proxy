namespace Devlight.Azure.Functions.Proxy
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IProxyService
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
