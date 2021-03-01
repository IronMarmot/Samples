using System;

/*
2021-02-22 JackLee
https://leetcode-cn.com/problems/toeplitz-matrix/


*/
namespace ToeplitzMatrix
{
    public class Class1
    {
        public bool IsToeplitzMatrix(int[][] matrix)
        {
            bool result=true;
            for(int i=0;i<matrix.Length-1;i++)
            {
                for(int j=0;j<matrix[i].Length-1;j++)
                {
                    if(matrix[i][j]!=matrix[i+1][j+1])
                    {
                        result = false;
                    }
                }
            }

            return result;
        }
    }
}
