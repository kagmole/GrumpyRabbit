using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrumpyRabbit
{
    /// <summary>
    /// A RabbitState defines how the rabbit "move". It is a state pattern that avoid
    /// the use of a lot of "if" in the game's core.
    /// 
    /// The "RabbitStateManager" is used to change the state.
    /// </summary>
    public abstract class RabbitState
    {
        protected GameCore gameCore;
        protected RabbitStateManager rabbitStateManager;

        /// <summary>
        /// Main contructors. Needs the game's core and his manager to change state.
        /// </summary>
        /// <param name="gameCore">Game's core</param>
        /// <param name="rabbitStateManager">State manager</param>
        public RabbitState(GameCore gameCore, RabbitStateManager rabbitStateManager)
        {
            this.gameCore = gameCore;
            this.rabbitStateManager = rabbitStateManager;
        }

        /// <summary>
        /// Called before the first update.
        /// </summary>
        public abstract void EnterState();

        /// <summary>
        /// Called after the last update.
        /// </summary>
        public abstract void LeaveState();

        public abstract void UpdateState(int elapsedMilliseconds);
    }
}
