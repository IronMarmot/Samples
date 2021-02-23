using System;
using Xunit;
using Maxsatisfied;

namespace Maxsatisfied.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 class1 = new Class1();
            int max=class1.MaxSatisfied(new int[]{1,0,1,2,1,1,7,5},new int[]{0,1,0,1,0,1,0,1},3);
            Assert.Equal(16,max);

            int max2=class1.MaxSatisfied(new int[]{1,0,1,2,1,1,7,5},new int[]{0,1,0,1,0,1,0,1},3);
            Assert.Equal(16,max2);
        }
    }
}
