using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace VRCT
{
	public class Excercise
	{
		public string Name { get; set; }
		public string Position { get; set; }
		public string Hands { get; set; }
		public string Bodyparts { get; set; }
		public List<string> Injuries { get; set; }
		public List<string> Symptoms { get; set; }
		public List<string> Goals { get; set; }
		public int Duration { get; set; }
		public string IntensityLevel { get; set; }
		public string CognitiveLoad { get; set; }
		public string PhysicalLoad { get; set; }

		public Excercise(string Name, string Position, string Hands, string Bodyparts, List<string> Injuries, 
			List<string> Symptoms, List<string> Goals, int Duration, string IntensityLevel, string CognitiveLoad, string PhysicalLoad)
		{
			this.Name = Name;
			this.Position = Position;
			this.Hands = Hands;
			this.Bodyparts = Bodyparts;
			this.Injuries = Injuries;
			this.Symptoms = Symptoms;
			this.Goals = Goals;
			this.Duration = Duration;
			this.IntensityLevel = IntensityLevel;
			this.CognitiveLoad = CognitiveLoad;
			this.PhysicalLoad = PhysicalLoad;
		}
	}
}
