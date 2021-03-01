using System;
using Xunit;
using Palindrome;

namespace Palindrome.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 class1=new Class1();
            bool result=class1.IsPalindrome("A man, a plan, a canal: Panama");
            Assert.True(result);

            bool result1=class1.IsPalindrome("race a car");
            Assert.False(result1);
        }

        [Fact]
        public void Test2()
        {
            Class2 class2=new Class2();
            ListNode one=new ListNode(1);
            ListNode two=new ListNode(2);
            ListNode three=new ListNode(3);
            ListNode four=new ListNode(2);
            ListNode five=new ListNode(1);
            one.next=two;
            two.next=three;
            three.next=four;
            four.next=five;
            five.next=null;

            bool result=class2.IsPolindrome(one);
            Assert.True(result);

            one.next=two;
            two.next=four;
            four.next=five;
            five.next=null;
            bool result1=class2.IsPolindrome(one);
            Assert.True(result1);
        }

        [Fact]
        public void Test3()
        {
            Class2 class2=new Class2();
            ListNode one=new ListNode(1);
            ListNode two=new ListNode(2);
            ListNode three=new ListNode(3);
            ListNode four=new ListNode(4);
            ListNode five=new ListNode(5);
            one.next=two;
            two.next=three;
            three.next=four;
            four.next=five;
            five.next=null;

            bool result=class2.IsPolindrome(one);
            Assert.False(result);
        }
    }
}
