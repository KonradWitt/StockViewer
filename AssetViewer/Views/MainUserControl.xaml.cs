using System.Windows.Controls;
using Microsoft.Data.Analysis;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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
        private AVQuoteResult _avQuoteResult;
        private string _searchText;
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

        public AVQuoteResult AVQuoteResult
        {
            get { return _avQuoteResult; }
            set
            {
                if (_avQuoteResult != value)
                {
                    _avQuoteResult = value;
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

        

        public MainUserControl()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            avHelper.SaveSearchCSVFromURL(_searchText);            
            DataFrame dataFrame = csvHelper.CSV2DataFrame(_searchText, AVFunction.search);
            AVSearchDataHelper avSearchDataHelper = new(dataFrame);
            AVSearchResults = new ObservableCollection<AVSearchResult>(avSearchDataHelper.GetSearchResults());
        }

        private void SearchResultsDataGridRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow selectedDataGridRow = (DataGridRow)sender;
            AVSearchResult selectedSearchResult = (AVSearchResult)selectedDataGridRow.DataContext;
            avHelper.SaveQuoteCSVFromURL(selectedSearchResult.Symbol);
            DataFrame dataFrame = csvHelper.CSV2DataFrame(selectedSearchResult.Symbol, AVFunction.quote);
            AVQuoteDataHelper avQuoteDataHelper = new(dataFrame);
            AVQuoteResult = avQuoteDataHelper.GetQuoteResult();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
