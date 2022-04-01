using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LeetCode.DataStructure2;

/// <summary>
/// https://leetcode.com/problems/search-a-2d-matrix-ii/
/// Write an efficient algorithm that searches for a value target in an m x n integer matrix matrix. This matrix has the following properties:
/// - Integers in each row are sorted in ascending from left to right.
/// - Integers in each column are sorted in ascending from top to bottom.
/// </summary>
public class Search2DMatrix
{
    public static IEnumerable<object[]> SearchMatrixTestCases => new[]
    {
        new object[]
        {
            new[]
            {
                new[] { 1, 4, 7, 11, 15 },
                new[] { 2, 5, 8, 12, 19 },
                new[] { 3, 6, 9, 16, 22 },
                new[] { 10, 13, 14, 17, 24 },
                new[] { 18, 21, 23, 26, 30 },
            },
            5,
            true
        },
        new object[]
        {
            new[]
            {
                new[] { 1, 4, 7, 11, 15 },
                new[] { 2, 5, 8, 12, 19 },
                new[] { 3, 6, 9, 16, 22 },
                new[] { 10, 13, 14, 17, 24 },
                new[] { 18, 21, 23, 26, 30 },
            },
            20,
            false
        }
        ,
        new object[]
        {
            new[]
            {
                new[] { 1, 4, 7, 11, 15 },
                new[] { 2, 5, 8, 12, 19 },
                new[] { 3, 6, 9, 16, 22 },
                new[] { 10, 13, 14, 17, 24 },
                new[] { 18, 21, 23, 26, 30 },
            },
            1,
            true
        },
        new object[]
        {
            new[]
            {
                new[] { 1, 4, 7, 11, 15 },
                new[] { 2, 5, 8, 12, 19 },
                new[] { 3, 6, 9, 16, 22 },
                new[] { 10, 13, 14, 17, 24 },
                new[] { 18, 21, 23, 26, 30 },
            },
            30,
            true
        },
        new object[]
        {
            new[]
            {
                new[] { 1, 1 }
            },
            30,
            false
        },
        new object[]
        {
            new[]
            {
                new[] { -1 },
                new[] { -1 },
            },
            2,
            false
        },
        new object[]
        {
            new[]
            {
                new[] { -1 },
                new[] { -1 },
            },
            -1,
            true
        },
    };

    [Theory]
    [MemberData(nameof(SearchMatrixTestCases))]
    public void ValidateSearchMatrix(int[][] matrix, int target, bool expectedResult)
    {
        new Solution().SearchMatrix(matrix, target).Should().Be(expectedResult);
    }  

    public class Solution
    {
        public bool SearchMatrix(int[][] matrix, int target)
        {
            for (var i = 0; i < matrix.Length; i++)
            {
                var foundVertically = matrix[i].Length > i && SearchInMatrix(matrix, target, i, true);

                if (foundVertically)
                {
                    return true;
                }


                var foundHorizontally = matrix[0].Length > i && SearchInMatrix(matrix, target, i, false);

                if (foundHorizontally)
                {
                    return true;
                }
            }

            return false;
        }

        private bool SearchInMatrix(int[][] matrix, int target, int index, bool vertically)
        {
            var start = 0;
            var end = (vertically ? matrix.Length : matrix[index].Length) - 1;

            while (start <= end)
            {
                var medianIndex = (start + end) / 2;

                if (vertically)
                {
                    if (matrix[medianIndex][index] > target)
                    {
                        end = medianIndex - 1;
                    }
                    else if (matrix[medianIndex][index] < target)
                    {
                        start = medianIndex + 1;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (matrix[index][medianIndex] > target)
                    {
                        end = medianIndex - 1;
                    }
                    else if (matrix[index][medianIndex] < target)
                    {
                        start = medianIndex + 1;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}