using System;

namespace HRSaga.Adventure.Context.InTavern.Domain.Model.Captains
{
    public class SignoffNotPossibleWithASquadNotReady : Exception
    {
        public SignoffNotPossibleWithASquadNotReady() { }
        public SignoffNotPossibleWithASquadNotReady(string message) : base(message) { }
        public SignoffNotPossibleWithASquadNotReady(string message, System.Exception inner) : base(message, inner) { }
        protected SignoffNotPossibleWithASquadNotReady(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class CaptainHasAlreadySignedMission : Exception
    {
        public CaptainHasAlreadySignedMission() { }
        public CaptainHasAlreadySignedMission(string message) : base(message) { }
        public CaptainHasAlreadySignedMission(string message, System.Exception inner) : base(message, inner) { }
        protected CaptainHasAlreadySignedMission(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class SquadSizetooBig : Exception
    {
        public SquadSizetooBig() { }
        public SquadSizetooBig(string message) : base(message) { }
        public SquadSizetooBig(string message, System.Exception inner) : base(message, inner) { }
        protected SquadSizetooBig(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    
}