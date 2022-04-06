using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;

/// <summary>
/// https://leetcode.com/problems/product-of-array-except-self/
/// Given an integer array nums, return an array answer such that answer[i] is equal to the product of all the elements of nums except nums[i].
/// The product of any prefix or suffix of nums is guaranteed to fit in a 32-bit integer.
/// You must write an algorithm that runs in O(n) time and without using the division operation.
/// </summary>
public class ProductOfArrayExceptSelf
{
    public static IEnumerable<object[]> TestCases => new[]
    {
        new object[]
        {
            new[] { 1, 2, 3, 4 },
            new[] { 24, 12, 8, 6 }
        },
        new object[]
        {
            new[] { -1, 1, 0, -3, 3 },
            new[] { 0, 0, 9, 0, 0 }
        },
    };

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Validate(int[] inputArray, int[] expectedArray)
    {
        new Solution()
            .ProductExceptSelf(inputArray)
            .Should()
            .BeEquivalentTo(expectedArray, o => o
                .WithStrictOrdering());
    }

    public class Solution
    {
        public int[] ProductExceptSelf(int[] nums)
        {
            /*
             * create two arrays
             * - one for all product before index
             * - one for all product after index
             * So result will be a product of two arrays
             */
            var before = new int[nums.Length];
            var after = new int[nums.Length];

            before[0] = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                before[i] = before[i - 1] * nums[i - 1];
            }

            after[nums.Length - 1] = 1;
            for (int i = nums.Length - 2; i >= 0; i--)
            {
                after[i] = after[i + 1] * nums[i + 1];
            }

            var result = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                result[i] = before[i] * after[i];
            }

            return result;
        }
    }
}