using Learning.Utils;
using Microsoft.AspNetCore.Components;

namespace Learning.Shared
{
  public class SortingVisualizerBase : ComponentBase
  {
    [CascadingParameter]
    public CascadingAppState AppState { get; set; } = null!;

    public bool IsFastAnimation { get; set; } = true;
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
      public RenderedBar(double widthP, double heightP)
      {
        WidthP = widthP;
        HeightP = heightP;
      }

      public double WidthP { get; set; }
      public double HeightP { get; set; }
    }

    private int _left = -1;
    private int _right = -1;
    private int _middle = -1;

    protected override void OnInitialized()
    {
      NewArray(ArraySize, MinVal, MaxVal);
    }

    #region Sorting Methods
    protected async Task SelectionSort()
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();

      int smallestBarIndex;
      for (var i = 0; i < GeneratedList.Count - 1; i++)
      {
        smallestBarIndex = i;
        for (var j = i + 1; j < GeneratedList.Count; j++)
        {
          if (GeneratedList[j] < GeneratedList[smallestBarIndex])
          {
            smallestBarIndex = j;
          }
        }

        _left = smallestBarIndex;
        _right = i;
        (GeneratedList[_left], GeneratedList[_right]) = (GeneratedList[_right], GeneratedList[_left]);

        await RenderSelectedBars();
      }

      DeselectBars();

      watch.Stop();
      SortTimer = watch.ElapsedMilliseconds;
    }

    protected async Task InsertionSort()
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();

      for (var i = 0; i < GeneratedList.Count - 1; i++)
      {
        for (var j = i + 1; j > 0; j--)
        {
          if (GeneratedList[j - 1] > GeneratedList[j])
          {
            _left = j;
            _right = j - 1;
            (GeneratedList[_left], GeneratedList[_right]) = (GeneratedList[_right], GeneratedList[_left]);

            await RenderSelectedBars();
          }
        }
      }

      DeselectBars();

      watch.Stop();
      SortTimer = watch.ElapsedMilliseconds;
    }

    protected void MergeSortWrap(List<int> arr, int a, int b)
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();

      MergeSort(arr, a, b);
      GetPercentageArray(arr);

      DeselectBars();

      watch.Stop();
      SortTimer = watch.ElapsedMilliseconds;
    }

    private List<int> MergeSort(List<int> arr, int a, int b)
    {
      if (a < b)
      {
        var m = a + (b - a) / 2;

        MergeSort(arr, a, m);
        MergeSort(arr, m + 1, b);
        Merge(arr, a, b, m);
      }

      return arr;
    }

    private void Merge(List<int> arr, int a, int b, int m)
    {
      var leftSize = m - a + 1;
      var rightSize = b - m;

      var left = new List<int>(leftSize);
      var right = new List<int>(rightSize);

      for (var i = 0; i < leftSize; i++)
      {
        left.Add(arr[a + i]);
      }
      for (var i = 0; i < rightSize; i++)
      {
        right.Add(arr[m + 1 + i]);
      }

      var leftIndex = 0;
      var rightIndex = 0;
      var mergingArrIndex = a;

      // merge the smallest numbers first in both arrs
      while (leftIndex < leftSize && rightIndex < rightSize)
      {
        if (left[leftIndex] <= right[rightIndex])
        {
          arr[mergingArrIndex] = left[leftIndex];
          leftIndex++;
        }
        else
        {
          arr[mergingArrIndex] = right[rightIndex];
          rightIndex++;
        }

        mergingArrIndex++;
      }

      // if one side had more bigger numbers than the other there will be left overs
      // which are the biggest numbers. So they get added at the end.
      while (leftIndex < leftSize)
      {
        arr[mergingArrIndex] = left[leftIndex];
        leftIndex++;
        mergingArrIndex++;
      }
      while (rightIndex < rightSize)
      {
        arr[mergingArrIndex] = right[rightIndex];
        rightIndex++;
        mergingArrIndex++;
      }
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

            await RenderSelectedBars();
          }
        }
      }

      DeselectBars();

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
    #endregion

    #region Rendering Methods
    protected string StyleSelectBar(int n)
    {
      if (n == _left || n == _right)
      {
        return "background-color: red";
      }
      else if (n == _middle)
      {
        return "background-color: pink";
      }

      return "";
    }

    private async Task RenderSelectedBars()
    {
      GetBarProperties(_left);
      GetBarProperties(_right);
      if (DoAnimation)
      {
        StateHasChanged();
        var delay = IsFastAnimation ? 1 : 1000;
        await Task.Delay(delay);
      }
    }

    private void GetBarProperties(int i)
    {
      var num = GeneratedList[i];
      var percentageX = Math.Round(value: (double)1 / ArraySize * 100, 5);
      var percentageY = Math.Round(value: (double)num / MaxVal * 100, 5);

      RandomArrayPercentages[i] = new RenderedBar(percentageX, percentageY);
    }

    private void GetPercentageArray(List<int> arr)
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

    private void DeselectBars()
    {
      _left = -1;
      _right = -1;
      _middle = -1;
    }

    #endregion

  }
}
