using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using OfficeOpenXml;

namespace Alfred.Client.Services
{
    public class SharedServices
    {
        private readonly IJSRuntime _jsRuntime;

        public SharedServices(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ExportExcelAsync<TItem>(IEnumerable<TItem> items, string name)
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(items, true);
                await _jsRuntime.InvokeVoidAsync("saveAsFile", $"{name}.xlsx",
                    Convert.ToBase64String(package.GetAsByteArray()));
            }
        }
    }
}