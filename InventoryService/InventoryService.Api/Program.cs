using InventoryService.Application.Common.Interfaces;
using InventoryService.Infrastructure.Kafka.Consumers;
using InventoryService.Infrastructure.Persistence;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

builder.Services.AddHostedService<ProductCreatedConsumer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();