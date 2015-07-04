using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Schema;

namespace Nano.Basics.Tree
{
    public class BinaryTreeNode<T> : TreeNode<T>
    {

        public BinaryTreeNode() : base()
        {
        }

        public BinaryTreeNode(T data)
        {
            Value = data;
        }

        public BinaryTreeNode(T data, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
        {
            Value = data;
            Children = new List<TreeNode<T>>
            { 
                    left, 
                    right
            };
        }

        public BinaryTreeNode<T> Left
        {
            get
            {
                if (base.Children == null)
                {
                    return null;
                }
                return base.Children[0] as BinaryTreeNode<T>;
            }
            set
            {
                if (base.Children == null)
                {
                    base.Children = new List<TreeNode<T>>(2);
                }

                base.Children[0] = value;
            }
        }

        public BinaryTreeNode<T> Right
        {
            get
            {
                if (base.Children == null)
                {
                    return null;
                }
                return base.Children[1] as BinaryTreeNode<T>;
            }
            set
            {
                if (base.Children == null)
                {
                    base.Children = new List<TreeNode<T>>(2);
                }

                base.Children[1] = value;
            }
        }
    }
}

