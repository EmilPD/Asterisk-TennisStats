using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;

using ATPTennisStat.ReportGenerators.Contracts;
using ATPTennisStat.SQLServerData;

namespace ATPTennisStat.ReportGenerators
{
    public class PdfReportGenerator : IReportGenerator
    {
        private const string ReportFileName = "TestReport.pdf";
        private const string ReportPath = "\\Reports\\Pdf\\";
        private const int TableColumnsNumber = 2;

        private readonly SqlServerDataProvider provider;

        public PdfReportGenerator(SqlServerDataProvider provider)
        {
            this.provider = provider;
        }

        public void GenerateReport()
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string path = dir + ReportPath;
            
            this.ExportToPdf(this.provider, path, ReportFileName);
        }

        public void ExportToPdf(SqlServerDataProvider sqlProvider, string pathToSave, string reportFileName)
        {
            if (!string.IsNullOrEmpty(reportFileName))
            {
                CreateDirectoryIfNotExists(pathToSave);
            }
            
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(pathToSave + reportFileName, FileMode.Create));

            doc.Open();
            
            BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            Font helvetica = new Font(font, 25f, Font.BOLD);
            Paragraph heading = new Paragraph("ATP Tennis Stat Report", helvetica);
            heading.SpacingAfter = 18f;
            heading.Alignment = Element.ALIGN_CENTER;
            doc.Add(heading);

            var cities = sqlProvider.cities.GetAll();

            PdfPTable table = CreateTable(TableColumnsNumber);
            
            var citiesHeading = CreateColumn("Cities ", 1);
            citiesHeading.Colspan = TableColumnsNumber;
            citiesHeading.BackgroundColor = new BaseColor(205, 205, 205);
            citiesHeading.PaddingBottom = 5f;
            table.AddCell(citiesHeading);

            GetHeaders(table);

            foreach (var city in cities)
            {
                var cityName = CreateColumn(city.Name, 1);
                cityName.PaddingBottom = 5f;
                table.AddCell(cityName);

                var countryName = CreateColumn(city.Country.Name, 1);
                countryName.PaddingBottom = 5f;
                table.AddCell(countryName);
            }

            doc.Add(table);
            doc.Close();
        }

        private static void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        private PdfPTable CreateTable(int tableColumnsNumber)
        {
            PdfPTable table = new PdfPTable(TableColumnsNumber);
            table.SpacingBefore = 25f;
            table.TotalWidth = 560f;
            table.LockedWidth = true;
            float[] widths = new float[] { 280f, 280f };
            table.SetWidths(widths);

            return table;
        }

        private void GetHeaders(PdfPTable table)
        {
            var cityNameHeading = CreateColumn("City name", 1);
            cityNameHeading.BackgroundColor = new BaseColor(241, 241, 241);
            cityNameHeading.PaddingBottom = 5f;

            var countryNameHeading = CreateColumn("Country name", 1);
            countryNameHeading.BackgroundColor = new BaseColor(241, 241, 241);
            countryNameHeading.PaddingBottom = 5f;

            table.AddCell(cityNameHeading);
            table.AddCell(countryNameHeading);
        }

        private PdfPCell CreateColumn(string columnName, int alignment)
        {
            PdfPCell column = new PdfPCell(new Phrase(columnName));
            column.HorizontalAlignment = alignment;

            return column;
        }
    }
}