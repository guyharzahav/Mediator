using Mediator.communication;
using Mediator.DataService;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mediator.ViewModel
{
    class MainWindowViewModel
    {
        Services Services;
        public ObservableCollection<KnowledgeService.KnowledgeItem> knowledgeItemViewSource { get; }
        public ObservableCollection<DataPoint> dataPointViewSource { get; }
        public ObservableCollection<IntersectionValue> interValueViewSource { get; }
        public MainWindowViewModel() 
        {
            Services = new Services();
            knowledgeItemViewSource = new ObservableCollection<KnowledgeService.KnowledgeItem>(Services.getKnowledgeItemsByConceptAndOutput());
            dataPointViewSource = new ObservableCollection<DataPoint>(Services.getDataPoints());
            interValueViewSource = new ObservableCollection<IntersectionValue>(Services.calcStateEffComplex(knowledgeItemViewSource.First(), knowledgeItemViewSource.Last(), Services.Plus));
        }
    }
}
