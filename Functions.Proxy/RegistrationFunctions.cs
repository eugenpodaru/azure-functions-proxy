namespace Devlight.Azure.Functions.Proxy
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using System;
    using System.Threading.Tasks;
    using static Resources;

    [StorageAccount(StorageAccountConnectionString)]
    public class RegistrationFunctions
    {
        [FunctionName(nameof(RegistrationTrigger))]
        public async Task<IActionResult> RegistrationTrigger([HttpTrigger(AuthorizationLevel.Function, "post", Route = "register")] ServiceEntry serviceEntry,
            [Table(ServicesTable)] IAsyncCollector<ServiceEntry> table)
        {
            serviceEntry.RegisteredAt = DateTime.UtcNow;

            await table.AddAsync(serviceEntry);

            return new OkObjectResult(serviceEntry);
        }
    }
}
