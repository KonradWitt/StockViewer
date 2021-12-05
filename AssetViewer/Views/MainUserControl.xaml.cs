using System.Windows.Controls;
using Microsoft.Data.Analysis;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using OxyPlot.Legends;

namespace StockViewer
{
    /// <summary>
    /// Interaction logic for SearchUserControl.xaml
    /// </summary>

    public partial class MainUserControl : UserControl, INotifyPropertyChanged
    {
        private AVHelper avHelper = new("PUP3Q1BNVQYSQ7KW");
        private CSVHelper csvHelper = new();
        private ObservableCollection<AVSearchResult> _avSearchResults;
        private AVQuoteResult _selectedQuoteResult;
        private string _searchText;
        private AVSearchResult _selectedSearchResult;
        private AVStock _selectedStock;
        private PlotModel _plotModel;
        private ValueSpan _priceSpan;
        public ObservableCollection<AVSearchResult> AVSearchResults
        {
            get { return _avSearchResults; }
            set
            {
                if (_avSearchResults != value)
                {
                    _avSearchResults = value;
                    OnPropertyChanged();
                }
            }
        }

        public AVQuoteResult SelectedQuoteResult
        {
            get { return _selectedQuoteResult; }
            set
            {
                if (_selectedQuoteResult != value)
                {
                    _selectedQuoteResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                }
            }
        }

        public AVSearchResult SelectedSearchResult
        {
            get { return _selectedSearchResult; }
            set
            {
                if (_selectedSearchResult != value)
                {
                    _selectedSearchResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public AVStock SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                if (_selectedStock != value)
                {
                    _selectedStock = value;
                    OnPropertyChanged();
                }
            }
        }

        public PlotModel PlotModel
        {
            get { return _plotModel; }
            set
            {
                if (_plotModel != value)
                {
                    _plotModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public ValueSpan PriceSpan
        {
            get { return _priceSpan; }
            set
            {
                if (_priceSpan != value)
                {
                    _priceSpan = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainUserControl()
        {
            DataContext = this;
            PlotModel = new PlotModel();
            setupPlotModel();
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            avHelper.SaveSearchCSVFromURL(_searchText);
            DataFrame dataFrame = csvHelper.CSV2DataFrame(_searchText, AVFunction.Search);
            AVSearchDataHelper avSearchDataHelper = new(dataFrame);
            AVSearchResults = new ObservableCollection<AVSearchResult>(avSearchDataHelper.GetSearchResults());
        }

        private void SearchResultsDataGridRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow selectedDataGridRow = (DataGridRow)sender;
            _selectedSearchResult = (AVSearchResult)selectedDataGridRow.DataContext;

            avHelper.SaveQuoteCSVFromURL(SelectedSearchResult.Symbol);
            DataFrame dataFrameQuote = csvHelper.CSV2DataFrame(SelectedSearchResult.Symbol, AVFunction.Quote);
            AVQuoteDataHelper avQuoteDataHelper = new(dataFrameQuote);
            _selectedQuoteResult = avQuoteDataHelper.GetQuoteResult();
            SelectedStock = new AVStock(_selectedSearchResult.Name, _selectedQuoteResult.Symbol, _selectedSearchResult.Currency, _selectedQuoteResult.Price, _selectedQuoteResult.ChangePercent);

            avHelper.SaveDailyAdjustedCSVFromURL(_selectedQuoteResult.Symbol);
            DataFrame dataFrameDailyAdjusted = csvHelper.CSV2DataFrame(SelectedSearchResult.Symbol, AVFunction.DailyAdjusted);
            AVDailyAdjustedDataHelper avDailyAdjustedDataHelper = new(dataFrameDailyAdjusted);
            PriceSpan = avDailyAdjustedDataHelper.GetPriceSpan(0);

            updatePlotModel();
        }

        private void setupPlotModel()
        {
            var dateAxis = new DateTimeAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 50, StringFormat = "dd.MM.yy", Angle = 30, Minimum = DateTimeAxis.ToDouble(DateTime.Now.AddYears(-1)), Maximum = DateTimeAxis.ToDouble(DateTime.Now), Font = "Helvetica", FontWeight = 500, TitleFont = "Helvetica", TitleFontWeight = 500, AxislineColor = OxyColors.White, TextColor = OxyColors.White, ExtraGridlineColor = OxyColors.White, MajorGridlineColor = OxyColors.White, TicklineColor = OxyColors.White, IsPanEnabled = false, IsZoomEnabled = false};
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { Key ="YAxis", MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Font = "Helvetica", FontWeight = 500, TitleFont = "Helvetica", TitleFontWeight = 500, TextColor = OxyColors.White, ExtraGridlineColor = OxyColors.White, AxislineColor = OxyColors.White, MajorGridlineColor = OxyColors.White, MinorGridlineColor = OxyColors.White, TicklineColor = OxyColors.White, IsPanEnabled = false, IsZoomEnabled = false };
            PlotModel.Axes.Add(valueAxis);
            PlotModel.PlotAreaBorderColor = OxyColors.White;
            PlotModel.PlotMargins = new OxyThickness(30,10,30,40);
            PlotModel.Series.Clear();
            PlotModel.InvalidatePlot(true);
        }

        private void updatePlotModel()
        {
            PlotModel.Series.Clear();
            var lineserie = new LineSeries();
            for (int i=0; i<_priceSpan.TimeStamps.Count; i++)
            {
                lineserie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(_priceSpan.TimeStamps[i]), _priceSpan.Values[i]));
            }
            lineserie.CanTrackerInterpolatePoints = false;
            lineserie.Color = OxyColors.CornflowerBlue;
            lineserie.StrokeThickness = 5;
            PlotModel.Series.Add(lineserie);
            updatePlotModelTimeframe(DateTime.Now.AddYears(-1));
            PlotModel.InvalidatePlot(true);
        }

        private void updatePlotModelTimeframe(DateTime minDate)
        {
            PlotModel.Axes[0].Minimum = DateTimeAxis.ToDouble(minDate);
            updatePlotYAxis(minDate);
            PlotModel.InvalidatePlot(true);
           
        }

        private void updatePlotYAxis(DateTime minDate)
        {
            int count = 0;
            
            for(int i=0; i< _priceSpan.TimeStamps.Count;i++)
            {
                if (_priceSpan.TimeStamps[i] > minDate)
                {
                    count = i;
                }
                else
                {
                    break;
                }
            }

            List<double> filteredPrices = _priceSpan.Values.GetRange(0, count);

            PlotModel.Axes[1].AbsoluteMaximum = filteredPrices.Max() + 0.1* filteredPrices.Max();
            PlotModel.Axes[1].AbsoluteMinimum = filteredPrices.Min() - 0.1 * filteredPrices.Min();
            double priceDifference = PlotModel.Axes[1].AbsoluteMaximum - PlotModel.Axes[1].AbsoluteMinimum;
            PlotModel.Axes[1].MajorStep = Math.Round(priceDifference/3);
        }

        private void btnPlot_1W_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            updatePlotModelTimeframe(DateTime.Now.AddDays(-7));
        }

        private void btnPlot_1M_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            updatePlotModelTimeframe(DateTime.Now.AddMonths(-1));
        }

        private void btnPlot_6M_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            updatePlotModelTimeframe(DateTime.Now.AddMonths(-6));
        }

        private void btnPlot_1Y_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            updatePlotModelTimeframe(DateTime.Now.AddYears(-1));
        }

        private void btnPlot_Max_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            updatePlotModelTimeframe(_priceSpan.TimeStamps.Last());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
