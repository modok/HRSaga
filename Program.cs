namespace HRSaga
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Client that call Application OverTheReal
            Context.OverTheRealm.Application.ApplicationCaptainService applicationCaptainService =new Context.OverTheRealm.Application.ApplicationCaptainService();

            //I want to create a new captain
            Context.OverTheRealm.Domain.Model.Captains.CaptainId captainId = applicationCaptainService.commandNewCaptain();
            
            //I want to hire a Warrior
            applicationCaptainService.commandHireWarrior(captainId);
            
            //I want to hire a Wizard
            applicationCaptainService.commandHireWizard(captainId);

            //I ask if the quad is ready
            Context.OverTheRealm.Domain.Model.Captains.Captain captain = applicationCaptainService.queryCaptain(captainId);
            while(!captain.isSquadReady()){
                //if not ready, I want to hire new Wizard until the Squad is ready
                applicationCaptainService.commandHireWizard(captainId);
                captain = applicationCaptainService.queryCaptain(captainId);
            }

            //I want to go to the Tavern
            //appInTheTavernService.commandGoToTavern(captainId, new Tavern());

        }
    }
}
