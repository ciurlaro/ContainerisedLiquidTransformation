using LiquidTransformationLib;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Enable file uploads
builder.Services.Configure<FormOptions>(o => o.MultipartBodyLengthLimit = 10_000_000);

builder.Services.AddCors();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapPost("/api/transform", async (HttpRequest req) =>
{
    var form = await req.ReadFormAsync();
    if (form.Files["template"] is not IFormFile tpl ||
        form.Files["content"] is not IFormFile json)
        return Results.BadRequest("Missing files");

    var root = string.IsNullOrWhiteSpace(form["rootElement"])
               ? null : form["rootElement"].ToString();

    using var tplReader  = new StreamReader(tpl.OpenReadStream());
    using var jsonReader = new StreamReader(json.OpenReadStream());

    var transformer = new Transformer(await tplReader.ReadToEndAsync());
    var result = transformer.RenderFromString(await jsonReader.ReadToEndAsync(), root);
    return Results.Ok(result);
});

app.Run();
