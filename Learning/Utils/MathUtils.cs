namespace Learning.Utils
{
    public class MathUtils
    {
        public static List<int> GenRandomInts(int size, int min, int max)
        {
            var list = new List<int>(size);
            var rand = new Random();

            for (var i = 0; i < size; i++)
            {
                list.Add(rand.Next(min, max));
            }

            return list;
        }
    }
}
