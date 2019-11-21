namespace HRSaga.Context.OverTheRealm.Domain.Model.Squads
{
    public class SquadService
    {
        public SquadService( ISquadRepository squadRepository)
        {
            this.SquadRepository = squadRepository;
        }
        readonly ISquadRepository SquadRepository;

        public void hire(SquadId squadId, Warrior warrior){
            Squad squad = this.SquadRepository.Get(squadId);
            squad.hire(warrior);
            this.SquadRepository.Save(squad);
            //return captain;
        }

    }
}