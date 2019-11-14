
namespace HRSaga.Context.InTheTavern
{
    public class Application
    {
        public Context.InMission.Captain acceptMission(Context.InTheTavern.Captain caprtain,Context.InTheTavern.Mission mission){
            Shared.CaptainId captainId = caprtain.acceptMission(mission);
            return Context.InMission.CaptainRepository.captainOfId(captainId);
        }

    }
    
}

