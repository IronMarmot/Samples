using System;
using Xunit;
using ToeplitzMatrix;

namespace ToeplitzMatrix.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 class1=new Class1();
            bool b1=class1.IsToeplitzMatrix(new int[3][]{new int[4]{1,2,3,4},new int[4]{5,1,2,3},new int[4]{9,5,1,2}});
            Assert.True(b1);

            bool b2=class1.IsToeplitzMatrix(new int[2][]{new int[2]{1,2},new int[2]{2,2}});
            Assert.False(b2);
        }
    }
}
