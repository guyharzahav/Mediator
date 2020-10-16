using System.Windows;
using System.Windows.Data;
using Mediator.ViewModel;

namespace Mediator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel VM;
        public MainWindow()
        {           
            InitializeComponent();
            VM = new MainWindowViewModel();
            DataContext = VM;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource knowledgeItemViewSource = ((CollectionViewSource)(this.FindResource("knowledgeItemViewSource")));
            knowledgeItemViewSource.Source = VM.knowledgeItemViewSource;
            CollectionViewSource dataPointViewSource = ((CollectionViewSource)(this.FindResource("dataPointViewSource")));
            dataPointViewSource.Source = VM.dataPointViewSource;
            CollectionViewSource interValueViewSource = ((CollectionViewSource)(this.FindResource("interValueViewSource")));
            interValueViewSource.Source = VM.interValueViewSource;
        }
    }
}
