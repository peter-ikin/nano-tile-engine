using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Nano.StateManagement
{
    public abstract class GameState : IGameState
    {
        #region member data 

        bool m_Visible;
        bool m_Enabled;

        List<GameComponent> m_ChildComponents;

        private IGameStateService m_StateManager;
        #endregion

        #region properties

        public List<GameComponent> Components
        {
            get { return m_ChildComponents; }
        }

        public IGameStateService StateManager
        {
            get { return m_StateManager; }
        }

        public bool Visible 
        { 
            get { return m_Visible; }
            set
            {
                m_Visible = value;
                foreach (GameComponent component in m_ChildComponents)
                {
                    if (component is DrawableGameComponent)
                        ((DrawableGameComponent)component).Visible = value;
                }
            }
        }

        public bool Enabled 
        { 
            get { return m_Enabled; }
            set
            {
                m_Enabled = value;
                foreach (GameComponent component in m_ChildComponents)
                {
                    component.Enabled = value;
                }
            }
        }
            
        #endregion

        protected GameState (IGameStateService manager)
        {
            m_StateManager = manager;
            m_ChildComponents = new List<GameComponent>();
        }

        #region DrawableGameComponent overrides


        public virtual void Update(GameTime gameTime)
        {
            foreach(GameComponent component in m_ChildComponents)
            {
                if(component.Enabled)
                    component.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;

            foreach(GameComponent component in m_ChildComponents)
            {
                if(component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;
                    if(drawComponent.Visible)
                        drawComponent.Draw(gameTime);
                }
            }
        }
        #endregion

        #region member functions

        public virtual void Activate()
        {

        }

        public virtual void Deactivate()
        {

        }

        #endregion
    }
}

