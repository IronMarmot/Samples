using System;
using Xunit;
using DegreeOfArray;

namespace DegreeOfArray.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 class1=new Class1();
            int len=class1.FindShortestSubArray(new int[]{1,2,2,1,3});
            Assert.Equal(2,len);
        }

        [Theory]
        [InlineData(new int[]{1,2,2,3,1,4,2})]
        public void Test2(int[] value)
        {
            Class1 class1=new Class1();
            int len=class1.FindShortestSubArray(value);
            Assert.Equal(6, len);
        }
    }
}
