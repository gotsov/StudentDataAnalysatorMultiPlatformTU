using Prism.Events;

namespace StudentDataAnalysatorMultiPlat;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell(); 
		SingletonClass.EventAggregator = new EventAggregator();
	}

}
