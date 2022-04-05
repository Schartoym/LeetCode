using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;

/// <summary>
/// https://leetcode.com/problems/increasing-triplet-subsequence/
/// Given an integer array nums, return true if there exists a triple of indices (i, j, k)
/// such that i < j < k and nums[i] < nums[j] < nums[k].
/// If no such indices exists, return false.
/// </summary>
public class IncreasingTripletSubsequence
{
    public static IEnumerable<object[]> TestCases => new[]
    {
        new object[]
        {
            new[] { 1,2,3,4,5},
            true
            //Any triplet where i < j < k is valid.
        },
        new object[]
        {
            new[] { 5,4,3,2,1},
            false
            //No triplet exists.
        },
        new object[]
        {
            new[] {2,1,5,0,4,6},
            true
            //The triplet (3, 4, 5) is valid because nums[3] == 0 < nums[4] == 4 < nums[5] == 6
        },
    };

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Validate(int[] nums, bool existsTriplet)
    {
        new Solution().IncreasingTriplet(nums).Should().Be(existsTriplet);
    }
    
    public class Solution 
    {
        public bool IncreasingTriplet(int[] nums) 
        {
            if (nums.Length < 3)
            {
                return false;
            }

            var first = int.MaxValue;
            var second = int.MaxValue;

            foreach (var num in nums)
            {
                if (num <= first)
                {
                    first = num;
                }
                else if(num <= second)
                {
                    second = num;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}