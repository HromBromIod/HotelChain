using HotelChain.Service.IoC;

var builder = WebApplication.CreateBuilder(args);

ApplicationConfigurator.ConfigureServices(builder);

var app = builder.Build();

ApplicationConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();

app.Run();