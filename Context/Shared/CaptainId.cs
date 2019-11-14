using System;
using HRSaga.Artefacts;

namespace HRSaga.Context.Shared
{
    public class CaptainId : GuidIdenty
    {   
        public CaptainId(Guid id):base(id){}

        public CaptainId(String id): base(new Guid(id)){}

    }
}