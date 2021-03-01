using System;
using Xunit;
using Transport;

namespace Transport.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 class1=new Class1();
            int[][] array=class1.Transport(new int[3][]{new int[3]{1,2,3},new int[3]{4,5,6},new int[3]{7,8,9}});
            Assert.Equal(new int[3][]{new int[3]{1,4,7},new int[3]{2,5,8},new int[3]{3,6,9}},array);

            int[][] array1=class1.Transport(new int[2][]{new int[3]{1,2,3},new int[3]{4,5,6}});
            Assert.Equal(new int[3][]{new int[2]{1,4},new int[2]{2,5},new int[2]{3,6}},array1);
        }
    }
}
