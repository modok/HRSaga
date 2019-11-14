using System;
using HRSaga.Artefacts;

namespace HRSaga.Artefacts
{
    public abstract class GuidIdenty : ValueObject
    {
        Guid _id;

        protected GuidIdenty(Guid id){
            this._id=id;
        }
        
        public override string ToString() => _id.ToString();
    }
}