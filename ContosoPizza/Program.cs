using ContosoPizza.Services; // make sure this matches your SalesReportnamespace
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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
app.UseAuthorization();
app.MapControllers();

// ---------------------
// Generate Sales Summary
// ---------------------
try
{
    // Path to the folder containing individual sales files
    string salesFolder = Path.Combine(Directory.GetCurrentDirectory(), "sales");
    Directory.CreateDirectory(salesFolder); // ensure the folder exists

    // Output file for the sales summary
    string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "SalesSummary.txt");

    // Call the SalesReport function
    SalesReport.GenerateSalesSummary(salesFolder, outputFile);

    Console.WriteLine($"Sales summary generated at: {outputFile}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error generating sales summary: {ex.Message}");
}

app.Run();
