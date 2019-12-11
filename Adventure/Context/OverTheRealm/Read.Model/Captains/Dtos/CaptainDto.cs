using System;

namespace CQRSCode.Read.Model.Captains.Dtos
{
    public class CaptainDto
    {
        public Guid Id {get; set;}
        public int warriors {get; set;}
        public int wizard {get; set;}
        public CaptainDto(Guid id)
        {
            this.Id = id;
            this.warriors=0;
            this.wizard=0;
        }
    }
}