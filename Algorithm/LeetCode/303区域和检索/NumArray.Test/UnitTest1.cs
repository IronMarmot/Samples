using System;
using Xunit;

namespace NumArray.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            NumArray array=new NumArray(new int[]{-2, 0, 3, -5, 2, -1});
            int result=array.SumRange(0, 2);
            Assert.Equal(1,result);

            NumArray2 array2=new NumArray2(new int[]{-2, 0, 3, -5, 2, -1});

            int result2=array2.SumRange(2,5);
            Assert.Equal(result2,-1);
        }
    }
}
