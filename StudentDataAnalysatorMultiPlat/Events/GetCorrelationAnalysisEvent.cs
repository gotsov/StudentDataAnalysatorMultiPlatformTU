using Prism.Events;
using StudentDataAnalysatorMultiPlat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.Events
{
    public class GetCorrelationAnalysisEvent : PubSubEvent<ObservableCollection<CorrelationAnalysisResult>>
    {
    }
}
