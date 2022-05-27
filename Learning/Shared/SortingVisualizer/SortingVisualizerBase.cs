using Learning.Utils;
using Microsoft.AspNetCore.Components;

namespace Learning.Shared
{
  public class SortingVisualizerBase : ComponentBase
  {
    [CascadingParameter]
    public CascadingAppState AppState { get; set; } = null!;

    public bool DoAnimation { get; set; }
    public long SortTimer { get; set; } = 0;
    public int NextSize { get; set; }
    public int MinArraySize { get; set; } = 2;
    public int MaxArraySize { get; set; } = 1000;
    public int ArraySize { get; set; } = 100;
    public int MinVal { get; set; } = 5;
    public int MaxVal { get; set; } = 100;
    public List<int> GeneratedList { get; set; } = null!;
    public List<RenderedBar> RandomArrayPercentages { get; set; } = null!;

    public struct RenderedBar
    {
      public RenderedBar(double widthP, double heightP, bool isSelected = false)
      {
        WidthP = widthP;
        HeightP = heightP;
        IsSelected = isSelected;
      }

      public double WidthP { get; set; }
      public double HeightP { get; set; }
      public bool IsSelected { get; set; }
    }

    private int _left = -1;
    private int _right = -1;

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

    protected async Task BubbleSort()
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();

      for (var i = 0; i < GeneratedList.Count; i++)
      {
        for (var j = 0; j < GeneratedList.Count - 1; j++)
        {
          if (GeneratedList[j + 1] < GeneratedList[j])
          {
            _left = j;
            _right = j + 1;
            (GeneratedList[_left], GeneratedList[_right]) = (GeneratedList[_right], GeneratedList[_left]);

            GetBarProperties(_left);
            GetBarProperties(_right);
            if (DoAnimation)
            {
              StateHasChanged();
              await Task.Delay(1);
            }
          }
        }
      }

      _left = -1;
      _right = -1;

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
    protected string SelectBar(int n)
    {
      if (n == _left)
      {
        return "background-color: red";
      }

      if (n == _right)
      {
        return "background-color: red";
      }

      return "";
    }

    protected void GetBarProperties(int i)
    {
      var num = GeneratedList[i];
      var percentageX = Math.Round(value: (double)1 / ArraySize * 100, 5);
      var percentageY = Math.Round(value: (double)num / MaxVal * 100, 5);

      RandomArrayPercentages[i] = new RenderedBar(percentageX, percentageY, true);
    }

    protected void GetPercentageArray(List<int> arr)
    {
      RandomArrayPercentages = new List<RenderedBar>();
      var percentageX = Math.Round(value: (double)1 / ArraySize * 100, 5);

      foreach (var num in arr)
      {
        var percentageY = Math.Round(value: (double)num / MaxVal * 100, 5);

        RandomArrayPercentages.Add(new RenderedBar(percentageX, percentageY));
      }
    }

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
      GetPercentageArray(GeneratedList);
    }
  }
}
