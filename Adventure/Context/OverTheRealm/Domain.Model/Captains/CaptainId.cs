using System;
using CQRSlite.Domain;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class CaptainId : Identity
    {

        public CaptainId():base(){}
       
        public CaptainId(String id):base(id){}

        public CaptainId(Identity identity):base(identity.Id){}
        
        
    }
}
