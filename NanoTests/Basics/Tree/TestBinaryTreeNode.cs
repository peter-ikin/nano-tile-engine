using System;
using NUnit.Framework;
using Nano.Basics.Tree;

namespace NanoTests.Basics.Tree
{
    [TestFixture]
    public class TestBinaryTreeNode
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
        public void TestDefaultConstructBinaryTreeNodeIntrinsic()
        {
            var node = new BinaryTreeNode<int>();

            Assert.That(node,Is.Not.Null);
        }

        [Test]
        public void TestDefaultConstructBinaryTreeNodeWithObject()
        {
            var node = new BinaryTreeNode<TestClass>();

            Assert.That(node,Is.Not.Null);
        }

        [Test]
        public void TestConstructBinaryTreeNodeWithIntrinsicData()
        {
            int data = 3;
            var node = new BinaryTreeNode<int>(data);

            Assert.That(node,Is.Not.Null);
        }

        [Test]
        public void TestConstructBinaryTreeNodeWithObjectData()
        {
            var data = new TestClass(321);
            var node = new BinaryTreeNode<TestClass>(data);

            Assert.That(node,Is.Not.Null);
        }

        [Test]
        public void TestFullConstructBinaryTreeNodeWithIntrinsic()
        {
            int data = 3;
            var left = new BinaryTreeNode<int>(1);
            var right = new BinaryTreeNode<int>(2);
            
            var node = new BinaryTreeNode<int>(data, left, right);

            Assert.That(node,Is.Not.Null);
        }

        [Test]
        public void TestFullConstructBinaryTreeNodeWithObject()
        {
            var data = new TestClass(3);
            var left = new BinaryTreeNode<TestClass>(new TestClass(12));
            var right = new BinaryTreeNode<TestClass>(new TestClass(32));

            var node = new BinaryTreeNode<TestClass>(data, left, right);

            Assert.That(node,Is.Not.Null);
        }

        [Test]
        public void TestBinaryTreeNodeDefaultValue()
        {
            var node = new BinaryTreeNode<int>();

            Assert.That(node.Value,Is.EqualTo(0));
        }

        [Test]
        public void TestBinaryTreeNodeDefaultValueWithClass()
        {
            var node = new BinaryTreeNode<TestClass>();

            Assert.That(node.Value,Is.EqualTo(null));
        }

        [Test]
        public void TesBinaryTreeNodeWithValueData()
        {
            int data = 3;
            var node = new BinaryTreeNode<int>(data);

            Assert.That(node.Value,Is.EqualTo(data));
        }

        [Test]
        public void TesBinaryTreeNodeWithObjectValueData()
        {
            var data = new TestClass(34);
            var node = new BinaryTreeNode<TestClass>(data);

            Assert.That(node.Value,Is.EqualTo(data));
            Assert.That(node.Value.IntVal, Is.EqualTo(34));
        }

        [Test]
        public void TestBinaryTreeNodeGetLeft()
        {
            int data = 3;
            var left = new BinaryTreeNode<int>(1);
            var right = new BinaryTreeNode<int>(2);

            var node = new BinaryTreeNode<int>(data, left, right);

            Assert.That(node.Left,Is.EqualTo(left));
            Assert.That(node.Left.Value, Is.EqualTo(1));
        }

        [Test]
        public void TestBinaryTreeNodeSetLeft()
        {
            int data = 3;
            var left = new BinaryTreeNode<int>(1);
            var right = new BinaryTreeNode<int>(2);

            var node = new BinaryTreeNode<int>(data, left, right);

            var newLeftNode = new BinaryTreeNode<int>(123);
            node.Left = newLeftNode;

            Assert.That(node.Left,Is.EqualTo(newLeftNode));
            Assert.That(node.Left.Value, Is.EqualTo(123));
        }

        [Test]
        public void TestBinaryTreeNodeGetRight()
        {
            int data = 3;
            var left = new BinaryTreeNode<int>(1);
            var right = new BinaryTreeNode<int>(2);

            var node = new BinaryTreeNode<int>(data, left, right);

            Assert.That(node.Right,Is.EqualTo(right));
            Assert.That(node.Right.Value, Is.EqualTo(2));
        }

        [Test]
        public void TestBinaryTreeNodeSetRight()
        {
            int data = 3;
            var left = new BinaryTreeNode<int>(1);
            var right = new BinaryTreeNode<int>(2);

            var node = new BinaryTreeNode<int>(data, left, right);

            var newRightNode = new BinaryTreeNode<int>(123);
            node.Right = newRightNode;

            Assert.That(node.Right,Is.EqualTo(newRightNode));
            Assert.That(node.Right.Value, Is.EqualTo(123));
        }

        [Test]
        public void TestBinaryTreeNodeGetLeftWithObject()
        {
            var data = new TestClass(3);
            var left = new BinaryTreeNode<TestClass>(new TestClass(1));
            var right = new BinaryTreeNode<TestClass>(new TestClass(2));

            var node = new BinaryTreeNode<TestClass>(data, left, right);

            Assert.That(node.Left,Is.EqualTo(left));
            Assert.That(node.Left.Value, Is.EqualTo(left.Value));
            Assert.That(node.Left.Value.IntVal, Is.EqualTo(1));
        }

        [Test]
        public void TestBinaryTreeNodeSetLeftWithObject()
        {
            var data = new TestClass(3);
            var left = new BinaryTreeNode<TestClass>(new TestClass(1));
            var right = new BinaryTreeNode<TestClass>(new TestClass(2));

            var node = new BinaryTreeNode<TestClass>(data, left, right);

            var newLeftNode = new BinaryTreeNode<TestClass>(new TestClass(123));
            node.Left = newLeftNode;

            Assert.That(node.Left,Is.EqualTo(newLeftNode));
            Assert.That(node.Left.Value.IntVal, Is.EqualTo(123));
        }

        [Test]
        public void TestBinaryTreeNodeGetRightWithObject()
        {
            var data = new TestClass(3);
            var left = new BinaryTreeNode<TestClass>(new TestClass(1));
            var right = new BinaryTreeNode<TestClass>(new TestClass(2));

            var node = new BinaryTreeNode<TestClass>(data, left, right);

            Assert.That(node.Right,Is.EqualTo(right));
            Assert.That(node.Right.Value, Is.EqualTo(right.Value));
            Assert.That(node.Right.Value.IntVal, Is.EqualTo(2));
        }

        [Test]
        public void TestBinaryTreeNodeSetRightWithObject()
        {
            var data = new TestClass(3);
            var left = new BinaryTreeNode<TestClass>(new TestClass(1));
            var right = new BinaryTreeNode<TestClass>(new TestClass(2));

            var node = new BinaryTreeNode<TestClass>(data, left, right);

            var newRightNode = new BinaryTreeNode<TestClass>(new TestClass(123));
            node.Right = newRightNode;

            Assert.That(node.Right,Is.EqualTo(newRightNode));
            Assert.That(node.Right.Value.IntVal, Is.EqualTo(123));
        }

    }
}

