var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", () => "Hello World");

app.MapGet("ok-object", () => Results.Ok(new
{
    Name = "Doða",
    Surname = "Koçak",
    Department = "Backend Developer"
}));

app.MapGet("request-limitation", async () =>
{
    await Task.Delay(5000);
    return Results.Ok(new
    {
        Name = "Doða",
        Surname = "Koçak",
        Department = "Backend Developer"
    });

});

app.MapGet("get", () => "This is a GET");
app.MapPost("post", () => "This is a POST");
app.MapPut("put", () => "This is a PUT");
app.MapPatch("patch", () => "This is a PATCH");
app.MapDelete("delete", () => "This is a DELETE");

app.MapMethods("options-or-head", new[] { "HEAD", "OPTIONS" }, 
    () => "Hello from either options HEAD or OPTIONS");

app.MapGet("get-params/{age:int}", (int age) =>
{
    return $"Age provided was {age}";
});

app.MapGet("cars/{CarPlate:regex(^\\d{{2}}\\s[A-Z]{{1,3}}\\s\\d{{3}}$)}", (string CarPlate) =>
{
    return $"Car plate provided was {CarPlate}";
});

app.MapGet("books/{isbn:length(13)}", (string isbn)=> 
{
    return $"ISBN provided was {isbn}";
});



app.Run();

