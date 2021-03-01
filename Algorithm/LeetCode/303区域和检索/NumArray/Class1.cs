using System;

/*
2020-03-01 JackLee
https://leetcode-cn.com/problems/range-sum-query-immutable

==========解法==========
1.最简单得遍历数组，根据下表求和即可，不包含任何技巧
2.看过题解后，看到一个技巧解法(前缀和解法)
    因为是求连续n个元素的和，哪我们可以在初始化数组的时候，直接求得前n个元素的和并将其保存在数组第n个位置
    当求解时，用array[j+1]-array[i]即可。

*/
namespace NumArray
{
    public class NumArray
    {
        int[] numArray;
        public NumArray(int[] nums)
        {
            numArray=new int[nums.Length];
            for(int i=0;i<nums.Length;i++)
            {
                numArray[i]=nums[i];
            }
        }

        public int SumRange(int i,int j)
        {
            int result=0;
            for(int k=i;k<=j;k++)
            {
                result+=numArray[k];
            }

            return result;
        }
    }

    public class NumArray2{
        int[] numsArray;
        public NumArray2(int[] nums)
        {
            numsArray=new int[nums.Length+1];//因为前n个元素的和存储到下一位
            for(int i=0;i<nums.Length;i++)
            {
                numsArray[i+1]=numsArray[i]+nums[i];
            }
        }

        public int SumRange(int i,int j)
        {
            //当要求解i-j的元素之和时，前j个元素（包括下标j）的和存储在numsArray[j+1]中，前i-1个元素存储在numaArray[i]中
            return numsArray[j+1]-numsArray[i];
        }
    }
}
