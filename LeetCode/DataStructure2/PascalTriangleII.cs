using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;

/// <summary>
/// https://leetcode.com/problems/pascals-triangle-ii/
/// Given an integer rowIndex, return the rowIndex_th (0-indexed) row of the Pascal's triangle.
/// </summary>
public class PascalTriangleII
{
    public static IEnumerable<object[]> TestCases => new[]
    {
        new object[]
        {
            3,
            new[] { 1,3,3,1 }
        },
        new object[]
        {
            0,
            new[] { 1 }
        },
        new object[]
        {
            1,
            new[] { 1,1 }
        },
        new object[]
        {
            4,
            new[] { 1, 4, 6, 4, 1 }
        },
        new object[]
        {
            5,
            new[] { 1, 5, 10, 10, 5, 1 }
        },
    };

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Validate(int rowIndex, int[] result)
    {
        new Solution().GetRow(rowIndex).Should().BeEquivalentTo(result);
    }
    public class Solution 
    {
        public IList<int> GetRow(int rowIndex)
        {
            var start = new int[]
            {
                1
            };

            if (rowIndex == 0)
            {
                return start;
            }
            
            var current = Array.Empty<int>();

            for (var i = 1; i <= rowIndex; i++)
            {
                current = new int[i+1];
                current[0] = 1;
                current[i] = 1;
                
                for (var j = 1; j < i; j++)
                {
                    current[j] = start[j - 1] + start[j];
                }

                start = current;
            }

            return current;
        }
    }
}