using System;
using System.Collections.Generic;
using System.Threading;

namespace ThreadedMergeSort
{
    /// <summary>
    /// static class for to contain sorting algorithms
    /// </summary>
    static class Sorter
    {
        /// <summary>
        /// Performs a standard mergesort
        /// </summary>
        /// <param name="numbers">list of integers to sort</param>
        /// <returns>sorted list of integers</returns>
        public static List<int> MergeSort(List<int> numbers)
        {
            List<int> left, right;
            List<int> sorted = new List<int> { };

            if (numbers.Count <= 1)
            {
                sorted = numbers;
            }
            else
            {
                int midPoint = numbers.Count / 2;
                left = numbers.GetRange(0, midPoint);
                right = numbers.GetRange(midPoint, numbers.Count - midPoint);

                left = MergeSort(left);
                right = MergeSort(right);
                sorted = merge(left, right);
            }

            return sorted;
        }

        /// <summary>
        /// combines to lists of integers into one sorted list of integers
        /// </summary>
        /// <param name="left">left list of ints</param>
        /// <param name="right"> right list of ints</param>
        /// <returns>sorted combo of both lists</returns>
        private static List<int> merge(List<int> left, List<int> right)
        {
            List<int> merged = new List<int> { };
            int leftIndex = 0, rightIndex = 0, mergeIndex = 0;
 
            while (leftIndex < left.Count || rightIndex < right.Count)
            { 
                if (leftIndex < left.Count && rightIndex < right.Count)
                {
                    if (left[leftIndex] <= right[rightIndex])
                    {
                        merged.Add(left[leftIndex]);
                        leftIndex++;
                        mergeIndex++;
                    }
                    else
                    {
                        merged.Add(right[rightIndex]);
                        rightIndex++;
                        mergeIndex++;
                    }
                }
                else if (leftIndex < left.Count)
                {
                    merged.Add(left[leftIndex]);
                    leftIndex++;
                    mergeIndex++;
                }
                else if (rightIndex < right.Count)
                {
                    merged.Add(right[rightIndex]);
                    rightIndex++;
                    mergeIndex++;
                }

            }
            return merged;
        }
        /// <summary>
        /// Performs a mergesort by creating a thread for every recursive call
        /// </summary>
        /// <param name="numbers">list of integers to sort</param>
        /// <returns>sorted list of integers</returns>
        public static List<int> ThreadedMergeSort(List<int> numbers)
        {
            List<int> left, right;
            List<int> sorted = new List<int> { };

            if (numbers.Count <= 1)
            {
                sorted = numbers;
            }
            else
            {
                int midPoint = numbers.Count / 2;
                left = numbers.GetRange(0, midPoint);
                right = numbers.GetRange(midPoint, numbers.Count - midPoint);
                var leftThread = new Thread(new ThreadStart(delegate ()
                {
                    ThreadedMergeSort(left);
                }));
                var rightThread = new Thread(new ThreadStart(delegate ()
                {
                    ThreadedMergeSort(right);
                }));
                leftThread.Start();
                rightThread.Start();
                leftThread.Join();
                rightThread.Join();
                sorted = merge(left, right);
            }
            return sorted;
        }
    }
}
