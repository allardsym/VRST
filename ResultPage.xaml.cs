using System.Collections.ObjectModel;

namespace VRCT;

public partial class ResultPage : ContentPage
{
	private List<Excercise> excerciseList = new List<Excercise>();
	private List<ListExercise> applicableList = new List<ListExercise>();
	
	private int excerciseInt = 0;
	private int handsInt = 0;
	private int positionInt = 0;
	private int typeInt = 0;
	private int difficultyInt = 0;
	private int intensityInt = 0;
	private string difficultyString;
	private int durationInt;
	private string durationString;
	private int exerciseDuration = 5;

	public ResultPage()
	{
		InitializeComponent();
		AddExercises();

		Position.Text = " " + Dataset.Position;
		Hands.Text = " " + Dataset.Hands;

		excerciseInt = Dataset.BodyParts switch
		{
			"Legs" => 1,
			"Knee" => 2,
			"Hands" => 3,
			"Spine" => 4,
			"Shoulder" => 5,
			"Balance" => 6,
			"Elbow" => 7,
			"Wrist" => 8,
			"Hand/Eye coordination" => 9,
			"Cognitive" => 10,
			"Upper body" => 11,
			"Focus" => 12,
			"Hip/Knee flexion" => 13,
			"Weight shifting" => 14,
			"Thighs" => 15,
			"Lower Limb" => 16,
			"Lungs" => 17,
			"Calming body and mind" => 18,
			_ => throw new NotImplementedException(),
		};

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

		// var intensity = (0.6 * Dataset.DoctorDifficulty) + (0.3 * Dataset.HeartRatePatient) + (0.1 * Dataset.PatientDifficulty);
		// intensityInt = intensity switch
		// {
		// 	<= 34 => 1,
		// 	<= 67 => 2,
		// 	> 67 => 3,
		// 	_ => 0
		// };
		//

		foreach (var excercise in excerciseList)
		{
			if (!excercise.Categories.Contains(excerciseInt))
				continue;
			if (!excercise.Hands.Contains(handsInt))
				continue;
			if (!excercise.Position.Contains(positionInt))
				continue;
			if (!excercise.Type.Contains(typeInt))
				continue;


			// difficultyInt = difficulty switch
			// {
			// 	<= 34 => 1,
			// 	<= 67 => 2,
			// 	> 67 => 3,
			// 	_ => 0
			// };
			//
			// difficultyString = difficulty switch
			// {
			// 	<= 34 => "Easy",
			// 	<= 67 => "Medium",
			// 	> 67 => "Hard",
			// 	_ => "Easy",
			// };

			// durationInt += ((int)(difficulty / excercise.Intensity[0]));
			// durationString = ((int)(difficulty / excercise.Intensity[0])).ToString() + "s";


			// (
			// 	Sum(0.6 * (Doctor Difficulty) + 0.3 * (Heart Rate Patient int(< 100 = hard)(100 - 140 = medium)(> 140 = easy) ) +0.1 * (Patient Difficulty))) *intensity of exercise(hard= 0.8, medium= 1, easy= 1.2
			// 	) 


			// Easy(0.34)
			// Medium(0.34 - 0.67)
			// Hard(0.67 - 1)

			var intensityMultiplier = excercise.Intensity[0] switch
			{
				<= 1 => 1.2,
				<= 2 => 1,
				>= 3 => 0.8
			};

			var difficulty = (0.6 * Dataset.DoctorDifficulty + 0.3 * Dataset.HeartRatePatient + 0.1 * Dataset.PatientDifficulty) * intensityMultiplier;
			difficultyString = difficulty switch
			{
				<= 34 => "Easy",
				<= 67 => "Medium",
				> 67 => "Hard",
				_ => "Easy",
			};

			var heartRateMultiplier = Dataset.HeartRatePatient switch
			{
				<= 34 => 0.6,
				<= 67 => 1,
				> 67 => 1.4
			};

			var duration = (0.8 * Dataset.DoctorValue + 0.2 * Dataset.PatientValue) * heartRateMultiplier * intensityMultiplier;

			duration = duration / exerciseDuration;

			durationInt += (int)duration;
			// durationString = duration switch
			// {
			// 	<= 34 => "Easy",
			// 	<= 67 => "Medium",
			// 	> 67 => "Hard",
			// 	_ => "Easy",
			// };
			applicableList.Add(new ListExercise(
				excercise.Name, 
				difficultyString,
				(int)duration + "x"));
		}

		Time.Text = " " + durationInt * exerciseDuration + "m";
		Exercises.ItemsSource = applicableList;
	}


	private void AddExercises()
	{

		excerciseList.Add(
			new Excercise(
				new List<int> { 1, 2, 3, 4 }, 
				"Beach Ball Squats",
				new List<int> { 1 },
				new List<int> { 1 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 4, 6, 7 },
				"Goalkeeping",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 1, 3, 5, 7 },
				"Fruit Picking",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 1 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 3, 5, 8, 9 },
				"Tennis",
				new List<int> { 1, 2, 3 },
				new List<int> { 1 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 3, 9, 10 },
				"Boxing",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 3, 5, 10 },
				"Sorting",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 3, 5 },
				"Fireflies",
				new List<int> { 1 },
				new List<int> { 1, 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 11, 12 },
				"Archery",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 3, 5, 10, 12 },
				"Music Sword",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 3, 5, 7, 10 },
				"Witch Craft",
				new List<int> { 1, 2, 3 },
				new List<int> { 1, 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 1, 5, 7, 13 },
				"Skiing",
				new List<int> { 1, 2, 3 },
				new List<int> { 1 },
				new List<int> { 2 },
				new List<int> { 1, 2, 3 },
				new List<int> { 3 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 13, 14 },
				"Please be seated",
				new List<int> { 1, 2, 3 },
				new List<int> { 1 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 1, 2, 15, 16 },
				"Ring Dive",
				new List<int> { 1 },
				new List<int> { 1 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 1 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 17, 18 },
				"Breathing",
				new List<int> { 1, 2, 3 },
				new List<int> { 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 1 }
			));

		excerciseList.Add(
			new Excercise(
				new List<int> { 1, 16, 17 },
				"Leg Raises",
				new List<int> { 1 },
				new List<int> { 2 },
				new List<int> { 1 },
				new List<int> { 1, 2, 3 },
				new List<int> { 2 }
			));
	}
}

