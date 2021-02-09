using System;
using ToDoList.Extensions;

namespace ToDoList.Modules
{
    public class RateLimiter : IRateLimiter
    {
        public int Burst { get; private set; }
        public TimeSpan Recover { get; private set; }

        public DateTime LastAccess { get; private set; } = default;

        private int _tokens;

        public int Tokens
        {
            get => LastAccess == default
                ? _tokens
                : (_tokens + (int)Math.Floor((DateTime.Now - LastAccess) / Recover)).Cap(Burst);

            private set => _tokens = value;
        }

        public RateLimiter(int _burst, TimeSpan _recover)
        {
            Burst = _burst;
            Recover = _recover;

            Tokens = Burst;
        }

        public bool TryReserve()
        {
            if (Tokens == 0)
                return false;

            Tokens--;
            LastAccess = DateTime.Now;

            return true;
        }
    }
}
