using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = "myroot"
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// for my web root (myroot)
app.UseStaticFiles();

// for another web root
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "mywebroot"))
});

app.UseRouting();

app.Map("/test", test);

//app.UseAuthorization();

//app.MapRazorPages();

app.Run();

static void test(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("test"); 
    }); 
}

