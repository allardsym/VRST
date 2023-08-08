using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRCT;

namespace VRCT
{
	class Excercises
	{
		private List<Excercise> excercisesList = new List<Excercise>();
		public List<Excercise> GetExercises()
		{
			excercisesList.Add(
				new Excercise(
					"Beach Ball Squats",
					"Stand",
					"2 hands",
					"Both",
					new List<string> { "S", "E", "W", "C" },
					new List<string> { "P", "STIFF", "RROM" },
					new List<string> { "R/RK", "PM", "IA" },
					5,
					"Medium",
					"Low",
					"High"
				));

			excercisesList.Add(
				new Excercise(
					"Goalkeeping",
					"Both",
					"1 or 2 hands",
					"Upper",
					new List<string> { "K", "A", "C" },
					new List<string> { "RS", "PD", "O/UT" },
					new List<string> { "P/RK", "PM", "FL", "EN", "IROM", "IA" },
					5,
					"Medium",
					"Medium",
					"Medium"
				));

			excercisesList.Add(
				new Excercise(
					"Fruit Picking",
					"Both",
					"1 or 2 hands",
					"Upper",
					new List<string> { "W", "S", "E", "K", "C" },
					new List<string> { "STIFF", "RROM" },
					new List<string> { "P/RK", "PM", "FL", "EN", "IROM", "IA" },
					5,
					"Low",
					"Low",
					"Low"
				));

			excercisesList.Add(
				new Excercise(
					"Tennis",
					"Stand",
					"1 or 2 hands",
					"Upper",
					new List<string> { "E", "S", "W" },
					new List<string> { "P", "STIFF", "RROM" },
					new List<string> { "P/RK", "PM", "FL", "EN", "IROM", "IA" },
					7,
					"Medium",
					"Low",
					"Medium"
				));

			excercisesList.Add(
				new Excercise(
					"Boxing",
					"Both",
					"1 or 2 hands",
					"Upper",
					new List<string> { "E", "S", "C" },
					new List<string> { "RS", "O/UT" },
					new List<string> { "P/RK", "FL", "EN", "IROM", "IA" },
					5,
					"High",
					"High",
					"High"
				));

			excercisesList.Add(
				new Excercise(
					"Sorting",
					"Both",
					"1 or 2 hands",
					"Upper",
					new List<string> { "E", "S", "W", "C" },
					new List<string> { "STIFF", "RROM" },
					new List<string> { "P/RK", "PM", "IROM", "IA" },
					5,
					"Medium",
					"High",
					"Low"
				));

			excercisesList.Add(
				new Excercise(
					"Fireflies",
					"Both",
					"1 or 2 hands",
					"Upper",
					new List<string> { "E", "S", "C" },
					new List<string> { "PD", "RROM" },
					new List<string> { "P/RK", "PM", "IROM", "IA" },
					5,
					"Low",
					"Low",
					"Low"
				));

			excercisesList.Add(
				new Excercise(
					"Archery",
					"Both",
					"1 or 2 hands",
					"Upper",
					new List<string> { "E", "S", "W" },
					new List<string> { "STIFF", "RROM" },
					new List<string> { "P/RK", "FL", "EN", "IROM" },
					5,
					"Medium",
					"Low",
					"Medium"
				));

			excercisesList.Add(
				new Excercise(
					"Music Sword",
					"Both",
					"1 or 2 hands",
					"Upper",
					new List<string> { "E", "S", "W" },
					new List<string> { "RS", "STIFF", "RROM" },
					new List<string> { "FL", "EN", "IROM", "IA" },
					10,
					"High",
					"High",
					"Medium"
				));

			excercisesList.Add(
				new Excercise(
					"Witch Craft",
					"Both",
					"1 or 2 hands",
					"Upper",
					new List<string> { "E", "S", "W" },
					new List<string> { "P", "STIFF", "RROM" },
					new List<string> { "PM", "IROM", "IA" },
					5,
					"Medium",
					"Medium",
					"Low"
				));

			excercisesList.Add(
				new Excercise(
					"Skiing",
					"Stand",
					"1 or 2 hands",
					"Lower",
					new List<string> { "K", "E", "S", "C" },
					new List<string> { "O/UT", "RS", "RROM" },
					new List<string> { "P/RK", "PM", "FL", "EN", "IROM", "IA" },
					5,
					"Medium",
					"Low",
					"Medium"
				));

			excercisesList.Add(
				new Excercise(
					"Please be seated",
					"Stand",
					"1 or 2 hands",
					"Both",
					new List<string> { "K", "C" },
					new List<string> { "O/UT", "RS", "RROM", "P" },
					new List<string> { "FL", "EN" },
					5,
					"Medium",
					"Medium",
					"Low"
				));

			excercisesList.Add(
				new Excercise(
					"Ring Dive",
					"Stand",
					"1 or 2 hands",
					"Both",
					new List<string> { "K", "A", "C" },
					new List<string> { "RS", "PD", "O/UT" },
					new List<string> { "PM", "FL", "IROM" },
					8,
					"Medium",
					"Low",
					"High"
				));

			excercisesList.Add(
				new Excercise(
					"Breathing",
					"Sit/Lay",
					"1 or 2 hands",
					"Both",
					new List<string> { "S", "E", "W", "K", "A" },
					new List<string> { "P" },
					new List<string> { "P/RK", "PM" },
					5,
					"Low",
					"Low",
					"Low"
				));

			excercisesList.Add(
				new Excercise(
					"Leg Raises",
					"Sit/Lay",
					"2 hands",
					"Lower",
					new List<string> { "S", "K", "C" },
					new List<string> { "RS", "PD", "O/UT" },
					new List<string> { "FL", "IROM" },
					5,
					"Medium",
					"Low",
					"Medium"
				));

			return excercisesList;
		}
	}
}
