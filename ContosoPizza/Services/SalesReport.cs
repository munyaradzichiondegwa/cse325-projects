using System;
using System.IO;
using System.Text; 
using System.Collections.Generic;
using ContosoPizza.Models;

namespace ContosoPizza.Services;

public static class SalesReport
{
    public static void GenerateSalesSummary(string inputDirectory, string outputFile)
    {
        var salesFiles = Directory.GetFiles(inputDirectory, "*.txt");

        decimal totalSales = 0;
        var salesDetails = new Dictionary<string, decimal>();

        foreach (var file in salesFiles)
        {
            var content = File.ReadAllText(file);

            if(decimal.TryParse(content, out decimal saleAmount))
            {
                salesDetails.Add(Path.GetFileName(file), saleAmount);
                totalSales += saleAmount;
            }
        }

        // Build the report
        var report = new StringBuilder();

        report.AppendLine("Sales Summary");
        report.AppendLine("----------------------------");
        report.AppendLine($" Total Sales: {totalSales:C}");
        report.AppendLine();
        report.AppendLine(" Details:");

        foreach (var sale in salesDetails)
        {
            report.AppendLine($"  {sale.Key}: {sale.Value:C}");
        }

        File.WriteAllText(outputFile, report.ToString());
    }
}
