using System.Collections.Generic;
using CQRSCode.Read.Model.Captains.Dtos;
using CQRSlite.Queries;

namespace CQRSCode.ReadModel.Captains.Queries
{
    public class GetCaptainList : IQuery<List<CaptainDto>>
    {
    }
}