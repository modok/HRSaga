using System.Collections.Generic;

namespace HRSaga.Context.OverTheRealm.Domain.Model.Squads
{
    public class Wizard : Character
    {
        public Wizard() : base(CharacterType.Wizard){}
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.getCharacterType();
        }
    }
}