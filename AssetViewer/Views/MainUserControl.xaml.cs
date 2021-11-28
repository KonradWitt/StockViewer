using System.Windows.Controls;
using Microsoft.Data.Analysis;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockViewer
{
    /// <summary>
    /// Interaction logic for SearchUserControl.xaml
    /// </summary>

    public partial class MainUserControl : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<AVSearchResult> _avSearchResults;
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

        private string _searchText;
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

        private AVHelper avHelper = new("PUP3Q1BNVQYSQ7KW");
        private CSVHelper csvHelper = new();

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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
