using System.Diagnostics;
using OfficeOpenXml;
using VkMarketParser.Core.VkMarket;

namespace VkMarketParser.Core.ResultWriter;

public class ExcelResultWriter : IResultWriter<Product>
{
    public async Task WriteAsync(IEnumerable<Product> products, string fullFileName)
    {
        if (Path.GetDirectoryName(fullFileName) is { } dir && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
        
        using (var ep = new ExcelPackage())
        {
            var row = 2;
            var worksheet = ep.Workbook.Worksheets.Add("Sheet1");
            
            AddHeaders(worksheet);

            foreach (var product in products)
            {
                worksheet.Cells[row, 1].Value = product.Name;
                worksheet.Cells[row, 2].Value = product.Link;
                worksheet.Cells[row, 3].Value = product.Description;
                worksheet.Cells[row, 4].Value = string.Join(",", product.Images);
                
                row++;
            }

            var file = new FileInfo(fullFileName);
            await ep.SaveAsAsync(file);
        }

        OpenDir(fullFileName);
    }

    private static void AddHeaders(ExcelWorksheet excelWorksheet)
    {
        excelWorksheet.Cells[1, 1].Value = "NAME";
        excelWorksheet.Cells[1, 2].Value = "LINK";
        excelWorksheet.Cells[1, 3].Value = "DESCRIPTION";
        excelWorksheet.Cells[1, 4].Value = "IMAGES";
    }

    private static void OpenDir(string fullName)
    {
        Process.Start(new ProcessStartInfo("explorer.exe", $@"/n, /select, {fullName}")
        {
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Normal
        });
    }
}