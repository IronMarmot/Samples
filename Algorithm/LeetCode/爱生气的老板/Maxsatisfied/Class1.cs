using System;


/*
其实是要求，连续的子数组，使其SUM(customers[i]*grumy[i])最大
1.第一次编写只是实现了要求，但效率非常低，因为时间复杂度为O(n²)
2.其实对于满意的客户是不变的，唯一变化的是可增加的生气客户。因此，我们可以在一次遍历过程中，既求出满意客户，又求出可增加的生气客户，相加即为最终结果。
在求可增加客户的过程中，首先我们要保证取连续的X个元素的和Sum，因此以i>=x为条件，当i=x时，sum-customers[0]*grumpy[0]即为初始的max值。另外需要注意的是，Math.Max函数
是在for循环中执行，而不是在if中。否则初始值max值会错误。
*/
namespace Maxsatisfied
{
    public class Class1
    {

        //1.只实现了需求，效率低，时间复杂度为O(n²)
        public int MaxSatisfied(int[] customers, int[] grumpy, int X) 
        {
            int max=0,position=0;
            for(int i=0;i<customers.Length-X+1;i++)
            {
                int sum=0;
                for(int j=0;j<X;j++)
                {
                    sum+=customers[i+j]*grumpy[i+j];
                }
                if(sum>max)
                {
                    max=sum;
                    position=i;
                }
            }

            max=0;
            for(int i=0;i<grumpy.Length;i++)
            {
                if(grumpy[i]==0||(i>=position&&i<position+X))
                {
                    max+=customers[i];
                }
            }

            return max;
        }
    
        public int MaxSatisfied2(int[] customers,int[] grumpy,int X)
        {
            int length=customers.Length;
            int sum1=0,sum2=0,max=0;
            for(int i=0;i<length;i++)
            {
                if(grumpy[i]==0)
                    sum1+=customers[i];
                else
                {
                    sum2+=customers[i];
                    if(i>=X)
                    {
                        sum2-=customers[i-X]*grumpy[i-X];
                    }
                    max=Math.Max(sum2,max);
                }
            }

            return sum1+max;
        }    
    }
}
