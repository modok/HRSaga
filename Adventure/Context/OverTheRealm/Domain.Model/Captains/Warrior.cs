using EventFlow.ValueObjects;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class Warrior :  ValueObject, ICharacter
    {
        public CharacterType CharacterType { get; }

        public Warrior(){
            this.CharacterType=CharacterType.Warrior;
        }
        
    }
}