namespace GrumpyRabbit.Entities.Dynamics
{
    public abstract class Character : Entity
    {
        protected int weight;

        protected int xSpeed;
        protected int ySpeed;

        protected Animation currentAnimation;

        public Character(int id, int left, int top, int right, int bottom, int weight)
            : base(id, left, top, right, bottom)
        {
            this.weight = weight;

            xSpeed = 0;
            ySpeed = 0;

            currentAnimation = null;
        }

        protected void ChangeAnimation(Animation animation)
        {
            currentAnimation = animation;
            currentAnimation.OnReset();
        }

        public override void Update(int elapsedMilliseconds)
        {
            currentAnimation.Update(elapsedMilliseconds);
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int XSpeed
        {
            get { return xSpeed; }
            set { xSpeed = value; }
        }

        public int YSpeed
        {
            get { return ySpeed; }
            set { ySpeed = value; }
        }
    }
}
