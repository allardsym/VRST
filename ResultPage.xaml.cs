using System.Collections.ObjectModel;

namespace VRCT;

public partial class ResultPage : ContentPage
{
	private List<Excercise> excerciseList = new List<Excercise>();
	private List<ListExercise> applicableList = new List<ListExercise>();
	
	private int handsInt = 0;
	private int positionInt = 0;
	private int typeInt = 0;
	private int difficultyInt = 0;
	private int intensityInt = 0;
	private string difficultyString;
	private int durationInt;
	private string durationString;
	
	public ResultPage()
	{
		InitializeComponent();
		AddExercises();

		Position.Text = " " + Dataset.Position;
		Hands.Text = " " + Dataset.Hands;

		handsInt = Dataset.Hands switch
		{
			"Both" => 1,
			"Left" => 2,
			"Right" => 3,
			_ => throw new NotImplementedException(),
		};

		positionInt = Dataset.Position switch
		{
			"Stand" => 1,
			"Sit" => 2,
			"Lay" => 3,
			_ => throw new NotImplementedException(),
		};

		typeInt = Dataset.PatientExerciseType switch
		{
			"Calm" => 1,
			"Sport" => 2,
			_ => throw new NotImplementedException(),
		};

		var difficulty = (0.6 * Dataset.DoctorDifficulty) + (0.3 * Dataset.HeartRatePatient) + (0.1 * Dataset.PatientDifficulty);
		difficultyInt = difficulty switch
		{
			<= 34 => 1,
			<= 67 => 2,
			> 67 => 3,
			_ => 0
		};

		difficultyString = difficulty switch
		{
			<= 34 => "Easy",
			<= 67 => "Medium",
			> 67 => "Hard",
			_ => "Easy",
		};

		// var intensity = (0.6 * Dataset.DoctorDifficulty) + (0.3 * Dataset.HeartRatePatient) + (0.1 * Dataset.PatientDifficulty);
		// intensityInt = intensity switch
		// {
		// 	<= 34 => 1,
		// 	<= 67 => 2,
		// 	> 67 => 3,
		// 	_ => 0
		// };
		//
		Difficulty.Text = " " + difficulty switch
		{
			<= 34 => "Easy",
			<= 67 => "Medium",
			> 67 => "Hard",
			_ => Difficulty.Text
		};

		foreach (var excercise in excerciseList)
		{
			if (!excercise.Hands.Contains(handsInt))
				continue;
			if (!excercise.Position.Contains(positionInt))
				continue;
			if (!excercise.Type.Contains(typeInt))
				continue;
			// if (!excercise.Difficulty.Contains(difficultyInt))
			// 	continue;
			// if (!excercise.Intensity.Contains(intensityInt))
			// 	continue;
			
			durationInt += ((int)(difficulty * excercise.Intensity[0]));
			durationString = ((int)(difficulty * excercise.Intensity[0])).ToString() + "s";
			applicableList.Add(new ListExercise(
				excercise.Name, 
				difficultyString,
				durationString));
		}

		Time.Text = durationInt.ToString() + "s";
		Exercises.ItemsSource = applicableList;
	}


	private void AddExercises()
	{

		excerciseList.Add(
			new Excercise("Beach Ball Squats",
				new List<int> { 1 },
				new List<int> { 1 },
				new List<int> { 2 },
				new List<int> { 3 },
				new List<int> { 2 }
			));

		excerciseList.Add(
			new Excercise("Goalkeeping",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise("Fruit Picking",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 1 }
			));

		excerciseList.Add(
			new Excercise("Tennis",
				new List<int> { 1, 2, 3 },
				new List<int> { 1 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise("Boxing",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise("Sorting",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));

		excerciseList.Add(
			new Excercise("Fireflies",
				new List<int> { 1 },
				new List<int> { 1, 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));

		excerciseList.Add(
			new Excercise("Archery",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));

		excerciseList.Add(
			new Excercise("Music Sword",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise("Witch Craft",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise("Skiing",
				new List<int> { 1, 2, 3 },
				new List<int> { 1 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise("Please be seated",
				new List<int> { 1, 2, 3 },
				new List<int> { 1 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));

		excerciseList.Add(
			new Excercise("Ring Dive",
				new List<int> { 1 },
				new List<int> { 1 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 1 }
			));

		excerciseList.Add(
			new Excercise("Breathing",
				new List<int> { 1, 2, 3 },
				new List<int> { 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 1 }
			));

		excerciseList.Add(
			new Excercise("Leg Raises",
				new List<int> { 1 },
				new List<int> { 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));
	}
}

