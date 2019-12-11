using System;
using System.Collections.Generic;
using CQRSCode.Read.Model.Captains.Dtos;

namespace HRSaga.Adventure.Context.OverTheRealm.Read.Model.Captains.Infrastructure
{
    public static class InMemoryDatabase 
    {
        public static readonly List<CaptainDto> List = new List<CaptainDto>();
        public static readonly Dictionary<Guid, CaptainDto> Details = new Dictionary<Guid,CaptainDto>();
    }
}