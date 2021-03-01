using System;
using System.Collections.Generic;

/*
2021-2-20 JackLee
https://leetcode-cn.com/problems/two-sum

*/
namespace TwoSum
{
    public class Class1
    {
        public int[] FindIndexOfTwoSum(int[] array,int target)
        {
            for(int i=0;i<array.Length;i++)
            {
                for(int j=i+1;j<array.Length;i++)
                {
                    if(array[i]+array[j]==target)
                    {
                        return new int[] { i, j };
                    }
                }
            }

            return new int[0];
        }


        public int[] FindIndexOfTwoSum2(int[] array,int target)
        {//也可以用list,但是空间复杂度都比较高，如何才能降低内存占用呢？
            Dictionary<int, int> numAndIndex = new Dictionary<int, int>();
            for(int i=0;i<array.Length;i++)
            {
                if(numAndIndex.ContainsKey(array[i]))
                {
                    //这里其实只要进来，肯定是i大，因此不用进行比较
                    return new int[] { numAndIndex[array[i]], i };
                }
                numAndIndex.Add(target-array[i],i);
            }

            return new int[0];
        }
    }
}
