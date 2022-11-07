using Ste.Framework;
using Ste.User.Application;
using Ste.User.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();//
builder.Services.AddFrameworkServices(builder.Configuration);//
builder.Services.AddApplicationServices();//
builder.Services.AddInfrastructureServices(builder.Configuration);//
builder.Services.AddSwaggerGen(options =>
    { options.CustomSchemaIds(type => type.FullName?.Replace("+", "_").Split('.').Last().Replace("_", ".")); });//

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
ConfigureApp.Init(app);//
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
