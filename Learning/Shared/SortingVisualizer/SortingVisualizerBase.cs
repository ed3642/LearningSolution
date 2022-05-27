using Learning.Utils;
using Microsoft.AspNetCore.Components;

namespace Learning.Shared
{
  public class SortingVisualizerBase : ComponentBase
  {
    [CascadingParameter]
    public CascadingAppState AppState { get; set; } = null!;

    public long SortTimer { get; set; } = 0;
    public int NextSize { get; set; }
    public int MinArraySize { get; set; } = 2;
    public int MaxArraySize { get; set; } = 1000;
    public int ArraySize { get; set; } = 100;
    public int MinVal { get; set; } = 5;
    public int MaxVal { get; set; } = 100;
    public List<int> GeneratedList { get; set; } = null!;
    public List<Tuple<double, double>> RandomArrayPercentages { get; set; } = null!;

    private List<int> _originalOrder = new List<int>();

    protected override void OnInitialized()
    {
      NewArray(ArraySize, MinVal, MaxVal);
    }

    protected void MergeSort(List<int> arr)
    {
      var aIndex = 0;
      var bIndex = arr.Count - 1;
      var middleIndex = (int)Math.Floor((decimal)(bIndex - aIndex) / 2) + aIndex;
      var a = arr[aIndex];
      var b = arr[bIndex];
      var m = arr[middleIndex];

      if (aIndex != bIndex)
      {

      }
    }

    private void Merge(List<int> arr, int a, int b, int m)
    {

    }

    protected void BubbleSort()
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();

      for (var i = 0; i < GeneratedList.Count; i++)
      {
        for (var j = 0; j < GeneratedList.Count - 1; j++)
        {
          var left = GeneratedList[j];
          var right = GeneratedList[j + 1];

          if (right < left)
          {
            (GeneratedList[j], GeneratedList[j + 1]) = (GeneratedList[j + 1], GeneratedList[j]);
          }
        }
      }

      GetPercentageArray(GeneratedList);

      watch.Stop();
      SortTimer = watch.ElapsedMilliseconds;
    }
    protected void CsharpSort()
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();

      GeneratedList.Sort();
      GetPercentageArray(GeneratedList);

      watch.Stop();
      SortTimer = watch.ElapsedMilliseconds;
    }

    protected void ResetArray()
    {
      GetPercentageArray(_originalOrder);
    }

    protected void GetPercentageArray(List<int> arr)
    {
      RandomArrayPercentages = new List<Tuple<double, double>>(ArraySize);
      var percentageX = Math.Round(value: (double)1 / ArraySize * 100, 5);

      foreach (var num in arr)
      {
        var percentageY = Math.Round(value: (double)num / MaxVal * 100, 5);

        RandomArrayPercentages.Add(Tuple.Create(percentageX, percentageY));
      }
    }

    // experimental to show loader
    protected void ShowLoader()
    {
      AppState.SomethingIsLoading = true;
    }
    protected void NewArray(int size, int min, int max, bool resize = false)
    {
      if (resize)
      {
        ArraySize = NextSize;
      }
      else
      {
        NextSize = size;
      }

      GeneratedList = MathUtils.GenRandomInts(size, min, max);
      _originalOrder = GeneratedList.ToList(); // copy by value
      GetPercentageArray(GeneratedList);
    }
  }
}
