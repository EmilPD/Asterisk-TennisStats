using System;
using System.IO;
using ATPTennisStat.ReportGenerators.Contracts;
using ATPTennisStat.SQLServerData;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ATPTennisStat.ReportGenerators
{
    public class PdfReportGenerator : IReportGenerator
    {
        private string reportFileName = "MatchesReport.pdf";
        private string reportPath = "\\Reports\\Pdf\\";
        private const int TableColumnsNumber = 5;
        private const float PaddingBottom = 5f;

        private readonly SqlServerDataProvider provider;

        public PdfReportGenerator(SqlServerDataProvider provider)
        {
            this.provider = provider;
        }

        public string ReportPath
        {
            get
            {
                return this.reportPath;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ReportPath");
                }

                this.reportPath = value;
            }
        }

        public string ReportFileName
        {
            get
            {
                return this.reportFileName;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ReportFileName");
                }

                this.reportFileName = value;
            }
        }

        public void GenerateReport()
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string reportPath = dir + this.ReportPath;

            this.ExportToPdf(this.provider, reportPath, this.ReportFileName);
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
            Font helvetica25 = new Font(font, 25f, Font.BOLD);
            Paragraph heading = new Paragraph("ATP Tennis Stat Report", helvetica25);
            heading.SpacingAfter = 18f;
            heading.Alignment = Element.ALIGN_CENTER;
            doc.Add(heading);

            var matches = sqlProvider.Matches.GetAll();

            PdfPTable table = CreateTable(TableColumnsNumber);

            var matchesHeading = CreateColumn("Matches ", 1);
            matchesHeading.Colspan = TableColumnsNumber;
            matchesHeading.BackgroundColor = new BaseColor(205, 205, 205);
            matchesHeading.PaddingBottom = PaddingBottom;
            table.AddCell(matchesHeading);

            GetHeaders(table);

            foreach (var match in matches)
            {
                var matchTournament = CreateColumn(match.Tournament.Name, 1);
                matchTournament.PaddingBottom = PaddingBottom;
                table.AddCell(matchTournament);

                var datePlayed = match.DatePlayed.ToString();
                var parsedDate = datePlayed.Split(' ')[0];
                var matchDate = CreateColumn(parsedDate, 1);
                matchDate.PaddingBottom = PaddingBottom;
                table.AddCell(matchDate);

                var matchWinner = CreateColumn(match.Winner.FirstName + " " + match.Winner.LastName, 1);
                matchWinner.PaddingBottom = PaddingBottom;
                table.AddCell(matchWinner);

                var matchLoser = CreateColumn(match.Loser.FirstName + " " + match.Loser.LastName, 1);
                matchLoser.PaddingBottom = PaddingBottom;
                table.AddCell(matchLoser);

                var matchResult = CreateColumn(match.Result, 1);
                matchResult.PaddingBottom = PaddingBottom;
                table.AddCell(matchResult);
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
            float[] widths = new float[] { 130f, 70f, 140f, 140f, 80f };
            table.SetWidths(widths);

            return table;
        }

        private void GetHeaders(PdfPTable table)
        {
            var headersColor = new BaseColor(241, 241, 241);
            const string MatchTournamentHeadingName = "Tournament";
            const string matchDateHeadingName = "Date played";
            const string matchWinnerHeadingName = "Winner";
            const string matchLoserHeadingName = "Loser";
            const string matchResultHeadingName = "Result";

            var matchTournamentHeading = CreateColumn(MatchTournamentHeadingName, 1);
            matchTournamentHeading.BackgroundColor = headersColor;
            matchTournamentHeading.PaddingBottom = PaddingBottom;

            var matchDateHeading = CreateColumn(matchDateHeadingName, 1);
            matchDateHeading.BackgroundColor = headersColor;
            matchDateHeading.PaddingBottom = PaddingBottom;

            var matchWinnerHeading = CreateColumn(matchWinnerHeadingName, 1);
            matchWinnerHeading.BackgroundColor = headersColor;
            matchWinnerHeading.PaddingBottom = PaddingBottom;

            var matchLoserHeading = CreateColumn(matchLoserHeadingName, 1);
            matchLoserHeading.BackgroundColor = headersColor;
            matchLoserHeading.PaddingBottom = PaddingBottom;

            var matchResultHeading = CreateColumn(matchResultHeadingName, 1);
            matchResultHeading.BackgroundColor = headersColor;
            matchResultHeading.PaddingBottom = PaddingBottom;

            table.AddCell(matchTournamentHeading);
            table.AddCell(matchDateHeading);
            table.AddCell(matchWinnerHeading);
            table.AddCell(matchLoserHeading);
            table.AddCell(matchResultHeading);
        }

        private PdfPCell CreateColumn(string columnName, int alignment)
        {
            BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            Font helvetica10 = new Font(font, 10f, Font.NORMAL);

            PdfPCell column = new PdfPCell(new Phrase(columnName, helvetica10));
            column.HorizontalAlignment = alignment;

            return column;
        }
    }
}