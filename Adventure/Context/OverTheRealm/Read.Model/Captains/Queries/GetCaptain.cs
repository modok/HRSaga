using System;
using System.Collections.Generic;
using CQRSCode.Read.Model.Captains.Dtos;
using CQRSlite.Queries;

namespace CQRSCode.ReadModel.Captains.Queries
{
    public class GetCaptain : IQuery<CaptainDto>
    {
        public GetCaptain(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}