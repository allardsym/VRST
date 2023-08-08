namespace VRCT;

public class ListExercise
{
	public string Name { get; set; }
	public string Difficulty { get; set; }
	public string Duration { get; set; }
	public ListExercise(string name, string difficulty1, string duration)
	{
		Name = name;
		Difficulty = difficulty1;
		Duration = duration;
	}
}