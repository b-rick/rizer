using rizer.Middleware;
using rizer.Model;
using rizer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddHttpClient();

var leagueConfiguration = new SleeperConfigurationModel();
builder.Configuration.GetSection("SleeperSettings").Bind(leagueConfiguration);
builder.Services.AddSingleton(leagueConfiguration);

var trawlerConfiguration = new TrawlConfigurationModel();
builder.Configuration.GetSection("TrawlerSettings").Bind(trawlerConfiguration);
builder.Services.AddSingleton(trawlerConfiguration);
builder.Services.AddSingleton<ICounterService, CounterService>();

var app = builder.Build();
// app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
