using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails(); // Add the IProblemDetailsService

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}
app.UseStatusCodePages();

var _fruit = new ConcurrentDictionary<string, Fruit>();

app.MapGet("/", void () => throw new Exception("Demonstrating automatic ProblemDetails"));

app.MapGet("/fruit", () => _fruit);

app.MapGet("/fruit/{id}", (string id) =>
    _fruit.TryGetValue(id, out var fruit)
        ? TypedResults.Ok(fruit)
        : Results.NotFound()); // standard error converted to ProblemDetails

app.MapPost("/fruit/{id}", (string id, Fruit fruit) =>
    _fruit.TryAdd(id, fruit)
        ? TypedResults.Created($"/fruit/{id}", fruit)
        : Results.ValidationProblem(new Dictionary<string, string[]>
        {
            { "id", new[] { "A fruit with this id already exists" } }
        }));

app.MapPut("/fruit/{id}", (string id, Fruit fruit) =>
{
    _fruit[id] = fruit;
    return Results.NoContent();
});

app.MapDelete("/fruit/{id}", (string id) =>
{
    _fruit.TryRemove(id, out _);
    return Results.NoContent();
});

app.Run();
record Fruit(string Name, int Stock);