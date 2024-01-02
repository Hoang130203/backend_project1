using project1_backend.Models;
using project1_backend.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowAllOrigins", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ProjectBongDaContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();  
var app = builder.Build();

app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseRouting(); // Đảm bảo rằng UseRouting() được gọi trước UseEndpoints()
/*
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<PaymentHub>("/payment");
    // Các endpoint khác...
});
*/
//app.UseRouting(); // Đảm bảo rằng UseRouting() được gọi trước UseEndpoints()

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<PaymentHub>("/payment");
    // Các endpoint khác...
});
app.MapControllers();

app.Run();
