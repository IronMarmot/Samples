using System;

/*
2021-02-24 JackLee
https://leetcode-cn.com/problems/reverse-linked-list

解法:
    1.借助3个指针来遍历链表即可
    2.若要使用递归，则将循环部分提取到方法即可。
*/
namespace ReverseList
{
    public class Class1
    {
        public ListNode ReverseList(ListNode head)
        {
            ListNode pre=null;
            ListNode current=head;
            while(current!=null)
            {
                ListNode next=current.next;
                current.next=pre;
                pre=current;
                current=next;
            }

            return pre;
        }
    }

    public class ListNode {
      public int val;
      public ListNode next;
      public ListNode(int val=0, ListNode next=null) {
          this.val = val;
          this.next = next;
      }
  }
}
