using System;
using NUnit.Framework;
using Nano.Engine.Sys;
using System.Collections.Generic;
using System.Linq;

namespace NanoTests.Engine.Sys
{
    /// <summary>
    /// Basic sanity Test of CMWC4096.
    /// These tests don't provide any statistical validation of randomness for the generated numbers.
    /// The tests here are just to make sure the RNG is working 
    /// </summary>
    [TestFixture]
    public class TestCMWC4096
    {
        [Test]
        public void TestInts()
        {
            IRandom rnd = new CMWC4096(1);

            List<int> intList = new List<int>();
            for(int i = 0; i < 10000; i++)
            {
                int rndInt = rnd.NextInt();
                intList.Add(rndInt);
            }
                
            Assert.That(intList.Distinct().Count() == intList.Count(), Is.True);
        }

       
        [TestCase(0,1,TestName="Test generate ints in range 0,1")]
        [TestCase(0,10,TestName="Test generate ints in range 0,10")]
        [TestCase(0,100,TestName="Test generate ints in rang 0,100")]
        [TestCase(0,1000,TestName="Test generate ints in range 0,1000")]
        [TestCase(3,12,TestName="Test generate ints in range 3,12")]
        [TestCase(-100,100,TestName="Test generate ints in range -100,100")]
        [TestCase(563,564,TestName="Test generate ints in range 563,564")]
        public void TestIntInRange(int min, int max)
        {
            IRandom rnd = new CMWC4096(1);

            bool passed = true;
            for(int i = 0; i < 10000; i++)
            {
                int rndInt = rnd.NextInt(min,max);
                if(rndInt<min || rndInt > max)
                {
                    passed = false;
                    break;
                }
            }

            Assert.That(passed, Is.True);
        }


        [Test]
        public void TestDoubles()
        {
            bool passed = true;

            IRandom rnd = new CMWC4096(1);

            List<double> dblList = new List<double>();
            for(int i = 0; i < 10000; i++)
            {
                double rndDbl = rnd.NextDouble();
                dblList.Add(rndDbl);
                if(rndDbl < 0 || rndDbl > 1)
                {
                    passed = false;
                    break;
                }
            }
                
            Assert.That(passed, Is.True);
            Assert.That(dblList.Distinct().Count() == dblList.Count(), Is.True);
        }

        [TestCase(0,1,TestName="Test generate doubles in range 0,1")]
        [TestCase(0,10,TestName="Test generate doubles in range 0,10")]
        [TestCase(0,100,TestName="Test generate doubles in rang 0,100")]
        [TestCase(0,1000,TestName="Test generate doubles in range 0,1000")]
        [TestCase(3,12,TestName="Test generate doubles in range 3,12")]
        [TestCase(-100,100,TestName="Test generate doubles in range -100,100")]
        [TestCase(563,564,TestName="Test generate doubles in range 563,564")]
        [TestCase(1.5,2.5,TestName="Test generate doubles in range 1.5,2.5")]
        public void TestDoublesInRange(double min, double max)
        {
            IRandom rnd = new CMWC4096(1);

            bool passed = true;
            for(int i = 0; i < 10000; i++)
            {
                double rndDbl = rnd.NextDouble(min,max);
                if(rndDbl<min || rndDbl > max)
                {
                    passed = false;
                    break;
                }
            }

            Assert.That(passed, Is.True);
        }
    }
}

