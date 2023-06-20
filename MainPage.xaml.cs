using System.Globalization;
using System.Reflection.Metadata;
using CommunityToolkit.Maui.Behaviors;

namespace VRCT;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		var selectedIndex = picker.SelectedIndex;

		if (selectedIndex != -1)
			Dataset.BodyParts = picker.Items[selectedIndex];
	}
	
	private async void OnGetClicked(object sender, EventArgs e)
	{

		await Shell.Current.GoToAsync("ResultPage");
	}

	void OnHandsRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (sender is RadioButton button) Dataset.Hands = (string)button.Content;
	}

	void OnPositionRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (sender is RadioButton button) Dataset.Position = (string)button.Content;
	}

	void OnDoctorDifficultyRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		var button = sender as RadioButton;
		if (button != null && (string)button.Content == "Easy")
			Dataset.DoctorDifficulty = 34;
		if (button != null && (string)button.Content == "Medium")
			Dataset.DoctorDifficulty = 67;
		if (button != null && (string)button.Content == "Hard")
			Dataset.DoctorDifficulty = 100;
	}

	void OnPatientDifficultyRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		var button = sender as RadioButton;
		if (button != null && (string)button.Content == "Easy")
			Dataset.PatientDifficulty = 34;
		if (button != null && (string)button.Content == "Medium")
			Dataset.PatientDifficulty = 67;
		if (button != null && (string)button.Content == "Hard")
			Dataset.PatientDifficulty = 100;
	}

	void OnPatientExerciseRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (sender is RadioButton button) Dataset.PatientExerciseType = (string)button.Content;
	}
	
	private void OnDoctorValueSliderValueChanged(object sender, ValueChangedEventArgs args)
	{
		var value = (int)args.NewValue;
		DoctorValue.Text = $"Doctor Duration: {value}";

		Dataset.DoctorValue = value;
	}
	
	private void OnPatientValueSliderValueChanged(object sender, ValueChangedEventArgs args)
	{
		var value = (int)args.NewValue;
		PatientValue.Text = $"Patient Duration: {value}";

		Dataset.PatientValue = value;
	}

	private void OnHearthRatePatientSliderValueChanged(object sender, ValueChangedEventArgs args)
	{
		var value = (int)args.NewValue;
		Hrp.Text = $"Heart Rate Patient: {value}";

		Dataset.HeartRatePatient = value switch
		{
			<= 100 => 100,
			<= 140 => 67,
			> 140 => 34
		};
	}

	private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
	{
		int value;
		if (!int.TryParse(e.NewTextValue, out value))
			HrEntry.Text = "";

		if (value > 0 && value < 300)
			Dataset.HeartRatePatient = value switch
			{
				<= 100 => 100,
				<= 140 => 67,
				> 140 => 34
			};
		else
			HrEntry.Text = "";
	}

	private void RadioButton_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		var button = sender as RadioButton;
		if (button != null && (string)button.Content == "15m")
			Dataset.DoctorValue = 15;
		if (button != null && (string)button.Content == "30m")
			Dataset.DoctorValue = 30;
		if (button != null && (string)button.Content == "45m")
			Dataset.DoctorValue = 45;
	}

	private void RadioButton_Preferential_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		var button = sender as RadioButton;
		if (button != null && (string)button.Content == "15m")
			Dataset.PatientValue = 15;
		if (button != null && (string)button.Content == "30m")
			Dataset.PatientValue = 30;
		if (button != null && (string)button.Content == "45m")
			Dataset.PatientValue = 45;
	}
}
