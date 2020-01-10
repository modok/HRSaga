using EventFlow.ValueObjects;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class Wizard : ValueObject, ICharacter
    {
        public CharacterType CharacterType { get; }

        public Wizard(){
            this.CharacterType=CharacterType.Wizard;
        }

        
    }
}