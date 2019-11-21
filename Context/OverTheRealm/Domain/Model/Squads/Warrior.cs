using System.Collections.Generic;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Squads
{
    public class Warrior : Character
    {
        public Warrior() : base(CharacterType.Warrior){}

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.getCharacterType();
        }
    }
}