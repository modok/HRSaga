using System;
using System.Collections.Generic;
using CQRSCode.Read.Model.Captains.Dtos;
using CQRSlite.Domain;

namespace HRSaga.Adventure.Context.OverTheRealm.Read.Model.Captains.Infrastructure
{
    public static class InMemoryDatabase 
    {
        public static readonly List<CaptainDto> List = new List<CaptainDto>();
        public static readonly Dictionary<Identity, CaptainDto> Details = new Dictionary<Identity,CaptainDto>();
    }
}