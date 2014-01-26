namespace GrumpyRabbit.Entities.Dynamics
{
    public class IdleAnimation : Animation
    {
        public IdleAnimation(Character character)
            : base(character)
        {

        }

        public override void OnReset()
        {
            // TODO
        }

        public override void Update(int elapsedMilliseconds)
        {
            /*bool rabbitAboveGround = false;*/
            
            //foreach (Hitbox squareHitbox in gameCore.WorldSquaresHitboxesList)
            //{
            //    /* Check if there is a ground under rabbit */
            //    if ((((Canvas.GetLeft(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Width > squareHitbox.Left && (Canvas.GetLeft(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Width < squareHitbox.Right)
            //        || Canvas.GetLeft(gameCore.RabbitRectangle) < squareHitbox.Right && Canvas.GetLeft(gameCore.RabbitRectangle) > squareHitbox.Left))
            //        && squareHitbox.Top == Canvas.GetTop(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Height)
            //        || SystemParameters.WorkArea.Bottom == Canvas.GetTop(gameCore.RabbitRectangle) + gameCore.RabbitRectangle.Height)
            //    {
            //        rabbitAboveGround = true;
            //        break;
            //    }
            //}
            //
            //if (!rabbitAboveGround)
            //{
            //    rabbitStateManager.ChangeMoveState(RabbitStateManager.MoveStateID.FALL);
            //}*/
        }
    }
}
