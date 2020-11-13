namespace Devlight.Azure.Functions.Proxy
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class ProxyService : IProxyService
    {
        private readonly HttpClient _client;

        public ProxyService(HttpClient client) => _client = client ?? throw new ArgumentNullException(nameof(client));

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => SendAsync(request, CancellationToken.None);
    }
}
