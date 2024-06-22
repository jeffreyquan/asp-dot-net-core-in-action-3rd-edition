WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.UseDeveloperExceptionPage(); // // can be omitted, added by WebApplication by default
app.UseStaticFiles();                   
app.UseRouting();                        
 
app.MapGet("/", () => "Hello World!");

app.Run();