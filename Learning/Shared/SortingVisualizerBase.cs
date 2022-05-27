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
        public List<int> RandomArray { get; set; } = null!;
        public List<Tuple<double, double>> RandomArrayPercentages { get; set; } = null!;

        private List<int> _originalOrder = new List<int>();

        protected override void OnInitialized()
        {
            NewArray(ArraySize, MinVal, MaxVal);
        }

        protected void MergeSort()
        {

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
        protected void CsharpSort()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            RandomArray.Sort();
            GetPercentageArray(RandomArray);

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

            RandomArray = MathUtils.GenRandomInts(size, min, max);
            _originalOrder = RandomArray.ToList(); // copy by value
            GetPercentageArray(RandomArray);
        }
    }
}
