using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GrumpyRabbit
{
    public class RabbitMoveGrab : RabbitState
    {
        public RabbitMoveGrab(GameCore gameCore, RabbitStateManager rabbitStateManager)
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
            double xTranslate = gameCore.DeltaMouseXPosition;
            double yTranslate = gameCore.DeltaMouseYPosition;

            gameCore.GameParameters["rabbitXSpeed"] = xTranslate / (double)elapsedMilliseconds;
            gameCore.GameParameters["rabbitYSpeed"] = yTranslate / (double)elapsedMilliseconds;

            Canvas.SetLeft(gameCore.RabbitRectangle, xTranslate + Canvas.GetLeft(gameCore.RabbitRectangle));
            Canvas.SetTop(gameCore.RabbitRectangle, yTranslate + Canvas.GetTop(gameCore.RabbitRectangle));
        }
    }
}
