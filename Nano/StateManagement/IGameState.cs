using System;
using Microsoft.Xna.Framework;

namespace Nano.StateManagement
{
    public interface IGameState
    {
        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);

        bool Visible { get; set; }

        bool Enabled { get; set; }

        void Activate();

        void Deactivate();
    }
}

