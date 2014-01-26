using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrumpyRabbit
{
    /// <summary>
    /// The RabbitStateManager is the context of the state pattern for the rabbit.
    /// It manages the states change.
    /// </summary>
    public class RabbitStateManager
    {
        /// <summary>
        /// ID of the states provided by this manager.
        /// </summary>
        public enum MoveStateID
        {
            DRAG, FALL, GRAB, IDLE, RUN
        }

        private RabbitMoveDrag rabbitMoveDrag;
        private RabbitMoveGrab rabbitMoveGrab;
        private RabbitMoveFall rabbitMoveFall;
        private RabbitMoveIdle rabbitMoveIdle;
        private RabbitMoveRun rabbitMoveRun;

        private RabbitState currentRabbitMoveState;

        /// <summary>
        /// Main constructor. Needs the game's core.
        /// </summary>
        /// <param name="gameCore">Game's core</param>
        public RabbitStateManager(GameCore gameCore)
        {
            InitializeStates(gameCore);
        }

        /// <summary>
        /// Initialize all states provided by this manager. It also creates
        /// parameters used by these.
        /// </summary>
        /// <param name="gameCore">Game's core</param>
        private void InitializeStates(GameCore gameCore)
        {
            gameCore.GameParameters.Add("rabbitXSpeed", 0.0);
            gameCore.GameParameters.Add("rabbitYSpeed", 0.0);

            rabbitMoveDrag = new RabbitMoveDrag(gameCore, this);
            rabbitMoveGrab = new RabbitMoveGrab(gameCore, this);
            rabbitMoveFall = new RabbitMoveFall(gameCore, this);
            rabbitMoveIdle = new RabbitMoveIdle(gameCore, this);
            rabbitMoveRun = new RabbitMoveRun(gameCore, this);

            currentRabbitMoveState = rabbitMoveIdle;
            currentRabbitMoveState.EnterState();
        }

        /// <summary>
        /// Manage states change. Calls LeaveState before the change and
        /// EnterState after.
        /// </summary>
        /// <param name="moveStateID">The state ID</param>
        public void ChangeMoveState(MoveStateID moveStateID)
        {
            currentRabbitMoveState.LeaveState();

            switch (moveStateID)
            {
                case MoveStateID.DRAG:
                    currentRabbitMoveState = rabbitMoveDrag;
                    break;
                case MoveStateID.FALL:
                    currentRabbitMoveState = rabbitMoveFall;
                    break;
                case MoveStateID.GRAB:
                    currentRabbitMoveState = rabbitMoveGrab;
                    break;
                case MoveStateID.IDLE:
                    currentRabbitMoveState = rabbitMoveIdle;
                    break;
                case MoveStateID.RUN:
                    currentRabbitMoveState = rabbitMoveRun;
                    break;
                default:
                    Console.Error.WriteLine("Unexpected move state ID.");
                    break;
            }
            currentRabbitMoveState.EnterState();
        }

        /// <summary>
        /// Updates the current state.
        /// </summary>
        /// <param name="elapsedMilliseconds">Elapsed milliseconds since last frame.</param>
        public void UpdateRabbit(int elapsedMilliseconds)
        {
            currentRabbitMoveState.UpdateState(elapsedMilliseconds);
        }
    }
}
