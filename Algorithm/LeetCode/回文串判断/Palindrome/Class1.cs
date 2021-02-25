using System;
/*
验证回文串
前提：
1.空串认为是回文串
2.只对字母和数字进行判断

将字符串转换成char[]，用2个指针从前后依次进行遍历，当当前元素不是字母和数字时移动到下一个，否则进行判断

这里有2个需要注意的：
1.在while(i)循环中，需要对i进行限制，以避免字符串都是非字母和数字的情况
2.对字母和数字的判断方法
*/
namespace Palindrome
{
    public class Class1
    {
        public bool IsPalindrome(string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                return true;
            }

            char[] chs=str.ToLower().ToCharArray();
            int i=0,j=chs.Length-1;
            while(i<j)
            {
                while(!IsAlphaOrNumber(chs[i]))
                {
                    i++;
                    if(i==chs.Length) return true;
                }
                while(!IsAlphaOrNumber(chs[j]))
                    j--;
                if(chs[i]!=chs[j])
                    return false;
                i++;
                j--;
            }
            return true;
        }

        public bool IsAlphaOrNumber(char c)
        {
            return (c>='a'&&c<='z')||(c>='0'&&c<='9');
        }
    }
}
