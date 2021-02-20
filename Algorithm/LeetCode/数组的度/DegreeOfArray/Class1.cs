using System;
using System.Collections.Generic;


/*2020-02-20
 * link:https://leetcode-cn.com/problems/degree-of-an-array/
 * 
 * 
 * 思路：
 * 要求数组度相同的最小连续子数组的长度，可以直接遍历数组得到每个非重复元素出现的次数，同时记录元素第一次出现和最后一次出现的位置
 * 
 * 存储：
 *      1.可以通过字典来存储，key存储非重复元素，3个元素的数组作为value，分别表示元素出现次数、第一次出现位置及最后一次出现位置
 *      2.也可以用多个数组，分别存储非重复元素、出现次数和第一次出现位置及最后一次出现位置。
 * 
 * 
 */
namespace DegreeOfArray
{
    public class Class1
    {
        public int FindShortestSubArray(int[] nums)
        {
            Dictionary<int, int[]> keyValues = new Dictionary<int, int[]>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (keyValues.ContainsKey(nums[i]))
                {
                    keyValues[nums[i]][0]++;
                    keyValues[nums[i]][2] = i;
                }
                else
                {
                    keyValues.Add(nums[i], new int[3] { 1, i, i });
                }
            }

            int maxCount = 0;
            int minLength = 99999;
            foreach (var item in keyValues)
            {
                if (item.Value[0]>maxCount)
                {
                    maxCount = item.Value[0];
                    minLength = item.Value[2] - item.Value[1];
                }
                else if (item.Value[0]==maxCount)
                {
                    if (item.Value[2]-item.Value[1]<minLength)
                    {
                        minLength = item.Value[2] - item.Value[1];
                    }
                }
            }
           
            return minLength + 1;
        }
    }
}
