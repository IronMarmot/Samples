using System;

/*
2021-02-24  JackLee

链表的中间节点（若有2个中间节点，则返回第二个）

借助快慢指针，慢指针每次移动一步，快指针每次移动2步，并且，借助哨兵节点，
当quick.next==null||quick.next.next==null时，则slow.next指针即指向所求的位置

==============
另外，
其实对于奇数个元素的链表来说，其中间元素是确定的。

对于偶数个元素的链表：
我发现，从第一个元素开始，借助快慢指针，

若返回前一个作为中间元素，则以quick.next!=null&&quick.next.next!=null作为条件，最终slow即指向所求。
若返回后一个作为中间元素，则以quick!=null&&quick.next!=null作为条件即可。
其实，后一个判断条件会比前一个多判断一次，若采用上一个判断方法，则slow.next极为所求。

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
