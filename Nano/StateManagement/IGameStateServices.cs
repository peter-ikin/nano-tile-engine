using System;

namespace Nano.StateManagement
{
    public interface IGameStateService
    {
        IGameState CurrentState
        {
            get;
        }
            
        void PopState();

        void PushState(IGameState newState);

        void ChangeState(IGameState newState);
    }
}

