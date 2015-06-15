using System;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Nano.Engine.Sys;

namespace Nano.StateManagement
{
    public class GameStateManager : DrawableGameComponent, IGameStateService
    {
        #region member data
     
        ILoggingService m_Logger;

        Stack<IGameState> m_GameStates = new Stack<IGameState>();

        #endregion

        #region properties

        public IGameState CurrentState
        {
            get 
            {
                if(m_GameStates.Count > 0)
                    return m_GameStates.Peek ();
                return null;
            }
        }

        #endregion

        public GameStateManager (Game game, ILoggingService logger)
            :base(game)
        {
            m_Logger = logger;
            m_Logger.Log("Constructing GameStateManager ...");
        }

        #region DrawableGameComponent overrides

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            List<IGameState> states = m_GameStates.ToList();
            states.Reverse();

            foreach( IGameState state in states)
            {
                if(state.Enabled)
                    state.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Update(gameTime);

            List<IGameState> states = m_GameStates.ToList();
            states.Reverse();

            foreach( IGameState state in states)
            {
                if(state.Visible)
                   state.Draw(gameTime);
            }
        }

        #endregion

        #region member functions
   
        public void PopState()
        {
            if (m_GameStates.Count > 0)
            {
                CurrentState.Enabled = false;
                CurrentState.Visible = false;
                CurrentState.Deactivate();

                m_GameStates.Pop();

                if(CurrentState != null)
                {
                    CurrentState.Enabled = true;
                    CurrentState.Visible = true;
                }
            }
        }

        public void PushState(IGameState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Enabled = false;
                CurrentState.Visible = false;
            }

            m_GameStates.Push(newState);

            newState.Enabled = true;
            newState.Visible = true;

            newState.Activate();
        }

        public void ChangeState(IGameState newState)
        {
            //unwind current stack of gamestates
            while (m_GameStates.Count > 0)
            {
                CurrentState.Enabled = false;
                CurrentState.Visible = false;
                CurrentState.Deactivate();
                m_GameStates.Pop();
            }

            //push the new state onto the stack and activate as normal
            m_GameStates.Push(newState);
            newState.Enabled = true;
            newState.Visible = true;
            newState.Activate();
        }
        #endregion
    }
}

