using RMQ.Worker.Publisher;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var amq = new RMQ_Publisher();
app.MapGet("/", () => amq);

app.Run();
