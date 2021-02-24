using System;

/*
链表的中间节点（若有2个中间节点，则返回第二个）

借助快慢指针，慢指针每次移动一步，快指针每次移动2步，并且，借助哨兵节点，
当quick.next==null||quick.next.next==null时，则slow指针即指向所求的位置

*/
namespace middleNode
{
    public class Class1
    {
        public ListNode MiddleNode(ListNode head) 
        {
            if(head==null||head.next==null)
                return head;
            
            ListNode slow=head;
            ListNode quick=head.next;
            while(quick.next!=null&&quick.next.next!=null)
            {
                slow=slow.next;
                quick=quick.next.next;
            }

            return slow.next;
        }
    }

    public class ListNode {
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
  }
}
