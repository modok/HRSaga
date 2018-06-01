using System;
namespace HRSaga.Entities
{
    public abstract class MissionGeneric
    {
        public Guid id { get; protected set; }
        private String status = "TODO";
        private int reward = 10;

        public MissionGeneric()
        {
            id = new Guid();   
        }

        public bool completed(){
            status = "DONE";
            Console.WriteLine("Mission completed!!");
            return true;
        }

        public int getReward(){
            if(status =="DONE"){
                return reward;
            }else{
                return 0;
            }
        }

    }
}
