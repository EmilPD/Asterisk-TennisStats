using System;
using System.IO;
using System.Linq;
using ATPTennisStat.ReportGenerators.Contracts;
using ATPTennisStat.SQLServerData;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ATPTennisStat.ReportGenerators.Enums;

namespace ATPTennisStat.ReportGenerators
{
    public class PdfReportGenerator : IReportGenerator
    {
        private const float TableWidth = 560f;
        private const float PaddingBottom = 5f;
        private const float FontSize = 10f;
        private const float ReportTitleFontSize = 25f;
        private const float SpaceBefore = 25f;
        private const float SpaceAfter = 18f;
        private const int ColumnAlignment = 1;
        private const int MatchesTableColumnsNumber = 5;
        private const int RankingTableColumnsNumber = 6;
        private const string NotProvidedInfo = "Not Provided";

        private BaseColor MainHeadingTitleBackgroundColor = new BaseColor(205, 205, 205);
        private BaseColor HeadingTitleBackgroundColor = new BaseColor(241, 241, 241);

        private string baseReportFileName = "Report-";
        private string reportPath = "\\Reports\\Pdf\\";

        private readonly SqlServerDataProvider provider;
        private PdfReportType reportType;

        public PdfReportGenerator(SqlServerDataProvider provider, PdfReportType reportType)
        {
            this.provider = provider;
            this.ReportType = reportType;
        }

        public PdfReportType ReportType
        {
            get
            {
                return this.reportType;
            }

            set
            {
                this.reportType = value;
            }
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
                return this.baseReportFileName;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("ReportFileName");
                }

