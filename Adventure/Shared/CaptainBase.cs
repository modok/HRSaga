using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Adventure.Shared
{
    public abstract class CaptainBase
    {
        protected List<Adventurer> _squad;
        protected ILogger _logger;
        public Guid id { get; protected set; }

        public int Gold { get; protected set; }

        public IEnumerable<Adventurer> Squad => _squad.AsReadOnly();
        public Mission Mission { get; protected set; }

        public Location Location { get; protected set; }
    }
}