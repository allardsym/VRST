using CommunityToolkit.Maui.Views;

namespace VRCT;

public partial class BodyPartsPopup : Popup
{
	public BodyPartsPopup()
	{
		InitializeComponent();
		CollectionView.ItemsSource = new Categories().GetCategories();
	}

	private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var current = e.CurrentSelection;
		Dataset.BodyParts = current.ToString();
	}
}