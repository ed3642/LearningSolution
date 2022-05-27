﻿using LearningProjects.Utils;
using Microsoft.AspNetCore.Components;

namespace LearningProjects.Shared
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
    public List<int> RandomArray { get; set; } = null!;
    public List<Tuple<double, double>> RandomArrayPercentages { get; set; } = null!;

    protected override void OnInitialized()
    {
      NewArray(ArraySize, MinVal, MaxVal);
    }

    protected void BubbleSort()
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();

      for (var i = 0; i < RandomArrayPercentages.Count; i++)
      {
        for (var j = 0; j < RandomArrayPercentages.Count - 1; j++)
        {
          var left = RandomArrayPercentages[j].Item2;
          var right = RandomArrayPercentages[j + 1].Item2;

          if (right < left)
          {
            (RandomArrayPercentages[j], RandomArrayPercentages[j + 1]) = (RandomArrayPercentages[j + 1], RandomArrayPercentages[j]);
          }
        }
      }

      watch.Stop();
      SortTimer = watch.ElapsedMilliseconds;
    }
    protected void ResetArray()
    {
      GetPercentageArray();
    }

    protected void GetPercentageArray()
    {
      RandomArrayPercentages = new List<Tuple<double, double>>(ArraySize);
      var percentageX = Math.Round(value: (double)1 / ArraySize * 100, 5);

      foreach (var num in RandomArray)
      {
        var percentageY = Math.Round(value: (double)num / MaxVal * 100, 5);

        RandomArrayPercentages.Add(Tuple.Create(percentageX, percentageY));
      }
    }

    protected void CsharpSort()
    {
      var watch = System.Diagnostics.Stopwatch.StartNew();

      RandomArrayPercentages.Sort();

      watch.Stop();
      SortTimer = watch.ElapsedMilliseconds;
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

      RandomArray = MathUtils.GenRandomInts(size, min, max);
      GetPercentageArray();
    }
  }
}