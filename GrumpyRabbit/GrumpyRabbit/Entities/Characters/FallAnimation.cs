namespace GrumpyRabbit.Entities.Dynamics
{
    public class FallAnimation : Animation
    {
        public FallAnimation(Character character)
            : base(character)
        {

        }

        public override void OnReset()
        {
            // TODO
        }

        public override void Update(int elapsedMilliseconds)
        {
        //double newXSpeed = gameCore.GameParameters["rabbitXSpeed"] + xSpeedFactor * ((double)elapsedMilliseconds / 1000.0);
        //double newYSpeed = gameCore.GameParameters["rabbitYSpeed"] + ySpeedFactor * ((double)elapsedMilliseconds / 1000.0);
        //
        ///* X speed must stop when around 0 */
        //if (newXSpeed < 0 && xSpeedFactor < 0.0 || newXSpeed > 0 && xSpeedFactor > 0.0)
        //{
        //    xSpeedFactor = 0.0;
        //    newXSpeed = 0.0;
        //}
        //
        //gameCore.GameParameters["rabbitXSpeed"] = newXSpeed;
        //gameCore.GameParameters["rabbitYSpeed"] = newYSpeed;
        //
        //double xTranslate = gameCore.GameParameters["rabbitXSpeed"] * (double)elapsedMilliseconds;
        //double yTranslate = gameCore.GameParameters["rabbitYSpeed"] * (double)elapsedMilliseconds;
        //
        //double newXPosition = xTranslate + Canvas.GetLeft(gameCore.RabbitRectangle);
        //double newYPosition = yTranslate + Canvas.GetTop(gameCore.RabbitRectangle);
        //
        //double leftLimit = SystemParameters.WorkArea.Left;
        //double rightLimit = SystemParameters.WorkArea.Right;
        //double topLimit = -1;
        //double bottomLimit = SystemParameters.WorkArea.Bottom;
        //
        ///* Check if a RabbitBox is closer of a collision with the rabbit than one or more limits */
        //foreach (Hitbox squareHitbox in gameCore.WorldSquaresHitboxesList)
        //{
        //    /* Check if rabbit can hit the hitbox vertically */
        //    if ((Canvas.GetTop(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Height > squareHitbox.Top && (Canvas.GetTop(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Height < squareHitbox.Bottom)
        //        || Canvas.GetTop(gameCore.RabbitRectangle) < squareHitbox.Bottom && Canvas.GetTop(gameCore.RabbitRectangle) > squareHitbox.Top))
        //    {
        //        /* Check left limit */
        //        if (squareHitbox.Right <= Canvas.GetLeft(gameCore.RabbitRectangle) && squareHitbox.Right >= leftLimit)
        //        {
        //            leftLimit = squareHitbox.Right;
        //        }
        //
        //        /* Check right limit */
        //        if (squareHitbox.Left >= Canvas.GetLeft(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Width && squareHitbox.Left <= rightLimit)
        //        {
        //            rightLimit = squareHitbox.Left;
        //        }
        //    }
        //
        //    /* Check if rabbit can hit the hitbox horizontally */
        //    if ((Canvas.GetLeft(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Width > squareHitbox.Left && (Canvas.GetLeft(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Width < squareHitbox.Right)
        //        || Canvas.GetLeft(gameCore.RabbitRectangle) < squareHitbox.Right && Canvas.GetLeft(gameCore.RabbitRectangle) > squareHitbox.Left))
        //    {
        //        /* Check top limit - By default, there is no top limit (< 0) */
        //        if (squareHitbox.Bottom <= Canvas.GetTop(gameCore.RabbitRectangle) && squareHitbox.Bottom >= topLimit)
        //        {
        //            topLimit = squareHitbox.Bottom;
        //        }
        //
        //        /* Check bottom limit */
        //        if (squareHitbox.Top >= Canvas.GetTop(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Height && squareHitbox.Top <= bottomLimit)
        //        {
        //            bottomLimit = squareHitbox.Top;
        //        }
        //    }
        //}
        //
        ///* Reduces right and bottom limits (origin of the rabbit is top-left) */
        //rightLimit -= gameCore.RabbitRectangle.Width;
        //bottomLimit -= gameCore.RabbitRectangle.Height;
        //
        ///* Look for collisions */
        //if (newXPosition < leftLimit)
        //{
        //    newXPosition = leftLimit;
        //    xSpeedFactor = 0.0;
        //    gameCore.GameParameters["rabbitXSpeed"] = 0.0;
        //}
        //
        //if (newXPosition > rightLimit)
        //{
        //    newXPosition = rightLimit;
        //    xSpeedFactor = 0.0;
        //    gameCore.GameParameters["rabbitXSpeed"] = 0.0;
        //}
        //
        //if (topLimit > 0 && newYPosition < topLimit)
        //{
        //    newYPosition = topLimit;
        //    gameCore.GameParameters["rabbitYSpeed"] = 0.0;
        //}
        //
        //if (bottomLimit > SystemParameters.WorkArea.Top && newYPosition > bottomLimit)
        //{
        //    newYPosition = bottomLimit;
        //    ySpeedFactor = 0.0;
        //    gameCore.GameParameters["rabbitYSpeed"] = 0.0;
        //}
        //
        //Canvas.SetLeft(gameCore.RabbitRectangle, newXPosition);
        //Canvas.SetTop(gameCore.RabbitRectangle, newYPosition);
        //
        }
    }
}
