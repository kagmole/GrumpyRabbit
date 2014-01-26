namespace GrumpyRabbit.Entities.Dynamics
{
    public class DragAnimation : Animation
    {
        private const int X_FRICTION = 1;
        private const int Y_FRICTION = 1;

        public DragAnimation(Character character)
            : base(character)
        {
        }

        public override void OnReset()
        {
            // TODO
        }

        public override void Update(int elapsedMilliseconds)
        {
            
        }
    }
}
