using System;
using Xunit;
using TwoSum;

namespace TwoSum.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 class1=new Class1();
            int[] m1=class1.FindIndexOfTwoSum(new int[]{2,7,11,15},9);
            Assert.Equal(new int[]{0,1},m1);
            int[] m2=class1.FindIndexOfTwoSum2(new int[]{3,3},6);
            Assert.Equal(new int[]{0,1},m2);
            int[] m3=class1.FindIndexOfTwoSum2(new int[]{2,7,11,15},9);
            Assert.Equal(new int[]{0,1},m3);
        }
    }
}
