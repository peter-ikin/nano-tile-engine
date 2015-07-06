using System;
using NUnit.Framework;
using Nano.Basics.Tree;
using System.Collections.Generic;

namespace NanoTests.Basics.Tree
{
    [TestFixture]
    public class TestTreeNode
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
        public void TestDefaultConstruct()
        {
            var node = new TreeNode<int>();

            Assert.That(node, Is.Not.Null);
        }

        [Test]
        public void TestDefaultConstructWithClass()
        {
            var node = new TreeNode<TestClass>();

            Assert.That(node, Is.Not.Null);
        }

        [Test]
        public void TestConstructWithIntrinsicData()
        {
            var node = new TreeNode<int>(3);

            Assert.That(node, Is.Not.Null);
        }

        [Test]
        public void TestConstructWithClassData()
        {
            var data = new TestClass(23);
            var node = new TreeNode<TestClass>(data);

            Assert.That(node, Is.Not.Null);
        }

        [Test]
        public void TestFullConstructWithIntrinsicData2Children()
        {
            var left = new TreeNode<int>(12);
            var right = new TreeNode<int>(21);
            var lst = new List<TreeNode<int>>{ left, right };
            var node = new TreeNode<int>(3,lst);
           
            Assert.That(node, Is.Not.Null);
        }

        [Test]
        public void TestFullConstructWithIntrinsicData4Children()
        {
            var left = new TreeNode<int>(12);
            var right = new TreeNode<int>(21);
            var middleLeft = new TreeNode<int>(1234);
            var middleRight = new TreeNode<int>(-12);
            var lst = new List<TreeNode<int>>{ left, middleLeft, middleRight, right };
            var node = new TreeNode<int>(3,lst);

            Assert.That(node, Is.Not.Null);
        }

        [Test]
        public void TestFullConstructWithClassData()
        {
            var data = new TestClass(23);

            var left = new TreeNode<TestClass>(new TestClass(12));
            var right = new TreeNode<TestClass>(new TestClass(21));
            var middleLeft = new TreeNode<TestClass>(new TestClass(1234));
            var middleRight = new TreeNode<TestClass>(new TestClass(-12));
            var lst = new List<TreeNode<TestClass>>{ left, middleLeft, middleRight, right };

            var node = new TreeNode<TestClass>(data,lst);

            Assert.That(node, Is.Not.Null);
        }

        [Test]
        public void TestGetValueWithIntrinsicData()
        {
            var node = new TreeNode<int>(3);

            Assert.That(node.Value, Is.EqualTo(3));
        }

        [Test]
        public void TestGetValueWithClassData()
        {
            var data = new TestClass(23);
            var node = new TreeNode<TestClass>(data);

            Assert.That(node.Value, Is.EqualTo(data));
            Assert.That(node.Value.IntVal, Is.EqualTo(23));
        }
    }
}

