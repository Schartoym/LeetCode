using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LeetCode;

/// <summary>
/// Given two sorted arrays nums1 and nums2 of size m and n respectively, return the median of the two sorted arrays.
/// The overall run time complexity should be O(log (m+n)).
/// </summary>
public class MedianTwoSortedArrays
{
    public static IEnumerable<object[]> FindMedianSortedArraysTestCases => new[]
    {
        new object[] { new[] { 1, 3 }, new[] { 2 }, 2 },
        new object[] { new[] { 1, 2 }, new[] { 3, 4 }, 2.5 },
    };

    [Theory]
    [MemberData(nameof(FindMedianSortedArraysTestCases))]
    public void Validate(int[] nums1, int[] nums2, double result)
    {
        new Solution().FindMedianSortedArrays(nums1, nums2).Should().Be(result);
    }

    public static IEnumerable<object[]> FindMedianSortedArrayTestCases => new[]
    {
        new object[] { new[] { 1, 2, 3 }, 2 },
        new object[] { new[] { 1, 2, 3, 4 }, 2.5 },
    };
  
    [Theory]
    [MemberData(nameof(FindMedianSortedArrayTestCases))]
    public void ValidateFindMedianInArray(int[] array, double result)
    {
        new Solution().FindMedianSortedArray(array).Should().Be(result);
    }

    public static IEnumerable<object[]> MergeSortedArrayTestCases => new[]
    {
        new object[] { new[] { 1, 3 }, new[] { 2 }, new[] { 1, 2, 3 } },
        new object[] { new[] { 1, 2 }, new[] { 3, 4 }, new[] { 1, 2, 3, 4 } },
        new object[] { new[] { 1, 2, 3, 3 }, new[] { 3, 4 }, new[] { 1, 2, 3, 3, 3, 4 } },
        new object[] { new[] { 1, 2, 3, 4 }, new[] { 3, 4 }, new[] { 1, 2, 3, 3, 4, 4 } },
        new object[] { new[] { 1, 2, 3, 4 }, Array.Empty<int>(), new[] { 1, 2, 3, 4 } },
    };
    
    [Theory]
    [MemberData(nameof(MergeSortedArrayTestCases))]
    public void MergeToSortedArrays(int[] firstArray, int[] secondArray, int[] result)
    {
        new Solution().MergeToSortedArrays(firstArray, secondArray).Should().BeEquivalentTo(result);
    }
    
    public class Solution
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var merged = MergeToSortedArrays(nums1, nums2);

            return FindMedianSortedArray(merged);
        }

        /// <summary>
        /// Finding median in sorted array
        /// </summary>
        public double FindMedianSortedArray(int[] sortedArray)
        {
            if (sortedArray.Length % 2 == 0)
            {
                return (sortedArray[sortedArray.Length / 2 - 1] + sortedArray[sortedArray.Length / 2]) / 2.0;
            }

            return sortedArray[sortedArray.Length / 2];
        }

        /// <summary>
        /// Merge two sorted arrays
        /// </summary>
        public int[] MergeToSortedArrays(int[] nums1, int[] nums2)
        {
            var totalItems = nums1.Length + nums2.Length;
            var merged = new int[totalItems];

            int i = 0, j = 0;

            for (var viewed = 0; viewed < totalItems; viewed++)
            {
                if (i >= nums1.Length)
                {
                    merged[viewed] = nums2[j];
                    j++;
                    continue;
                }

                if (j >= nums2.Length)
                {
                    merged[viewed] = nums1[i];
                    i++;
                    continue;
                }

                var currentInFirst = nums1[i];
                var currentInSecond = nums2[j];

                int itemToAdd;
                if (currentInFirst < currentInSecond)
                {
                    itemToAdd = currentInFirst;
                    i++;
                }
                else
                {
                    itemToAdd = currentInSecond;
                    j++;
                }

                merged[viewed] = itemToAdd;
            }

            return merged;
        }
    }
}