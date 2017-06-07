using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;

namespace ATPTennisStat.Importers
{
    public class ExcelImporter
    {
        public static void Read()
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

            string path = dir + "\\Data\\Excel\\TennisStatsDatabase.xlsx";
            Console.WriteLine(path);
            var workbook = new XLWorkbook(path);
            var ws = workbook.Worksheet(1);
            Console.WriteLine(ws.Name);
            var currentRegion = ws.RangeUsed().AsTable();
            var names = currentRegion.DataRange.Rows()
                .Select(nameRow => nameRow.Field("Name").GetString())
                .ToList();
            names.ForEach(Console.WriteLine);
        }
    }
}
