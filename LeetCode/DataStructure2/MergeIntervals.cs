using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;
/// <summary>
/// https://leetcode.com/problems/merge-intervals/
/// Given an array of intervals where intervals[i] = [start_i, end_i],
/// merge all overlapping intervals, and return an array of the non-overlapping intervals that cover all the intervals in the input.
/// </summary>
/// <remarks>
/// Constraints:
///   1 <= intervals.length <= 10^4
///   intervals[i].length == 2
///   0 <= start_i <= end_i <= 10^4
/// </remarks>
public class MergeIntervals
{
    public static IEnumerable<object[]> TestCases => new[]
    {
        new object[]
        {
            new[] { new[] { 1, 3 }, new[] { 2, 6 }, new[] { 8, 10 }, new[] { 15, 18 } },
            new[] { new[] { 1, 6 }, new[] { 8, 10 }, new[] { 15, 18 } }
        },
        new object[]
        {
            new[] { new[] { 1, 4 }, new[] { 4, 5 } },
            new[] { new[] { 1, 5 } }
        },
        new object[]
        {
            new[] { new[] { 1, 4 }, new[] { 2, 3 } },
            new[] { new[] { 1, 4 } }
        },
    };

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Validate(int[][] array, int[][] result)
    {
        new Solution().Merge(array).Should().BeEquivalentTo(result);
    }
    public class Solution {
        public int[][] Merge(int[][] intervals)
        {
            var result = new List<int[]>(intervals.Length);

            intervals = intervals.OrderBy(i => i[0]).ToArray();

            int currentIntervalStart = intervals[0][0], currentIntervalEnd = intervals[0][1];
            
            for (var i = 1; i < intervals.Length; i++)
            {
                var interval = intervals[i];

                var start = interval[0];
                var end = interval[1];
                
                if (start > currentIntervalEnd)
                {
                    result.Add(new []{currentIntervalStart, currentIntervalEnd});
                    currentIntervalStart =  start;
                    currentIntervalEnd = end;
                    continue;
                }

                if (currentIntervalEnd < end)
                {
                    currentIntervalEnd = end;
                }
            }

            result.Add(new[] { currentIntervalStart, currentIntervalEnd });
            
            return result.ToArray();
        }
    }
}