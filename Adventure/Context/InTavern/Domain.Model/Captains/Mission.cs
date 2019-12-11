using System.Collections.Generic;

namespace HRSaga.Adventure.Context.InTavern.Domain.Model.Captains
{
    public class Mission : Common.Domain.Model.ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}