using RMQ.Worker.Consumer;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var amq = new RMQ_Consumer();
app.MapGet("/", () => amq);

app.Run();
