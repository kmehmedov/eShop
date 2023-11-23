namespace Order.Domain
{
    public abstract class Entity<T>
    {
        int _id;
        public virtual int Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }

        public bool IsTransient()
        {
            return this.Id == default;
        }
    }
}
