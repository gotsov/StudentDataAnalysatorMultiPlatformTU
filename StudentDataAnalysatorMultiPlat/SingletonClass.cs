using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataAnalysatorMultiPlat
{
    public sealed class SingletonClass
    {
        private static readonly SingletonClass instance = new SingletonClass();

        static SingletonClass()
        {
        }

        private SingletonClass()
        {
        }

        public static SingletonClass Instance
        {
            get
            {
                return instance;
            }
        }

        public static IEventAggregator EventAggregator { get; set; }
    }
}
