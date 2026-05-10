namespace SnakeOJTester
{
    public sealed class CStyleRandom
    {
        private uint _state;

        public CStyleRandom(int seed)
        {
            _state = (uint)seed;
        }

        public int Next()
        {
            _state = unchecked(_state * 1103515245u + 12345u);
            return (int)((_state / 65536u) % 32768u);
        }

        public int NextInt(int maxExclusive)
        {
            if (maxExclusive <= 0)
            {
                return 0;
            }
            return Next() % maxExclusive;
        }
    }
}
