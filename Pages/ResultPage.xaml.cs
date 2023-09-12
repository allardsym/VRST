using System.IO.Compression;
using Microsoft.ML.Data;
using Microsoft.ML;
using System.Linq;

namespace VRCT;

public partial class ResultPage : ContentPage
{
	private List<Excercise> excerciseList = new List<Excercise>();
	private List<ListExercise> applicableList = new List<ListExercise>();
	private List<ListExercise> finalList = new List<ListExercise>();
	
	private int bodyPartsInt = 0;
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


		excerciseList = new Excercises().GetExercises();

		Position.Text = " " + Dataset.Position;
		Hands.Text = " " + Dataset.Hands;
		HR.Text = " " + Dataset.HeartRatePatient;
		Time.Text = " 50m";

		bodyPartsInt = Dataset.BodyParts switch
		{
			"Lower" => 0,
			"Upper" => 1,
			"Whole" => 1,
			_ => 1,
		};

		handsInt = Dataset.Hands switch
		{
			"Both" => 0,
			"Left" => 1,
			"Right" => 2,
			_ => 0,
		};

		positionInt = Dataset.Position switch
		{
			"Standing" => 0,
			"Sitting/Laying" => 1,
			_ => 0,
		};

		foreach (var excercise in excerciseList)
		{
			if (excercise.Position == Dataset.Position)
				continue;
			if (excercise.Hands == Dataset.Hands)
				continue;
			if (excercise.Bodyparts == Dataset.BodyParts)
				continue;
			if (!excercise.Injuries.Contains(Dataset.Injury))
				continue;
			if (!excercise.Symptoms.Contains(Dataset.Symptom))
				continue;
			if (!excercise.Goals.Contains(Dataset.Goal))
				continue;
			
			var intensityDifficultyMultiplier = 0.0;
			switch (excercise.IntensityLevel)
			{
				case "Low":
					intensityDifficultyMultiplier = 1;
					break;
				case "Medium":
					intensityDifficultyMultiplier = 0.8;
					break;
				case "Hard":
					intensityDifficultyMultiplier = 0.6;
					break;
			}

			var a = (220 - Dataset.Age);
			var s = Dataset.HeartRatePatient / a;
			var d = s * 0.8;
			var f = 1 - d;
			var g = Dataset.HeartRatePatient;
			var h = Dataset.HeartRatePatient;

			var HeartRatePatientLinear = (1f - (((Dataset.HeartRatePatient) / (220f - Dataset.Age) * 0.8f)));

			var difficulty = (Dataset.DoctorDifficulty * 0.7 + Dataset.PatientDifficulty * 0.3) * HeartRatePatientLinear * intensityDifficultyMultiplier;

			difficultyString = difficulty switch
			{
				<= 36 => "Easy",
				<= 58 => "Medium",
				> 58 => "Hard",
				_ => "Easy",
			};

			var heartRateMultiplier = Dataset.HeartRatePatient switch
			{
				<= 34 => 0.6,
				<= 67 => 1,
				> 67 => 1.4
			};
			

			var intensityDurationMultiplier = 0.0;
			switch (excercise.IntensityLevel)
			{
				case "Low":
					intensityDurationMultiplier = 1;
					break;
				case "Medium":
					intensityDurationMultiplier = 0.6;
					break;
				case "Hard":
					intensityDurationMultiplier = 0.2;
					break;
			}
			
			var doctorVal = Dataset.DoctorValue switch
			{
				<= 10 => 33,
				<= 20 => 66,
				> 20 => 100
			};

			var patientValue = Dataset.PatientValue switch
			{
				<= 10 => 33,
				<= 20 => 66,
				> 20 => 100
			};

			var duration = (doctorVal * 0.8 + patientValue * 0.2) * HeartRatePatientLinear * intensityDurationMultiplier;

			durationString = duration switch
			{
				<= 34 => "10m",
				<= 67 => "20m",
				> 67 => "30m",
				_ => "10m",
			};
			
			applicableList.Add(
				new ListExercise(
					excercise.Name, 
					difficultyString,
					durationString
				)
			);
		}


		var modelHand = 2;
		switch (Dataset.Hands)
		{
			case "Both":
				modelHand = 2;
				break;
			case "Left":
				modelHand = 1;
				break;
			case "Right":
				modelHand = 1;
				break;
		}

		MLModel3.ModelInput modelInput = new MLModel3.ModelInput()
		{
			Position = Dataset.Position,
			Hands = modelHand,
			Bodyparts = Dataset.BodyParts,
			Injuries = Dataset.Injury,
			Symptoms = Dataset.Symptom,
			Goals = Dataset.Goal,
			Outcome = null
		};

		// MLModel3.ModelInput modelInput = new MLModel3.ModelInput()
		// {
		// 	Position = "Standing",
		// 	Hands = 2,
		// 	Bodyparts = "Lower",
		// 	Injuries = "S",
		// 	Symptoms = "P",
		// 	Goals = "R/RK"
		// };

		var modelOutput = Task.Run(() => MLModel3.Predict(modelInput));
		var labelBuffer = new VBuffer<ReadOnlyMemory<char>>();
		MLModel3.PredictEngine.Value.OutputSchema["Score"].Annotations.GetValue("SlotNames", ref labelBuffer);
		var labels = labelBuffer.DenseValues().Select(l => l.ToString()).ToArray();

		var topScores = labels.ToDictionary(l => l, l => (decimal)modelOutput.Result.Score[Array.IndexOf(labels, l)])
			.OrderByDescending(kv => kv.Value)
			.Take(3);

		foreach (var x in topScores)
		{
			Console.WriteLine(x.Key + " " + x.Value);
		}

		foreach (var x in applicableList)
		{
			foreach (var y in topScores)
			{
				if (x.Name == y.Key)
				{
					finalList.Add(x);
				}
			}
		}

		//ExercisesListView.ItemsSource = applicableList;
		
		ExercisesListView.ItemsSource = finalList;
	}

	private static async Task<PredictionEngine<MLModel3.ModelInput, MLModel3.ModelOutput>> predEngineTask()
	{
		const string filePath = "MLModel3.zip";
		using var stream = await FileSystem.OpenAppPackageFileAsync(filePath);
		var mlContext = new MLContext();
		ITransformer mlModel = mlContext.Model.Load(stream, out var _);
		return mlContext.Model.CreatePredictionEngine<MLModel3.ModelInput, MLModel3.ModelOutput>(mlModel);
	}
}

