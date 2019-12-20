using System;
using EventFlow.Core;

namespace HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains
{
    public class CaptainId : Identity<CaptainId>
    {

        public CaptainId(string value):base(value){}
       
    }
}
