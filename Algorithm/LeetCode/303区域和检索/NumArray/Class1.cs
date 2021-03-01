using System;

/*
2020-03-01 JackLee
========题目=======
给定一个整数数组  nums，求出数组从索引 i 到 j（i ≤ j）范围内元素的总和，包含 i、j 两点。

实现 NumArray 类：

NumArray(int[] nums) 使用数组 nums 初始化对象
int sumRange(int i, int j) 返回数组 nums 从索引 i 到 j（i ≤ j）范围内元素的总和，包含 i、j 两点（也就是 sum(nums[i], nums[i + 1], ... , nums[j])）

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/range-sum-query-immutable
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
==============

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
