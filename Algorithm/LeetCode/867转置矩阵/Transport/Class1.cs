using System;
/*
2021-02-25 JackLee
https://leetcode-cn.com/problems/transpose-matrix/

转置矩阵
对行索引和列索引进行互换。可以理解为改变数组的遍历顺序，按列进行遍历即可。
初始化一个数组，行索引和列索引分别为给定数组的列索引和行索引
循环遍历数组，先遍历给定数组的列，再遍历给定数组的行即可。

*/
namespace Transport
{
    public class Class1
    {
        public int[][] Transport(int[][] matrix)
        {
            if(matrix==null)
                return matrix;

            int len_row=matrix.Length;
            int len_column=matrix[0].Length;
            int[][] array=new int[len_column][];

            for(int i=0;i<len_column;i++)
            {
                int[] temp=new int[len_row];
                for(int j=0;j<len_row;j++)
                {
                    temp[j]=matrix[j][i];
                }
                array[i]=temp;
            }

            return array;
        }

        public int[][] Transport2(int[][] matrix)
        {
            if(matrix==null)
                return matrix;
            
            int len_row=matrix.Length;
            int len_column=matrix[0].Length;

            int[][] array=new int[len_column][];
            for(int i=0;i<len_column;i++)
            {
                array[i]=new int[len_row];
            }

            for(int i=0;i<len_column;i++)
            {
                for(int j=0;j<len_row;j++)
                {
                    array[i][j]=matrix[j][i];
                }
            }

            return array;
        }
    }
}
