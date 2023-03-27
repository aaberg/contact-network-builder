using Contact.Network.Domain.Contact;
using Contact.Network.Service;
using Contact.Network.Service.Contact;
using Marten;
using Marten.Events.Projections;
using Marten.Services.Json;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
    
    options.Events.AddEventTypes(new []{
        typeof(Events.ContactCreated),
        typeof(Events.ContactRenamed),
        typeof(Events.ContactMarkedAsDeleted),
        typeof(Events.CompanySpecified),
        typeof(Events.CompanyRemoved),
        typeof(Events.JobTitleSpecified),
        typeof(Events.JobTitleRemoved),
        typeof(Events.BirthDaySpecified),
        typeof(Events.BirthDayRemoved),
        typeof(Events.PhoneNumberAdded),
        typeof(Events.EmailAdded),
    });
});

builder.Services.AddScoped<IApplicationService<Contact.Network.Domain.Contact.Contact>, ContactService>();

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
