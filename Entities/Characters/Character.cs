using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;


namespace HRSaga.Entity.Characters
{
    public abstract class Character
    {
        public Guid id { get; protected set; }
        protected ILogger logger;


    }
}