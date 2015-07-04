using System;
using System.Collections.Generic;

namespace Nano.Basics.Tree
{
    public class TreeNode<T>
    {
        T m_Data;
        List<TreeNode<T>> m_Children = null;

        public TreeNode()
        {
        }

        public TreeNode(T data)
        {
            m_Data = data;
        }

        public TreeNode(T data, List<TreeNode<T>> children)
        {
            m_Data = data;
            m_Children = children;
        }

        public T Value
        {
            get
            {
                return m_Data;
            }
            set
            {
                m_Data = value;
            }
        }

        protected List<TreeNode<T>> Children
        {
            get
            {
                return m_Children;
            }
            set
            {
                m_Children = value;
            }
        }
    }
}

