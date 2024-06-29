WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.Build();

// Note that this is for demonstration only
// Using a List is not thread-safe and should not be used in 
// practice. Instead, use a database or a thread-safe data structure
var _people = new List<Person>
{
    new("Tom", "Hanks"),
    new("Denzel", "Washington"),
    new("Leonardo", "DiCaprio"),
    new("Al", "Pacino"),
    new("Morgan", "Freeman"),
};

app.MapGet("/person/{name}", (string name) => _people.Where(p => p.FirstName.StartsWith(name)));

app.Run();
record Person(string FirstName, string LastName);

