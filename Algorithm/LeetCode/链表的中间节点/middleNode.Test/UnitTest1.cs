using System;
using Xunit;

namespace middleNode.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1 class1=new Class1();
            ListNode head=new ListNode(1);
            ListNode two=new ListNode(2);
            ListNode three=new ListNode(3);
            ListNode four=new ListNode(4);
            ListNode five=new ListNode(5);
            head.next=two;
            two.next=three;
            three.next=four;
            four.next=five;
            five.next=null;
            ListNode node=class1.MiddleNode(head);
            Assert.Equal(3,node.val);
        }
    }
}
