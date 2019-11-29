using System.Collections.Generic;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Captains.Squads
{
    public class Wizard : Common.Domain.Model.ValueObject, ICharacter
    {
        public CharacterType CharacterType { get; }

        public Wizard(){
            this.CharacterType=CharacterType.Wizard;
        }
        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.CharacterType;
        }

        
    }
}