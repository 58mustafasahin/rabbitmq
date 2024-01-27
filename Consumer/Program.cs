using Consumer.Contexts;
using Consumer.SubServices;
using Consumer.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ConsumerDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));


var capConfig = builder.Configuration.GetSection("CapConfig").Get<CapConfig>();
builder.Services.AddCap(options =>
{
    options.UseEntityFramework<ConsumerDbContext>();
    options.UsePostgreSql(builder.Configuration.GetConnectionString("DbConnection"));
    options.UseRabbitMQ(options =>
    {
        options.ConnectionFactoryOptions = options =>
        {
            options.Ssl.Enabled = capConfig.SslEnable;
            options.HostName = capConfig.HostName;
            options.UserName = capConfig.UserName;
            options.Password = capConfig.Password;
            options.Port = capConfig.Port;
        };
    });
    options.UseDashboard(otp => { otp.PathMatch = "/MyCap"; });
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddTransient<CustomerSubServices>();

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
