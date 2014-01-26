namespace GrumpyRabbit.Entities.Dynamics
{
    public class GrabAnimation : Animation
    {
        public GrabAnimation(Character character)
            : base(character)
        {

        }

        public override void OnReset()
        {
            // TODO
        }

        public override void Update(int elapsedMilliseconds)
        {
            /*            double xTranslate = gameCore.DeltaMouseXPosition;
            double yTranslate = gameCore.DeltaMouseYPosition;

            gameCore.GameParameters["rabbitXSpeed"] = xTranslate / (double)elapsedMilliseconds;
            gameCore.GameParameters["rabbitYSpeed"] = yTranslate / (double)elapsedMilliseconds;

            Canvas.SetLeft(gameCore.RabbitRectangle, xTranslate + Canvas.GetLeft(gameCore.RabbitRectangle));
            Canvas.SetTop(gameCore.RabbitRectangle, yTranslate + Canvas.GetTop(gameCore.RabbitRectangle));*/
        }
    }
}
