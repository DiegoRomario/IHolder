using IHolder.API;
using IHolder.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation()
                .AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

    //options.AddOpenBehavior(typeof(ValidationBehavior<,>));
    //options.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
