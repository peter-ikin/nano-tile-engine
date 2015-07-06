using System;
using NUnit.Framework;
using Nano.Basics.Tree;
using NUnit.Framework.Constraints;

namespace NanoTests.Basics.Tree
{
    [TestFixture]
    public class TestBinaryTree
    {
        private class TestClass
        {
            int m_Integer;

            public TestClass(int value)
            {
                m_Integer = value;   
            }

            public int IntVal 
            { 
                get 
                {
                    return m_Integer;
                }
            }
        }

        [Test]
        public void TestConstructBinaryTreeInt()
        {
            var tree = new BinaryTree<int>();

            Assert.That(tree, Is.Not.Null);
            Assert.That(tree.Root, Is.Null);
        }

        [Test]
        public void TestConstructBinaryTreeWithClass()
        {
            var tree = new BinaryTree<TestClass>();

            Assert.That(tree, Is.Not.Null);
            Assert.That(tree.Root, Is.Null);
        }

        [Test]
        public void TestGetAndSetRootInt()
        {
            var tree = new BinaryTree<int>();

            var root = new BinaryTreeNode<int>(12,
                new BinaryTreeNode<int>(10),
                new BinaryTreeNode<int>(2));
            
            tree.Root = root;
            Assert.That(tree, Is.Not.Null);
            Assert.That(tree.Root, Is.EqualTo(root));
        }

        [Test]
        public void TestGetAndSetRootClass()
        {
            var tree = new BinaryTree<TestClass>();

            var data = new TestClass(45);
            var left = new BinaryTreeNode<TestClass>(new TestClass(40));
            var right = new BinaryTreeNode<TestClass>(new TestClass(5));

            Assert.That(tree.Root, Is.Null);
            var root = new BinaryTreeNode<TestClass>(data, left, right);
            tree.Root = root;

            Assert.That(tree.Root, Is.EqualTo(root));

        }

        [Test]
        public void TestClear()
        {
            var tree = new BinaryTree<TestClass>();

            var data = new TestClass(45);
            var left = new BinaryTreeNode<TestClass>(new TestClass(40));
            var right = new BinaryTreeNode<TestClass>(new TestClass(5));

            Assert.That(tree.Root, Is.Null);
            var root = new BinaryTreeNode<TestClass>(data, left, right);
            tree.Root = root;

            Assert.That(tree.Root, Is.EqualTo(root));

            tree.Clear();

            Assert.That(tree.Root, Is.Null);
        }
    }
}

