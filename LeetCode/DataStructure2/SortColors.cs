using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;

/// <summary>
/// https://leetcode.com/problems/sort-colors/
/// Given an array nums with n objects colored red, white, or blue, sort them in-place so that objects of the same color are adjacent, with the colors in the order red, white, and blue.
/// We will use the integers 0, 1, and 2 to represent the color red, white, and blue, respectively.
/// You must solve this problem without using the library's sort function.
/// </summary>
/// <remarks>
/// Constraints:
///   n == nums.length
///   1 <= n <= 300
///   nums[i] is either 0, 1, or 2
/// </remarks>
public class SortColors
{
    
    public static IEnumerable<object[]> TestCases => new[]
    {
        new object[] { new[] { 2,0,2,1,1,0}, new[]{0,0,1,1,2,2} },
        new object[] { new[] { 2,0,1 }, new[]{0,1,2} },
    };

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Validate(int[] array, int[] result)
    {
        new Solution().SortColors(array);
        array.Should().BeEquivalentTo(result);
    }

    public class Solution 
    {
        public void SortColors(int[] nums)
        {
            var countDict = new int[3];

            for (var i = 0; i < nums.Length; i++)
            {
                countDict[nums[i]]++;
            }

            for (var i = 0; i < nums.Length; i++)
            {
                if (countDict[0] > 0)
                {
                    nums[i] = 0;
                    countDict[0]--;
                }
                else if (countDict[1] > 0)
                {
                    nums[i] = 1;
                    countDict[1]--;
                }
                else if (countDict[2] > 0)
                {
                    nums[i] = 2;
                    countDict[2]--;
                }
            }
        }
    }
}