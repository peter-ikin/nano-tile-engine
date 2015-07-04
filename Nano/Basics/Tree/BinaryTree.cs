using System;

namespace Nano.Basics.Tree
{
    public class BinaryTree<T>
    {
        BinaryTreeNode<T> m_Root;

        public BinaryTree()
        {
            m_Root = null;
        }

        public virtual void Clear()
        {
            m_Root = null;
        }

        public BinaryTreeNode<T> Root
        {
            get
            {
                return m_Root;
            }
            set
            {
                m_Root = value;
            }
        }
    }
}