                this.baseReportFileName = value;
            }
        }

        public void GenerateReport()
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string reportPath = dir + this.ReportPath;

            this.ExportToPdf(this.provider, reportPath, baseReportFileName, reportType);
        }

        public void ExportToPdf(SqlServerDataProvider sqlProvider, string pathToSave, string reportFileName, PdfReportType reportType)
        {
            if (!string.IsNullOrEmpty(reportFileName))
            {
                CreateDirectoryIfNotExists(pathToSave);
            }

            var reportName = Enum.GetName(typeof(PdfReportType), reportType);
            reportFileName += reportName + ".pdf";

            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(pathToSave + reportFileName, FileMode.Create));

            doc.Open();

            BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            Font helvetica25 = new Font(font, ReportTitleFontSize, Font.BOLD);
            Paragraph heading = new Paragraph("ATP Tennis Stat Report", helvetica25);
            heading.SpacingAfter = 18f;
            heading.Alignment = Element.ALIGN_CENTER;
            doc.Add(heading);

            var tableColumnsNumber = MatchesTableColumnsNumber;

            if (reportType == PdfReportType.Ranking)
            {
                tableColumnsNumber = RankingTableColumnsNumber;
            }

            PdfPTable table = CreateTable(tableColumnsNumber, reportType);

            if (reportType == PdfReportType.Matches)
            {
                var matches = sqlProvider.Matches.GetAll()
                    .OrderBy(m => m.DatePlayed);

                AddHeadingTitle(table, reportName, FontSize, MatchesTableColumnsNumber);
                AddHeaders(table, reportType, FontSize);

                foreach (var match in matches)
                {
                    var matchTournament = CreateColumn(match.Tournament.Name, ColumnAlignment, FontSize);
                    matchTournament.PaddingBottom = PaddingBottom;
                    table.AddCell(matchTournament);

                    var datePlayed = match.DatePlayed.ToString();
                    var parsedDate = datePlayed.Split(' ')[0];
                    var matchDate = CreateColumn(parsedDate, ColumnAlignment, FontSize);
                    matchDate.PaddingBottom = PaddingBottom;
                    table.AddCell(matchDate);

                    var matchWinner = CreateColumn(match.Winner.FirstName + " " + match.Winner.LastName, ColumnAlignment, FontSize);
                    matchWinner.PaddingBottom = PaddingBottom;
                    table.AddCell(matchWinner);

                    var matchLoser = CreateColumn(match.Loser.FirstName + " " + match.Loser.LastName, ColumnAlignment, FontSize);
                    matchLoser.PaddingBottom = PaddingBottom;
                    table.AddCell(matchLoser);

                    var matchResult = CreateColumn(match.Result, ColumnAlignment, FontSize);
                    matchResult.PaddingBottom = PaddingBottom;
                    table.AddCell(matchResult);
                }
            }
            else if (reportType == PdfReportType.Ranking)
            {
                var ranking = sqlProvider.Players.GetAll()
                    .Where(p => p.Ranking != null)
                    .OrderBy(p => p.Ranking);

                AddHeadingTitle(table, reportName, FontSize, RankingTableColumnsNumber);
                AddHeaders(table, reportType, FontSize);

                foreach (var player in ranking)
                {
                    var playerRanking = CreateColumn(player.Ranking.ToString() ?? NotProvidedInfo, ColumnAlignment, FontSize);
                    playerRanking.PaddingBottom = PaddingBottom;
                    table.AddCell(playerRanking);

                    var playerName = CreateColumn(player.FirstName + " " + player.LastName, ColumnAlignment, FontSize);
                    playerName.PaddingBottom = PaddingBottom;
                    table.AddCell(playerName);

                    var playerHeight = CreateColumn(player.Height.ToString() ?? NotProvidedInfo, ColumnAlignment, FontSize);
                    playerHeight.PaddingBottom = PaddingBottom;
                    table.AddCell(playerHeight);

                    var playerWeight = CreateColumn(player.Weight.ToString() ?? NotProvidedInfo, ColumnAlignment, FontSize);
                    playerHeight.PaddingBottom = PaddingBottom;
                    table.AddCell(playerHeight);

                    var playerCityName = "";

                    if (player.City != null)
                    {
                        playerCityName = player.City.Name;
                    }
                    else
                    {
                        playerCityName = NotProvidedInfo;
                    }

                    var playerCity = CreateColumn(playerCityName, ColumnAlignment, FontSize);
                    playerCity.PaddingBottom = PaddingBottom;
                    table.AddCell(playerCity);

                    var playerCountryName = "";

                    if (player.City != null)
                    {
                        playerCountryName = player.City.Country.Name;
                    }
                    else
                    {
                        playerCountryName = NotProvidedInfo;
                    }

                    var playerCountry = CreateColumn(playerCountryName, ColumnAlignment, FontSize);
                    playerCountry.PaddingBottom = PaddingBottom;
                    table.AddCell(playerCountry);
                }
            }
            else
            {
                throw new ArgumentException("Invalid Report Type");
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

        private void AddHeadingTitle(PdfPTable table, string headingTitleName, float fontSize, int Colspan)
        {
            var headingTitle = CreateColumn(headingTitleName, ColumnAlignment, fontSize);
            headingTitle.Colspan = Colspan;
            headingTitle.BackgroundColor = MainHeadingTitleBackgroundColor;
            headingTitle.PaddingBottom = PaddingBottom;
            table.AddCell(headingTitle);
        }

        private PdfPTable CreateTable(int tableColumnsNumber, PdfReportType reportType)
        {
            if (reportType == PdfReportType.Matches)
            {
                PdfPTable table = new PdfPTable(MatchesTableColumnsNumber);

                table.SpacingBefore = SpaceBefore;
                table.TotalWidth = TableWidth;
                table.LockedWidth = true;
                float[] widths = new float[] { 130f, 70f, 140f, 140f, 80f };
                table.SetWidths(widths);

                return table;
            }
            else if (reportType == PdfReportType.Ranking)
            {
                PdfPTable table = new PdfPTable(RankingTableColumnsNumber);

                table.SpacingBefore = SpaceBefore;
                table.TotalWidth = TableWidth;
                table.LockedWidth = true;
                float[] widths = new float[] { 60f, 150f, 60f, 60f, 100f, 130f };
                table.SetWidths(widths);

                return table;
            }
            else
            {
                throw new ArgumentException("Invalid Report type");
            }
        }

        private void AddHeaders(PdfPTable table, PdfReportType reportType, float fontSize)
        {
            if (reportType == PdfReportType.Matches)
            {
                const string MatchTournamentHeadingName = "Tournament";
                const string matchDateHeadingName = "Date played";
                const string matchWinnerHeadingName = "Winner";
                const string matchLoserHeadingName = "Loser";
                const string matchResultHeadingName = "Result";

                var matchTournamentHeading = CreateColumn(MatchTournamentHeadingName, ColumnAlignment, fontSize);
                matchTournamentHeading.BackgroundColor = HeadingTitleBackgroundColor;
                matchTournamentHeading.PaddingBottom = PaddingBottom;

                var matchDateHeading = CreateColumn(matchDateHeadingName, ColumnAlignment, fontSize);
                matchDateHeading.BackgroundColor = HeadingTitleBackgroundColor;
                matchDateHeading.PaddingBottom = PaddingBottom;

                var matchWinnerHeading = CreateColumn(matchWinnerHeadingName, ColumnAlignment, fontSize);
                matchWinnerHeading.BackgroundColor = HeadingTitleBackgroundColor;
                matchWinnerHeading.PaddingBottom = PaddingBottom;

                var matchLoserHeading = CreateColumn(matchLoserHeadingName, ColumnAlignment, fontSize);
                matchLoserHeading.BackgroundColor = HeadingTitleBackgroundColor;
                matchLoserHeading.PaddingBottom = PaddingBottom;

                var matchResultHeading = CreateColumn(matchResultHeadingName, ColumnAlignment, fontSize);
                matchResultHeading.BackgroundColor = HeadingTitleBackgroundColor;
                matchResultHeading.PaddingBottom = PaddingBottom;

                table.AddCell(matchTournamentHeading);
                table.AddCell(matchDateHeading);
                table.AddCell(matchWinnerHeading);
                table.AddCell(matchLoserHeading);
                table.AddCell(matchResultHeading);
            }
            else if (reportType == PdfReportType.Ranking)
            {
                const string PlayerRankHeadingName = "Rank";
                const string PlayerNameHeadingName = "Player";
                const string PlayerHeightHeadingName = "Height";
                const string PlayerWeightHeadingName = "Weight";
                const string PlayerCityHeadingName = "City";
                const string PlayerCountryHeadingName = "Country";

                var PlayerRankHeading = CreateColumn(PlayerRankHeadingName, ColumnAlignment, fontSize);
                PlayerRankHeading.BackgroundColor = HeadingTitleBackgroundColor;
                PlayerRankHeading.PaddingBottom = PaddingBottom;

                var PlayerNameHeading = CreateColumn(PlayerNameHeadingName, ColumnAlignment, fontSize);
                PlayerNameHeading.BackgroundColor = HeadingTitleBackgroundColor;
                PlayerNameHeading.PaddingBottom = PaddingBottom;

                var PlayerHeightHeading = CreateColumn(PlayerHeightHeadingName, ColumnAlignment, fontSize);
                PlayerHeightHeading.BackgroundColor = HeadingTitleBackgroundColor;
                PlayerHeightHeading.PaddingBottom = PaddingBottom;

                var PlayerWeightHeading = CreateColumn(PlayerWeightHeadingName, ColumnAlignment, fontSize);
                PlayerWeightHeading.BackgroundColor = HeadingTitleBackgroundColor;
                PlayerWeightHeading.PaddingBottom = PaddingBottom;

                var PlayerCityHeading = CreateColumn(PlayerCityHeadingName, ColumnAlignment, fontSize);
                PlayerCityHeading.BackgroundColor = HeadingTitleBackgroundColor;
                PlayerCityHeading.PaddingBottom = PaddingBottom;

                var PlayerCountryHeading = CreateColumn(PlayerCountryHeadingName, ColumnAlignment, fontSize);
                PlayerCountryHeading.BackgroundColor = HeadingTitleBackgroundColor;
                PlayerCountryHeading.PaddingBottom = PaddingBottom;

                table.AddCell(PlayerRankHeading);
                table.AddCell(PlayerNameHeading);
                table.AddCell(PlayerHeightHeading);
                table.AddCell(PlayerWeightHeading);
                table.AddCell(PlayerCityHeading);
                table.AddCell(PlayerCountryHeading);
            }
            else
            {
                throw new ArgumentException("Invalid Report type");
            }
        }

        private PdfPCell CreateColumn(string columnName, int alignment, float fontSize)
        {
            BaseFont font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            Font helvetica10 = new Font(font, fontSize, Font.NORMAL);

            PdfPCell column = new PdfPCell(new Phrase(columnName, helvetica10));
            column.HorizontalAlignment = alignment;

            return column;
        }
    }
}