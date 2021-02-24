using System;

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
