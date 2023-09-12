using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions;
using Microsoft.ML.Data;
using Microsoft.ML;
using Newtonsoft.Json.Linq;

namespace VRCT;

public partial class MainPage : ContentPage
{
	private bool LiveHr = false;
	public MainPage()
	{
		InitializeComponent();


		// MLModel3.ModelInput modelInput = new MLModel3.ModelInput()
		// {
		// 	Position = "Standing",
		// 	Hands = 2,
		// 	Bodyparts = "Lower",
		// 	Injuries = "S",
		// 	Symptoms = "P",
		// 	Goals = "R/RK"
		// };

		Dataset.DoctorValue = 15;
		Dataset.PatientValue = 15;


	}

	async Task LoadMauiAsset()
	{
		using var stream = await FileSystem.OpenAppPackageFileAsync("MLModel3.zip");
		using var reader = new StreamReader(stream);

		var contents = reader.ReadToEnd();
	}


	void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	{
		// var picker = (Picker)sender;
		// var selectedIndex = picker.SelectedIndex;
		//
		// if (selectedIndex != -1)
		// 	Dataset.BodyParts = picker.Items[selectedIndex];
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

	void OnBodyRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (sender is RadioButton button) Dataset.BodyParts = (string)button.Content;
	}

	void OnFocusSelectedIndexChangedInjury(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		if (picker.SelectedIndex == 0)
			Dataset.Injury = "S";
		if (picker.SelectedIndex == 1)
			Dataset.Injury = "E";
		if (picker.SelectedIndex == 2)
			Dataset.Injury = "W";
		if (picker.SelectedIndex == 3)
			Dataset.Injury = "K";
		if (picker.SelectedIndex == 4)
			Dataset.Injury = "A";
		if (picker.SelectedIndex == 5)
			Dataset.Injury = "C";
	}

	void OnFocusSelectedIndexChangedSymptom(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		if (picker.SelectedIndex == 0)
			Dataset.Symptom = "P";
		if (picker.SelectedIndex == 1)
			Dataset.Symptom = "STIFF";
		if (picker.SelectedIndex == 2)
			Dataset.Symptom = "RROM";
		if (picker.SelectedIndex == 3)
			Dataset.Symptom = "RS";
		if (picker.SelectedIndex == 4)
			Dataset.Symptom = "PD";
		if (picker.SelectedIndex == 5)
			Dataset.Symptom = "O/UT";

	}

	void OnFocusSelectedIndexChangedGoal(object sender, EventArgs e)
	{
		var picker = (Picker)sender;
		if (picker.SelectedIndex == 0)
			Dataset.Goal = "P/RK";
		if (picker.SelectedIndex == 1)
			Dataset.Goal = "PM";
		if (picker.SelectedIndex == 2)
			Dataset.Goal = "FL";
		if (picker.SelectedIndex == 3)
			Dataset.Goal = "EN";
		if (picker.SelectedIndex == 4)
			Dataset.Goal = "IROM";
		if (picker.SelectedIndex == 5)
			Dataset.Goal = "IA";
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

	void OnGameplayRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		var button = sender as RadioButton;
		if (button != null && (string)button.Content == "Predictable")
			Dataset.Gameplay = 0;
		if (button != null && (string)button.Content == "Chaotic")
			Dataset.Gameplay = 1;
		if (button != null && (string)button.Content == "Both")
			Dataset.Gameplay = 2;
	}

	void OnPatientExerciseRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (sender is RadioButton button) Dataset.PatientExerciseType = (string)button.Content;
	}

	private void OnDoctorValueSliderValueChanged(object sender, ValueChangedEventArgs args)
	{
		var value = (int)args.NewValue;

		if (value == 15 && Dataset.DoctorValue == 0)
			return;

		Dataset.DoctorValue = value;
		DoctorValue.Text = String.Format("Recommended Therapist Duration: {0}m", value);
	}

	private void OnPatientValueSliderValueChanged(object sender, ValueChangedEventArgs args)
	{
		var value = (int)args.NewValue;

		if (value == 15 && Dataset.PatientValue == 0)
			return;

		Dataset.PatientValue = value;
		PatientValue.Text = String.Format("Preferential Patient Duration: {0}m", value);
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
			Dataset.HeartRatePatient = value;
		else
			HrEntry.Text = "";
	}

	private void RadioButton_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		var button = sender as RadioButton;
		if (button != null && (string)button.Content == "10m")
			Dataset.DoctorValue = 10;
		if (button != null && (string)button.Content == "20m")
			Dataset.DoctorValue = 20;
		if (button != null && (string)button.Content == "30m")
			Dataset.DoctorValue = 30;
	}

	private void RadioButton_Preferential_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		var button = sender as RadioButton;
		if (button != null && (string)button.Content == "10m")
			Dataset.PatientValue = 10;
		if (button != null && (string)button.Content == "20m")
			Dataset.PatientValue = 20;
		if (button != null && (string)button.Content == "30m")
			Dataset.PatientValue = 30;
	}

	private void CategoryBtn_OnClicked(object sender, EventArgs e)
	{
		var popup = new BodyPartsPopup();
		this.ShowPopup(popup);
	}

	private void AgeEntry_OnTextChanged(object sender, TextChangedEventArgs e)
	{
		int value;
		if (!int.TryParse(e.NewTextValue, out value))
			AgeEntry.Text = "";
		Dataset.Age = value;
	}

	private void OnGetHRClicked(object sender, EventArgs e)
	{
		HrEntry.Text = Dataset.HeartRateDevice.ToString();
		// HrEntry.IsReadOnly = !HrEntry.IsReadOnly;
		// LiveHr = !LiveHr;
		// var _ = UpdateHrText();
	}

	// private async Task UpdateHrText()
	// {
	// 	while (LiveHr)
	// 	{
	// 		HrEntry.Text = Dataset.HeartRateDevice.ToString();
	// 	}
	// }
}