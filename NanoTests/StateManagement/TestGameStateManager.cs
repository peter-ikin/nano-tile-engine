using System;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using Nano.StateManagement;
using Nano.Engine.Sys;
using Moq;

namespace NanoTests.StateManagement
{        
    [TestFixture]
    public class TestGameStateManager
    {    
        private class TestStateA : Nano.StateManagement.GameState
        {
            public TestStateA(IGameStateService manager)
                :base(manager)
            {

            }
            public override void Draw(GameTime gameTime)
            {
                DrawCallCount++;
            }

            public override void Update(GameTime gameTime)
            {
                UpdateCallCount++;
            }

            public override void Activate()
            {
                ActivateCallCount++;
            }

            public override void Deactivate()
            {
                DeactivateCallCount++;
            }

            public int ActivateCallCount { get; private set; }
            public int DeactivateCallCount { get; private set; }

            public int UpdateCallCount { get; private set; }
            public int DrawCallCount { get; private set; }
        }

        private class TestStateB : Nano.StateManagement.GameState
        {
            public TestStateB(IGameStateService manager)
                :base(manager)
            {

            }
            public override void Draw(GameTime gameTime)
            {
                DrawCallCount++;
            }

            public override void Update(GameTime gameTime)
            {
                UpdateCallCount++;
            }

            public override void Activate()
            {
                ActivateCallCount++;
            }

            public override void Deactivate()
            {
                DeactivateCallCount++;
            }

            public int ActivateCallCount { get; private set; }
            public int DeactivateCallCount { get; private set; }

            public int UpdateCallCount { get; private set; }
            public int DrawCallCount { get; private set; }
        }
            
        private class TestStateC : Nano.StateManagement.GameState
        {
            public TestStateC(IGameStateService manager)
                :base(manager)
            {

            }
            public override void Draw(GameTime gameTime)
            {
                DrawCallCount++;
            }

            public override void Update(GameTime gameTime)
            {
                UpdateCallCount++;
            }

            public override void Activate()
            {
                ActivateCallCount++;
            }

            public override void Deactivate()
            {
                DeactivateCallCount++;
            }

            public int ActivateCallCount { get; private set; }
            public int DeactivateCallCount { get; private set; }

            public int UpdateCallCount { get; private set; }
            public int DrawCallCount { get; private set; }
        }

        ILoggingService m_Logger;

        [SetUp]
        public void Init()
        {
            var mockLogger = new Mock<ILoggingService>();
            m_Logger = mockLogger.Object;
        }

        [Test]
        public void TestConstruct()
        {
            IGameStateService manager = new GameStateManager(null, m_Logger);
            Assert.That(manager,Is.Not.Null); 
        }

        [Test]
        public void TestDefaultGameState()
        {
            IGameStateService manager = new GameStateManager(null, m_Logger);
            Assert.That(manager.CurrentState, Is.Null);
        }

        [Test]
        public void TestPushSingleGameState()
        {
            IGameStateService manager = new GameStateManager(null, m_Logger);
            IGameState state = new TestStateA(manager);
            manager.PushState(state);
            Assert.That(manager.CurrentState, Is.EqualTo(state));
        }

        [Test]
        public void TestPopSingleGameState()
        {
            IGameStateService manager = new GameStateManager(null, m_Logger);
            IGameState state = new TestStateA(manager);
            manager.PushState(state);
            Assert.That(manager.CurrentState, Is.EqualTo(state));
            manager.PopState();
            Assert.That(manager.CurrentState, Is.Null);
        }

        [Test]
        public void TestPushTwoGameStates()
        {
            IGameStateService manager = new GameStateManager(null, m_Logger);
            IGameState stateA = new TestStateA(manager);
            manager.PushState(stateA);
            IGameState stateB = new TestStateB(manager);
            manager.PushState(stateB);
            Assert.That(manager.CurrentState, Is.EqualTo(stateB));
        }

        [Test]
        public void TestPushTwoPopOneGameState()
        {
            IGameStateService manager = new GameStateManager(null, m_Logger);
            IGameState stateA = new TestStateA(manager);
            manager.PushState(stateA);
            IGameState stateB = new TestStateB(manager);
            manager.PushState(stateB);
            manager.PopState();
            Assert.That(manager.CurrentState, Is.EqualTo(stateA));
        }

        [Test]
        public void TestUpdateOnSingleState()
        {
            GameStateManager manager = new GameStateManager(null, m_Logger);
            TestStateA stateA = new TestStateA(manager);
            manager.PushState(stateA);

            manager.Update(new GameTime());

            Assert.That(stateA.UpdateCallCount, Is.EqualTo(1));
        }

        [Test]
        public void TestUpdateOnTwoStates()
        {
            GameStateManager manager = new GameStateManager(null, m_Logger);
            TestStateA stateA = new TestStateA(manager);
            TestStateB stateB = new TestStateB(manager);

            manager.PushState(stateA);
            manager.PushState(stateB);

            manager.Update(new GameTime());

            Assert.That(stateA.UpdateCallCount, Is.EqualTo(0));
            Assert.That(stateB.UpdateCallCount, Is.EqualTo(1));
        }

