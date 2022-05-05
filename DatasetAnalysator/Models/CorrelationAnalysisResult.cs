using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat.Models
{
    public class CorrelationAnalysisResult
    {
        private float corralation;

        public CorrelationAnalysisResult(float corralation)
        {
            this.corralation = corralation;

        }

        public float Correlation
        {
            get { return corralation; }
            set { corralation = value; }
        }
    }
}
