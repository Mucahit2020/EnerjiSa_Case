using AutoMapper;
using EnergyOutageNotifier.API.Extensions.SignalR;
using EnergyOutageNotifier.Business.Map;
using EnergyOutageNotifier.Business.Services.Abstracts;
using EnergyOutageNotifier.Business.Services.Concrates;
using EnergyOutageNotifier.Models.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IOutageService, OutageService>();

builder.Services.AddDbContext<EnerjisaDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("EnerjisaDBContext"),
        option => option.MigrationsAssembly("EnergyOutageNotifier.API")
    )
);
builder.Services.AddAutoMapper(typeof(ModelToResourceProfile));

//builder.Services.AddSignalR(o =>
//{
//    o.EnableDetailedErrors = true;
//});



builder.Services.AddCors(options => options.AddPolicy("AllowAll",
                   builder =>
                   {
                       builder.AllowAnyHeader()
                              .AllowAnyMethod()
                              .SetIsOriginAllowed((host) => true)
                              .AllowCredentials();

                   }));

builder.Services.AddSignalR();




var app = builder.Build();

app.UseCors(c => c.WithOrigins(new string[] { "http://localhost:4200" })
    .AllowAnyMethod()
    .AllowCredentials()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.UseRouting(); // UseRouting yöntemi ekleniyor

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/outageNotificationsAdd");
    endpoints.MapControllers();
});



app.Run();