        [Test]
        public void TestUpdateAccrossStateTransitions()
        {
            GameStateManager manager = new GameStateManager(null, m_Logger);
            TestStateA stateA = new TestStateA(manager);
            TestStateB stateB = new TestStateB(manager);

            manager.PushState(stateA);
            manager.PushState(stateB);

            manager.Update(new GameTime());

            Assert.That(stateA.UpdateCallCount, Is.EqualTo(0));
            Assert.That(stateB.UpdateCallCount, Is.EqualTo(1));

            manager.PopState();
            manager.Update(new GameTime());

            Assert.That(stateA.UpdateCallCount, Is.EqualTo(1));
            Assert.That(stateB.UpdateCallCount, Is.EqualTo(1));
        }

        [Test]
        public void TestDrawOnSingleState()
        {
            GameStateManager manager = new GameStateManager(null, m_Logger);
            TestStateA stateA = new TestStateA(manager);
            manager.PushState(stateA);

            manager.Draw(new GameTime());

            Assert.That(stateA.DrawCallCount, Is.EqualTo(1));
        }

        [Test]
        public void TestDrawOnTwoStates()
        {
            GameStateManager manager = new GameStateManager(null, m_Logger);
            TestStateA stateA = new TestStateA(manager);
            TestStateB stateB = new TestStateB(manager);

            manager.PushState(stateA);
            manager.PushState(stateB);

            manager.Draw(new GameTime());

            Assert.That(stateA.DrawCallCount, Is.EqualTo(0));
            Assert.That(stateB.DrawCallCount, Is.EqualTo(1));
        }

        [Test]
        public void TestDrawAccrossStateTransitions()
        {
            GameStateManager manager = new GameStateManager(null, m_Logger);
            TestStateA stateA = new TestStateA(manager);
            TestStateB stateB = new TestStateB(manager);

            manager.PushState(stateA);
            manager.PushState(stateB);

            manager.Draw(new GameTime());

            Assert.That(stateA.DrawCallCount, Is.EqualTo(0));
            Assert.That(stateB.DrawCallCount, Is.EqualTo(1));

            manager.PopState();
            manager.Draw(new GameTime());

            Assert.That(stateA.DrawCallCount, Is.EqualTo(1));
            Assert.That(stateB.DrawCallCount, Is.EqualTo(1));
        }

        [Test]
        public void TestGameStateActivateDeactivate()
        {
            IGameStateService manager = new GameStateManager(null, m_Logger);
            var state = new TestStateA(manager);

            manager.PushState(state);

            Assert.That(state.ActivateCallCount, Is.EqualTo(1));
            Assert.That(state.DeactivateCallCount, Is.EqualTo(0));

            manager.PopState();

            Assert.That(state.ActivateCallCount, Is.EqualTo(1));
            Assert.That(state.DeactivateCallCount, Is.EqualTo(1));
        }

        [Test]
        public void TestGameStateActivateDeactivateAccrossStateTransition()
        {
            IGameStateService manager = new GameStateManager(null, m_Logger);
            var stateA = new TestStateA(manager);
            var stateB = new TestStateB(manager);

            manager.PushState(stateA);

            Assert.That(stateA.ActivateCallCount, Is.EqualTo(1));
            Assert.That(stateA.DeactivateCallCount, Is.EqualTo(0));
            Assert.That(stateB.ActivateCallCount, Is.EqualTo(0));
            Assert.That(stateB.DeactivateCallCount, Is.EqualTo(0));

            manager.PushState(stateB);

            Assert.That(stateA.ActivateCallCount, Is.EqualTo(1));
            Assert.That(stateA.DeactivateCallCount, Is.EqualTo(0));
            Assert.That(stateB.ActivateCallCount, Is.EqualTo(1));
            Assert.That(stateB.DeactivateCallCount, Is.EqualTo(0));

            manager.PopState();

            Assert.That(stateA.ActivateCallCount, Is.EqualTo(1));
            Assert.That(stateA.DeactivateCallCount, Is.EqualTo(0));
            Assert.That(stateB.ActivateCallCount, Is.EqualTo(1));
            Assert.That(stateB.DeactivateCallCount, Is.EqualTo(1));

            manager.PopState();

            Assert.That(stateA.ActivateCallCount, Is.EqualTo(1));
            Assert.That(stateA.DeactivateCallCount, Is.EqualTo(1));
            Assert.That(stateB.ActivateCallCount, Is.EqualTo(1));
            Assert.That(stateB.DeactivateCallCount, Is.EqualTo(1));
        }


        [Test]
        public void TestGameStateChangeState()
        {
            IGameStateService manager = new GameStateManager(null, m_Logger);
            var stateA = new TestStateA(manager);
            var stateB = new TestStateB(manager);
            var stateC = new TestStateC(manager);

            manager.PushState(stateA);
            manager.PushState(stateB);

            manager.ChangeState(stateC);

            //check activate / deactivate counts of states
            Assert.That(stateA.ActivateCallCount, Is.EqualTo(1));
            Assert.That(stateA.DeactivateCallCount, Is.EqualTo(1));
            Assert.That(stateB.ActivateCallCount, Is.EqualTo(1));
            Assert.That(stateB.DeactivateCallCount, Is.EqualTo(1));
            Assert.That(stateC.ActivateCallCount, Is.EqualTo(1));
            Assert.That(stateC.DeactivateCallCount, Is.EqualTo(0));

            //check current state
            Assert.That(manager.CurrentState, Is.EqualTo(stateC));
        }
    }



}

