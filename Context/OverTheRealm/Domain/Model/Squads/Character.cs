using HRSaga.Context.Common.Domain.Model;


namespace HRSaga.Context.OverTheRealm.Domain.Model.Squads
{
    public abstract class Character : ValueObject
    {
        protected CharacterType CharacterType { get; private set;}

        protected Character(CharacterType characterType)
        {
            this.CharacterType=characterType;
        }
        protected CharacterType getCharacterType(){
            return this.CharacterType;
        }
        
        
    }
}