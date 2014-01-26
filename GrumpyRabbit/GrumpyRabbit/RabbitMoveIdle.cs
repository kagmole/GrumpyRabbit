using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GrumpyRabbit
{
    public class RabbitMoveIdle : RabbitState
    {
        public RabbitMoveIdle(GameCore gameCore, RabbitStateManager rabbitStateManager)
            : base(gameCore, rabbitStateManager)
        {
        }

        public override void EnterState()
        {
            // TODO
        }

        public override void LeaveState()
        {
            // TODO
        }

        public override void UpdateState(int elapsedMilliseconds)
        {
            bool rabbitAboveGround = false;
            
            foreach (SquareHitbox squareHitbox in gameCore.WorldSquaresHitboxesList)
            {
                /* Check if there is a ground under rabbit */
                if ((((Canvas.GetLeft(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Width > squareHitbox.Left && (Canvas.GetLeft(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Width < squareHitbox.Right)
                    || Canvas.GetLeft(gameCore.RabbitRectangle) < squareHitbox.Right && Canvas.GetLeft(gameCore.RabbitRectangle) > squareHitbox.Left))
                    && squareHitbox.Top == Canvas.GetTop(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Height)
                    || SystemParameters.WorkArea.Bottom == Canvas.GetTop(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Height)
                {
                    rabbitAboveGround = true;
                    break;
                }
            }

            if (!rabbitAboveGround)
            {
                rabbitStateManager.ChangeMoveState(RabbitStateManager.MoveStateID.FALL);
            }
        }
    }
}
