namespace KggGz3
{
    public class FromTo
    {
        public Triangle From { get; }
        public Triangle To { get; }

        public FromTo(Triangle from, Triangle to)
        {
            From = from;
            To = to;
        }

        private bool Equals(FromTo other)
        {
            return Equals(From, other.From) && Equals(To, other.To);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FromTo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((From?.GetHashCode() ?? 0)*397) ^ (To?.GetHashCode() ?? 0);
            }
        }

        public override string ToString()
        {
            return $"{nameof(From)}: {From}, {nameof(To)}: {To}";
        }
    }
}