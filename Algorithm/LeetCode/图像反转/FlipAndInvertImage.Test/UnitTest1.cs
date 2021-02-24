using System;
using Xunit;
using FlipAndInvertImage;

namespace FlipAndInvertImage.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 class1=new Class1();
            int[][] array=class1.FlipAndInvertImage(new int[3][]{new int[3]{1,1,0},new int[3]{1,0,1},new int[3]{0,0,0}});
            Assert.Equal(new int[3][]{new int[3]{1,0,0},new int[3]{0,1,0},new int[3]{1,1,1}},array);
            int[][] array2=class1.FlipAndInvertImage(new int[4][]{new int[4]{1,1,0,0},new int[4]{1,0,0,1},new int[4]{0,1,1,1},new int[4]{1,0,1,0}});
            Assert.Equal(new int[4][]{new int[4]{1,1,0,0},new int[4]{0,1,1,0},new int[4]{0,0,0,1},new int[4]{1,0,1,0}},array2);
        }
    }
}
