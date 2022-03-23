using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;

/// <summary>
/// https://leetcode.com/problems/3sum/
/// Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]]
/// such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.
/// Notice that the solution set must not contain duplicate triplets.
/// </summary>
public class ThreeSum
{
    public static IEnumerable<object[]> TestCases => new[]
    {
        new object[] { new[] { -1, 0, 1, 2, -1, -4 }, new[] { new[] { -1, -1, 2 }, new[] { -1, 0, 1 } } },
        new object[] { Array.Empty<int>(), Array.Empty<int[]>() },
        new object[] { new[] {0}, Array.Empty<int[]>() },
    };

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Validate(int[] array, int[][] result)
    {
        new Solution().ThreeSum(array).Should().BeEquivalentTo(result);
    }
    
    public class Solution 
    {
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            var result = new List<IList<int>>();
            
            Array.Sort(nums);

            for (var i = 0; i < nums.Length-2; i++)
            {
                //Skipping duplicate numbers
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }

                var left = i + 1;
                var right = nums.Length - 1;

                while (left < right)
                {
                    if (nums[i] + nums[left] + nums[right] == 0)
                    {
                        result.Add(new List<int>{nums[i], nums[left], nums[right]});

                        left++;

                        //Skipping duplicate for second numbere
                        while (nums[left] == nums[left-1] && left < right)
                        {
                            left++;
                        }

                        right--;
                    }
                    else if (nums[i] + nums[left] + nums[right] < 0)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }    
            }

            return result;
        }
    }
}