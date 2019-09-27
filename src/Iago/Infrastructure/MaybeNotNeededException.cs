using System;

namespace Iago.Common
{
    public class MaybeNotNeededException : Exception
    {
        public MaybeNotNeededException(string message = "") : base(message) {}
    }
}