namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class Warrior : Common.Domain.Model.ValueObject, ICharacter
    {
        public CharacterType CharacterType { get; }

        public Warrior(){
            this.CharacterType=CharacterType.Warrior;
        }
        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return this.CharacterType;
        }

        
    }
}