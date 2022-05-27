namespace LearningProjects.Utils
{
  public class MathUtils
  {
    /// <summary>
    /// Inclusive random list of ints.
    /// </summary>
    /// <param name="size">Size of array</param>
    /// <param name="min">inclusive min val</param>
    /// <param name="max">inclusive max val</param>
    /// <returns>List<int></returns>
    public static List<int> GenRandomInts(int size, int min, int max)
    {
      var list = new List<int>();
      var rand = new Random();

      for (var i = 0; i < size; i++)
      {
        list.Add(rand.Next(min, max));
      }

      return list;
    }
  }
}
