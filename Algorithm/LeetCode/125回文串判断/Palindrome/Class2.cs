using System;
/*
链表处理回文串判断
首先找到链表的中间节点，对后半部分链表进行反转，然后依次对链表元素进行比较。

链表解法
*/
namespace Palindrome
{
    public class Class2
    {
        public bool IsPolindrome(ListNode head)
        {
            if(head==null||head.next==null)
                return true;

            //寻找链表的中间节点
            ListNode slow=head;
            ListNode quick=head;
            while(quick!=null&&quick.next!=null)
            {
                slow=slow.next;
                quick=quick.next.next;
            }

            //反转从slow到链表尾的元素
            ListNode prev=null;
            ListNode current=slow;
            ListNode next=current.next;
            while(current.next!=null)
            {
                current.next=prev;
                prev=current;
                current=next;
                next=next.next;
            }
            current.next=prev;

            //此时，反转的链表的head为current
            while(head!=null&&current!=null)
            {
                if(head.val!=current.val)
                    return false;
                head=head.next;
                current=current.next;
            }

            return true;
        }
    }

    public class ListNode{
        public int val;
        public ListNode next;
        public ListNode(int value)
        {
            this.val=value;
        }
    }
}