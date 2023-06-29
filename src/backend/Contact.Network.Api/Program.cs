using System.Text.Json;
using System.Text.Json.Serialization;
using Contact.Network.Api.Contact;
using Marten;
using Marten.Services.Json;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMarten(options => {
    var connectionString = builder.Configuration.GetConnectionString("Marten")!;
    options.Connection(connectionString);
    
    options.UseDefaultSerialization(
        serializerType: SerializerType.SystemTextJson,
        enumStorage: EnumStorage.AsString,
        casing: Casing.CamelCase
    );

    if (builder.Environment.IsDevelopment()) {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
    
    options.Events.AddContactEventTypes();
    options.Events.AddTenantEventTypes();
}).UseLightweightSessions();

builder.Services
    .InstallContactService()
    .InstallTenantService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
