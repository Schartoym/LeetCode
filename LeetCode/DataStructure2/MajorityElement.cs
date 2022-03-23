using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;

/// <summary>
/// https://leetcode.com/problems/majority-element/
/// Given an array nums of size n, return the majority element.
/// The majority element is the element that appears more than ⌊n / 2⌋ times.
/// You may assume that the majority element always exists in the array.
/// </summary>
/// <remarks>
/// Constraints:
///  n == nums.length
///  1 <= n <= 5 * 104
///  -10^9 <= nums[i] <= 10^9
/// </remarks>
public class MajorityElement
{
    public static IEnumerable<object[]> TestCases => new[]
    {
        new object[] { new[] { 3,2,3}, 3 },
        new object[] { new[] { 2,2,1,1,1,2,2}, 2 },
    };

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Validate(int[] array, int result)
    {
        new Solution().MajorityElement(array).Should().Be(result);
    }

    public class Solution 
    {
        public int MajorityElement(int[] nums)
        {
            var countDict = new Dictionary<int, int>();

            foreach (var num in nums)
            {
                if (countDict.ContainsKey(num))
                {
                    countDict[num]++;
                }
                else
                {
                    countDict.Add(num, 1);
                }
            }

            var major = nums.Length / 2;

            foreach (var key in countDict.Keys)
            {
                if (countDict[key] > major)
                {
                    return key;
                }
            }

            return 0;
        }
    }
}