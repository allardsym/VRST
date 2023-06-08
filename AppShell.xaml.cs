namespace VRCT;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("ResultPage", typeof(ResultPage));
	}
}
