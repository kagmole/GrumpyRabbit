namespace GrumpyRabbit.Entities.Dynamics
{
    public class RabbitCharacter : Character
    {
        public RabbitCharacter(int id, int left, int top, int right, int bottom, int weight)
            : base(id, left, top, right, bottom, weight)
        {
            currentAnimation = new FallAnimation(this);
        }

        public override void LookForCollisions()
        {
            // TODO
        }
    }
}
