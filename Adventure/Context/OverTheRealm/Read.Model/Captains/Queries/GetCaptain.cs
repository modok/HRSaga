using System;
using System.Collections.Generic;
using CQRSCode.Read.Model.Captains.Dtos;
using CQRSlite.Domain;
using CQRSlite.Queries;
using HRSaga.Adventure.Context.OverTheRealm.Domain.Model.Captains;

namespace CQRSCode.ReadModel.Captains.Queries
{
    public class GetCaptain : IQuery<CaptainDto>
    {
        public GetCaptain(CaptainId captainId)
        {
            CaptainId = captainId;
        }

        public CaptainId CaptainId { get; set; }
    }
}