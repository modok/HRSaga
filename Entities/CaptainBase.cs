using System;
using System.Collections.Generic;
using HRSaga.ValueObjects;

namespace HRSaga.Entities
{
    public abstract class CaptainBase
    {
        protected Guid _id;
        protected List<Character> _squad;
        protected int _gold;
        protected int _maxSquadDimension = 5;

        protected CaptainBase(Context.OverTheRealm.Entities.Captain captain): this(captain._id,captain._squad,captain._gold)
        {
            
        }

        protected CaptainBase(Context.InTheTavern.Entities.Captain captain): this(captain._id, captain._squad, captain._gold)
        {
            
        }

        protected CaptainBase(Context.InMission.Entities.Captain captain) : this(captain._id, captain._squad, captain._gold)
        {

        }         

        protected CaptainBase(Guid id, List<Character> squad, int gold)
        {
            _id = id;
            _squad = squad;
            _gold = gold;
        }

        public int GetGold(){
            return _gold;
        }
    }

    public class SquadNotReady : Exception { }

    public class FullSquadException : Exception { }
}