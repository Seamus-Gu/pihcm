var builder = WebApplication.CreateBuilder(args)
     .AddPIHCMPlatform(options =>
     {
         options.EnableSqlSugar = false;
     });

var app = builder.Build();

app.UsePIHCMPlatform();
app.Run();
