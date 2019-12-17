using System;
using CQRSlite.Domain;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains;

namespace CQRSCode.Read.Model.Captains.Dtos
{
    public class CaptainDto
    {
        public CaptainId CaptainId {get; set;}
        public int warriors {get; set;}
        public int wizard {get; set;}
        public bool squadIsFull {get; set;}
        public CaptainDto(CaptainId captainId)
        {
            this.CaptainId = captainId;
            this.warriors=0;
            this.wizard=0;
        }
    }
}