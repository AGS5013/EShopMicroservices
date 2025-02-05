


using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

//add services to the container

builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();

//configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => {

});


app.Run();
