using Api.Extensions;
using Api.GlobalHandling;
using Application.ServiceInterface;
using Application.Services;
using Infra.IoC;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorizationPolicies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<FluentValidationGlobalExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandling>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();
static HttpContext? GetHttpContext(IHttpContextAccessor accessor)
{
    return accessor.HttpContext;
}
builder.Services.AddSingleton((Func<IServiceProvider, IUriService>)(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = GetHttpContext(accessor).Request;
    var uri = string.Concat((string)request.Scheme, "://", (string)request.Host.ToUriComponent());
    return new UriService(uri);
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder => builder
    .SetIsOriginAllowed(orign => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
app.UseExceptionHandler();
app.MapControllers();

app.Run();

