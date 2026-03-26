var builder = WebApplication.CreateBuilder(args)
     .AddPIHCMPlatform();

var app = builder.Build();

app.UsePIHCMPlatform();
app.Run();
