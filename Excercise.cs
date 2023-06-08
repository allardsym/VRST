using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRCT
{
	public class Excercise
	{
		public string Name { get; set; }
		public List<int> Hands { get; set; }
		public List<int> Position { get; set; }
		public List<int> Type { get; set; }
		public List<int> Difficulty { get; set; }
		public List<int> Intensity { get; set; }

		public Excercise(string name, List<int> hands, List<int> pos, List<int> type, List<int> diff, List<int> intensity)
		{
			this.Name = name;
			this.Hands = hands;
			this.Position = pos;
			this.Type = type;
			this.Difficulty = diff;
			this.Intensity = intensity;
		}
	}
}
