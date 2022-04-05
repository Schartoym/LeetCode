using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;

/// <summary>
/// https://leetcode.com/problems/non-overlapping-intervals/
/// Given an array of intervals intervals where intervals[i] = [start_i, end_i],
/// return the minimum number of intervals you need to remove to make the rest of the intervals non-overlapping.
/// </summary>
public class NonOverlappingIntervals
{
    public static IEnumerable<object[]> TestCases => new[]
    {
        new object[]
        {
            new[] { new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 }, new[] { 1, 3 } },
            1
            //[1,3] can be removed and the rest of the intervals are non-overlapping.
        },
        new object[]
        {
            new[] { new[] { 1, 2 }, new[] { 1, 2 }, new[] { 1, 2 } },
            2
            //You need to remove two [1,2] to make the rest of the intervals non-overlapping.
        },
        new object[]
        {
            new[] { new[] { 1, 2 }, new[] { 2, 3 } },
            0
            //You don't need to remove any of the intervals since they're already non-overlapping.
        },
        new object[]
        {
            new[]
            {
                new[] { -52, 31 }, new[] { -73, -26 }, new[] { 82, 97 }, new[] { -65, -11 }, new[] { -62, -49 },
                new[] { 95, 99 }, new[] { 58, 95 }, new[] { -31, 49 }, new[] { 66, 98 }, new[] { -63, 2 },
                new[] { 30, 47 }, new[] { -40, -26 }
            },
            7
        },
    };

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Validate(int[][] intervals, int expectedResult)
    {
        new Solution().EraseOverlapIntervals(intervals).Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(new[] { 1, 3 }, new[] { 1, 2 }, true)]
    [InlineData(new[] { 1, 2 }, new[] { 1, 2 }, true)]
    [InlineData(new[] { 1, 4 }, new[] { 2, 3 }, true)]
    [InlineData(new[] { 1, 4 }, new[] { 5, 6 }, false)]
    [InlineData(new[] { 1, 4 }, new[] { 4, 6 }, false)]
    [InlineData(new[] { 1, 4 }, new[] { 3, 6 }, true)]
    public void IsOverlap(int[] one, int[] another, bool isOverlap)
    {
        new Solution().IsOverlap(one, another).Should().Be(isOverlap);
    }

    public class Solution
    {

        public int EraseOverlapIntervals(int[][] intervals)
        {
            //Sort intervals by end, then by length
            var sortedIntervals = intervals
                .OrderBy(i => i[1])
                .ThenBy(i => Math.Abs(i[1] - i[0]))
                .ToArray();

            var removedIntervals = 0;


            var lastInterval = sortedIntervals[0];
            for (int i = 1; i < sortedIntervals.Length; i++)
            {
                var currentInterval = sortedIntervals[i];

                if (IsOverlap(lastInterval, currentInterval))
                {
                    removedIntervals++;
                }
                else
                {
                    lastInterval = currentInterval;
                }
            }

            return removedIntervals;
        }

        public bool IsOverlap(int[] one, int[] another)
        {
            int[] first, second;

            if (one[0] < another[0])
            {
                first = one;
                second = another;
            }
            else
            {
                first = another;
                second = one;
            }

            if (first[0] == second[0])
            {
                return true;
            }

            if (first[1] == second[1])
            {
                return true;
            }

            if (first[1] > second[0])
            {
                return true;
            }

            if (first[1] >= second[1])
            {
                return true;
            }

            return false;
        }
    }
}