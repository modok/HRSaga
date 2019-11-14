
namespace HRSaga.Context.OverTheRealm
{
    public class Application
    {
        public Context.InTheTavern.Captain CaptainGoToTavern(Context.OverTheRealm.Captain caprtain,Context.OverTheRealm.Tavern tavern){
            Shared.CaptainId captainId = caprtain.goToTavern(tavern);
            return Context.InTheTavern.CaptainRepository.captainOfId(captainId);
        }
    }
    
}

