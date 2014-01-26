namespace GrumpyRabbit.Entities.Dynamics
{
    public abstract class Animation
    {
        protected Character character;

        public Animation(Character character)
        {
            this.character = character;
        }

        public abstract void OnReset();

        public abstract void Update(int elapsedMilliseconds);
    }
}
