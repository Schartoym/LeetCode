using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;

/// <summary>
/// https://leetcode.com/problems/single-number/
/// Given a non-empty array of integers nums, every element appears twice except for one. Find that single one.
/// You must implement a solution with a linear runtime complexity and use only constant extra space.
/// </summary>
/// <remarks>
/// Constraints:
/// 1 <= nums.length <= 3 * 10^4
/// -3 * 10^4 <= nums[i] <= 3 * 10^4
/// Each element in the array appears twice except for one element which appears only once.
/// </remarks>
public class SingleNumber
{
    public static IEnumerable<object[]> TestCases => new[]
    {
        new object[] { new[] { 2, 2, 1}, 1 },
        new object[] { new[] { 4,1,2,1,2 }, 4 },
        new object[] { new[] { 1 }, 1 },
        new object[] { new[] { 1,1,100, 100, 200 }, 200},
    };

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Validate(int[] array, int result)
    {
        new Solution().SingleNumber(array).Should().Be(result);
    }
    
    public class Solution
    {
        private static readonly int Min = -3*(int)Math.Pow(10, 4);
        private static readonly int Max = 3*(int)Math.Pow(10, 4);
        
        public int SingleNumber(int[] nums)
        {
            /*
             * We have strong constraint for numbers in array, so we can create a array for counting.
             *
             * Main idea: https://en.wikipedia.org/wiki/Counting_sort
             */
            var countArray = new int[Math.Abs(Min) + Math.Abs(Max) + 1];

            foreach (var num in nums)
            {
                var index = GetIndexForNum(num);
                countArray[index] += 1;
            }

            for (var i = 0; i < countArray.Length; i++)
            {
                if (countArray[i] == 1)
                {
                    return GetNumForIndex(i);
                }
            }

            return 0;
        }

        public int GetIndexForNum(int num)
        {
            return Math.Abs(Min) + num;
        }

        public int GetNumForIndex(int index)
        {
            return index-Math.Abs(Min);
        }
    }
}