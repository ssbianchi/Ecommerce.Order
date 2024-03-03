using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Ecommerce.Order.API.Filter;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Ecommerce.Order.Repository;
using Ecommerce.Order.Application;
using Ecommerce.Order.CrossCutting;

var builder = WebApplication.CreateBuilder(args);
var builderConfig = builder.Configuration;
#if !DEBUG
 
Environment.SetEnvironmentVariable("AZURE_TENANT_ID", builderConfig["KeyVault:TenantId"]);
 
// Add Key Vaults from Azure to appsettings configuration
builder.Host.ConfigureAppConfiguration((context, config) =>
{
    var credential = new ChainedTokenCredential(/*new EnvironmentCredential(),*/ new ManagedIdentityCredential(), new AzureCliCredential());
    var secretClient = new SecretClient(new Uri(builderConfig["KeyVault:Url"]), credential);
    config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
});
 
#else
// Add Key Vaults from Azure to appsettings configuration
builder.Host.ConfigureAppConfiguration((context, config) =>
{
    var url = builderConfig["KeyVault:Url"];
    var tenantId = builderConfig["KeyVault:TenantId"];
    var clientId = builderConfig["KeyVault:ClientId"];
    var clientSecret = builderConfig["KeyVault:ClientSecret"];
    //var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
    //var client = new SecretClient(new Uri(url), credential);
    //config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
});
#endif

// Add an ActionFilter which takes care of a standarised Exception message.
builder.Services.AddControllers(c => c.Filters.Add<HttpExceptionFilter>())
                .ConfigureApiBehaviorOptions(s => s.SuppressModelStateInvalidFilter = true);

// Suppress NULL fields in returned Json
builder.Services.AddMvc().AddJsonOptions(options =>
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Ecommerce Order API",
        Description = "An ASP.NET Core Web API to acess diverse services of the Ecommerce Order.",
        Contact = new OpenApiContact { Name = "Ecommerce", Url = new Uri("https://www.ecommerce.com"), Email = "ecommerce@ecommerce.com" },
    });
});

// Register the layers config
builder.Services.RegisterRepository(builderConfig["EcommerceOrderDatabase"]);
builder.Services.RegisterApplication();
builder.Services.RegisterCrossCutting(builderConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseAuthorization();

app.MapControllers();

app.Run();
