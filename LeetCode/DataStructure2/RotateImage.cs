using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace LeetCode.DataStructure2;

/// <summary>
/// https://leetcode.com/problems/rotate-image/
/// You are given an n x n 2D matrix representing an image, rotate the image by 90 degrees (clockwise).
/// You have to rotate the image in-place, which means you have to modify the input 2D matrix directly.
/// DO NOT allocate another 2D matrix and do the rotation
/// </summary>
public class RotateImage
{
    private readonly ITestOutputHelper _outputHelper;

    public RotateImage(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    public static IEnumerable<object[]> RotateTestCases => new[]
    {
        new object[]
        {
            new[]
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 },
                new[] { 7, 8, 9 }
            },
            new[]
            {
                new[] { 7, 4, 1 },
                new[] { 8, 5, 2 },
                new[] { 9, 6, 3 }
            }
        },
        new object[]
        {
            new[]
            {
                new[] { 5, 1, 9, 11 },
                new[] { 2, 4, 8, 10 },
                new[] { 13, 3, 6, 7 },
                new[] { 15, 14, 12, 16 }
            },
            new[]
            {
                new[] { 15, 13, 2, 5 },
                new[] { 14, 3, 4, 1 },
                new[] { 12, 6, 8, 9 },
                new[] { 16, 7, 10, 11 }
            }
        },
    };
    
    public static IEnumerable<object[]> TransposeTestCases => new[]
    {
        new object[]
        {
            new[]
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 },
                new[] { 7, 8, 9 }
            },
            new[]
            {
                new[] { 1, 4, 7 },
                new[] { 2, 5, 8 },
                new[] { 3, 6, 9 }
            }
        },
        new object[]
        {
            new[]
            {
                new[] { 5, 1, 9, 11 },
                new[] { 2, 4, 8, 10 },
                new[] { 13, 3, 6, 7 },
                new[] { 15, 14, 12, 16 }
            },
            new[]
            {
                new[] { 5, 2, 13, 15 },
                new[] { 1, 4, 3, 14 },
                new[] { 9, 8, 6, 12 },
                new[] { 11, 10, 7, 16 }
            }
        },
    };
    
    public static IEnumerable<object[]> FlipVerticallyTestCases => new[]
    {
        new object[]
        {
            new[]
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 },
                new[] { 7, 8, 9 }
            },
            new[]
            {
                new[] { 3, 2, 1 },
                new[] { 6, 5, 4 },
                new[] { 9, 8, 7 }
            }
        },
        new object[]
        {
            new[]
            {
                new[] { 5, 1, 9, 11 },
                new[] { 2, 4, 8, 10 },
                new[] { 13, 3, 6, 7 },
                new[] { 15, 14, 12, 16 }
            },
            new[]
            {
                new[] { 11, 9, 1, 5 },
                new[] { 10, 8, 4, 2 },
                new[] { 7, 6, 3, 13 },
                new[] { 16, 12, 14, 15 }
            }
        },
    };
    
    [Theory]
    [MemberData(nameof(RotateTestCases))]
    public void ValidateRotate(int[][] array, int[][] expected)
    {
        WriteMatrix("Initial", array);

        new Solution().Rotate(array);
        
        WriteMatrix("Rotated", array);

        array.Should().BeEquivalentTo(expected, o => o
            .WithStrictOrdering());
    }

    [Theory]
    [MemberData(nameof(TransposeTestCases))]
    public void ValidateTranspose(int[][] array, int[][] expected)
    {
        WriteMatrix("Initial", array);

        new Solution().Transpose(array);
        
        WriteMatrix("Transposed", array);

        array.Should().BeEquivalentTo(expected, o => o
            .WithStrictOrdering());
    }  
    
    [Theory]
    [MemberData(nameof(FlipVerticallyTestCases))]
    public void ValidateFlipVertically(int[][] array, int[][] expected)
    {
        WriteMatrix("Initial", array);
        
        new Solution().FlipVertically(array);
        
        WriteMatrix("Flipped", array);
        
        array.Should().BeEquivalentTo(expected, o => o
            .WithStrictOrdering());
        
    }

    public class Solution
    {
        public void Rotate(int[][] matrix)
        {
            //Main idea - transpose matrix, then flip vertically
            
            //first transpose matrix
            Transpose(matrix);
            
            //then flip
            FlipVertically(matrix);
        }

        public void Transpose(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    // ReSharper disable once SwapViaDeconstruction
                    var tmp = matrix[i][j];
                    matrix[i][j] = matrix[j][i];
                    matrix[j][i] = tmp;
                }
            }
        }

        public void FlipVertically(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                var rowLength = matrix[i].Length;
                
                for (int j = 0; j < rowLength/2; j++)
                {
                    // ReSharper disable once SwapViaDeconstruction
                    var tmp = matrix[i][j];

                    matrix[i][j] = matrix[i][rowLength - j - 1];
                    matrix[i][rowLength - j - 1] = tmp;
                }
            }
        }
    }
    
    private void WriteMatrix(string title, int[][] matrix)
    {
        _outputHelper.WriteLine(title);
        var sb = new StringBuilder();

        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                sb.Append($"{matrix[i][j]}\t");
            }

            sb.AppendLine();
        }
        
        _outputHelper.WriteLine(sb.ToString());
    }
}