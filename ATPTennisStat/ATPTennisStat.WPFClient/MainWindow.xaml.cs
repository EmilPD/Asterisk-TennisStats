using ATPTennisStat.Models;
using ATPTennisStat.ReportGenerators;
using ATPTennisStat.Repositories;
using ATPTennisStat.Repositories.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATPTennisStat.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button_Load(object sender, RoutedEventArgs e)
        {

            ATPTennisStat.WPFClient.ATPTennisStatSqlServerDataSet aTPTennisStatSqlServerDataSet 
                = ((ATPTennisStat.WPFClient.ATPTennisStatSqlServerDataSet)
                (this.FindResource("aTPTennisStatSqlServerDataSet")));

            // Load data into the table Countries. You can modify this code as needed.
            ATPTennisStat.WPFClient.ATPTennisStatSqlServerDataSetTableAdapters
                .CountriesTableAdapter aTPTennisStatSqlServerDataSetCountriesTableAdapter 
                = new ATPTennisStat.WPFClient.ATPTennisStatSqlServerDataSetTableAdapters
                .CountriesTableAdapter();

            aTPTennisStatSqlServerDataSetCountriesTableAdapter
                .Fill(aTPTennisStatSqlServerDataSet.Countries);

            System.Windows.Data.CollectionViewSource countriesViewSource 
                = ((System.Windows.Data.CollectionViewSource)
                (this.FindResource("countriesViewSource")));

            countriesViewSource.View.MoveCurrentToFirst();

            // Load data into the table Cities. You can modify this code as needed.
            ATPTennisStat.WPFClient.ATPTennisStatSqlServerDataSetTableAdapters
                .CitiesTableAdapter aTPTennisStatSqlServerDataSetCitiesTableAdapter 
                = new ATPTennisStat.WPFClient
                .ATPTennisStatSqlServerDataSetTableAdapters
                .CitiesTableAdapter();

            aTPTennisStatSqlServerDataSetCitiesTableAdapter
                .Fill(aTPTennisStatSqlServerDataSet.Cities);
            System.Windows.Data.CollectionViewSource citiesViewSource 
                = ((System.Windows.Data.CollectionViewSource)
                (this.FindResource("citiesViewSource")));

            citiesViewSource.View.MoveCurrentToFirst();

            aTPTennisStatSqlServerDataSetCitiesTableAdapter.Update(aTPTennisStatSqlServerDataSet.Cities);
        }

        private void button_Save(object sender, RoutedEventArgs e)
        {
            ATPTennisStat.WPFClient.ATPTennisStatSqlServerDataSet aTPTennisStatSqlServerDataSet
                = ((ATPTennisStat.WPFClient.ATPTennisStatSqlServerDataSet)
                (this.FindResource("aTPTennisStatSqlServerDataSet")));

            // Load data into the table Countries. You can modify this code as needed.
            ATPTennisStat.WPFClient.ATPTennisStatSqlServerDataSetTableAdapters
                .CountriesTableAdapter aTPTennisStatSqlServerDataSetCountriesTableAdapter
                = new ATPTennisStat.WPFClient.ATPTennisStatSqlServerDataSetTableAdapters
                .CountriesTableAdapter();

            System.Windows.Data.CollectionViewSource countriesViewSource
                = ((System.Windows.Data.CollectionViewSource)
                (this.FindResource("countriesViewSource")));

            // Load data into the table Cities. You can modify this code as needed.
            ATPTennisStat.WPFClient.ATPTennisStatSqlServerDataSetTableAdapters
                .CitiesTableAdapter aTPTennisStatSqlServerDataSetCitiesTableAdapter
                = new ATPTennisStat.WPFClient
                .ATPTennisStatSqlServerDataSetTableAdapters
                .CitiesTableAdapter();
            
            System.Windows.Data.CollectionViewSource citiesViewSource
                = ((System.Windows.Data.CollectionViewSource)
                (this.FindResource("citiesViewSource")));

            aTPTennisStatSqlServerDataSetCitiesTableAdapter.Update(aTPTennisStatSqlServerDataSet);
            aTPTennisStatSqlServerDataSetCountriesTableAdapter.Update(aTPTennisStatSqlServerDataSet);
        }

        private void button_Generate(object sender, RoutedEventArgs e)
        {
            var context = new SqlServerDbContext();
            var unitOfWork = new EfUnitOfWork(context);
            var citiesRepository = new EfRepository<City>(context);
            var countriesRepository = new EfRepository<Country>(context);

            var provider = new SqlServerDataProvider(
                unitOfWork,
                citiesRepository,
                countriesRepository);

            var generator = new PdfReportGenerator(provider);
            generator.GenerateReport();
        }
    }
}
