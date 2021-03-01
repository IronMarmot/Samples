using System;
/*
2021-02-24 JackLee
https://leetcode-cn.com/problems/flipping-an-image/

按要求，先倒序，再反转元素。
其实，在解析过程中，该先后并不限制，可以再一次循环中处理完成。

第二种解法：
用2个指针，分别指向第一个和最后一个元素，移动指针直到相遇
另外，求反转的思想确实优秀，因为是先倒叙，在反转，因此，如果前后2个元素不同，那么经过倒叙-反转后，其实和现在的值是相同的，即可以不处理，
要处理的是前后值相同的元素，（这样一来，比第一种方法要少好多次数据交换操作）求反转的话，可以用1-current或者直接用异或（^1）
异或：相同为0，不同为1，因此这里与 ^1进行计算，=1则为0，!=1则为1；
*/
namespace FlipAndInvertImage
{
    public class Class1
    {
        public int[][] FlipAndInvertImage(int[][] A) {
            int len=A.Length;
            for(int i=0;i<len;i++)
            {
                int len2=A[i].Length;
                for(int j=0;j<(len2+1)/2;j++)
                {
                    if(j!=len2-j-1)
                    {
                        int tmp=A[i][j]==1?0:1;
                        A[i][j]=A[i][len2-j-1]==1?0:1;
                        A[i][len2-j-1]=tmp;
                    }
                    else
                    {
                        A[i][j]=A[i][j]==1?0:1;
                    }
                }
            }

            return A;
        }
    
        public int[][] FlipAndInvertImage2(int[][] A)
        {
            int len = A.Length;
            for (int i = 0; i < len; i++)
            {
                int left = 0;
                int rigth = A[i].Length - 1;
                while (left < rigth)
                {
                    if (A[i][left] == A[i][rigth])
                    {
                        A[i][left] ^= 1;
                        A[i][rigth] ^= 1;
                    }
                    left++;
                    rigth--;
                }
                if (left == rigth)
                    A[i][left] ^= 1;
            }
            return A;
        }
    }
}
