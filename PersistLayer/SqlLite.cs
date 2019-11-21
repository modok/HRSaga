using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace HRSaga.PersistLayer
{
    public class CaptainPersisted
    {
        public static string sqlTableDrop ="DROP TABLE IF EXISTS captain";
        public static string sqlTableCreation =@"CREATE TABLE captain(
                guid TEXT PRIMARY KEY,
                num_warrior INT NOT NULL DEFAULT 0,
                num_wizzard INT NOT NULL DEFAULT 0,
                isInTheTavern BOOL NOT NULL DEFAULT FALSE,
                hasMission BOOL NOT NULL DEFAULT FALSE,
                gold INT NOT NULL DEFAULT 0
                )";
        private string _guid;
        private int _num_warrior;
        private int _num_wizzard;
        private bool _isInTheTavern;
        private bool _hasMission;
        private int _gold;

        public CaptainPersisted(string guid, int num_warrior, int num_wizzard,bool isInTheTavern, bool hasMission, int gold){
            _guid = guid;
            _num_warrior = num_warrior;
            _num_wizzard = num_wizzard;
            _isInTheTavern = isInTheTavern;
            _hasMission = hasMission;
            _gold = gold;

        }
        /* public CaptainPersisted(Context.InTheTavern.Captain captain){
            _guid=captain.getCaptainId().ToString();
           // _num_warrior=captain.getSquad().numWarriors();
           // _num_wizzard=captain.getSquad().numWizard();
            _isInTheTavern=captain.isInTavern();
        } */

        public CaptainPersisted(Context.OverTheRealm.Domain.Model.Captain.CaptainId captain){
           // _guid=captain.getCaptainId().ToString();
           // _isInTheTavern=(captain.getTavern()!= null?true:false);
           // _hasMission=captain.hasMission();
        }
        
        public CaptainPersisted(Context.InMission.Captain captain){
            _guid=captain.getCaptainId().ToString();
            _hasMission=(captain.getMission()!= null?true:false);
            _gold = captain.getGold();
        }

        public string Guid { get => _guid; }
        public int Num_warrior { get => _num_warrior; }
        public int Num_wizzard { get => _num_wizzard; }
        public bool IsInTheTavern { get => _isInTheTavern; }
        public bool HasMission { get => _hasMission; }
        public int Gold { get => _gold; }
        
    }
    public class SqlLite
    {
        //private static string _cs = "Data Source=:memory:";
        private static string _cs = @"URI=file:./test.db";
        private static SQLiteConnection _con = null;
        private static SQLiteCommand _cmd = null;

        private SQLiteCommand getSQLiteCommand(){
            if(_con == null || _cmd == null){
                _con = new SQLiteConnection(_cs);
                _con.Open();
                _cmd = new SQLiteCommand(_con);
            }
            _cmd.Reset();
            return _cmd;
        }

        public void initializzateCaptainTable(bool initializzateCaptainTable)
        {
            if(initializzateCaptainTable){
                //clean table
                getSQLiteCommand().CommandText =CaptainPersisted.sqlTableDrop;
                getSQLiteCommand().ExecuteNonQuery();
                //create table
                getSQLiteCommand().CommandText = CaptainPersisted.sqlTableCreation;
                getSQLiteCommand().ExecuteNonQuery();    
            }
        }
        
        public void newCaptain(Context.OverTheRealm.Domain.Model.Captain.CaptainId captainId){
            getSQLiteCommand().CommandText = "INSERT INTO captain(guid) VALUES(@guid)";
            getSQLiteCommand().Prepare();

            getSQLiteCommand().Parameters.AddWithValue("@guid", captainId.ToString());
            getSQLiteCommand().ExecuteNonQuery();
        }

        public void saveCaptain(Context.OverTheRealm.Domain.Model.Captain.CaptainId captain){
            CaptainPersisted captainPersisted = new CaptainPersisted(captain); 
            
            getSQLiteCommand().CommandText = "UPDATE captain SET num_warrior =@num_warrior, num_wizzard=@num_wizzard, isInTheTavern=@isInTheTavern WHERE guid=@guid";
            getSQLiteCommand().Prepare();

            getSQLiteCommand().Parameters.AddWithValue("@guid", captainPersisted.Guid);
            getSQLiteCommand().Parameters.AddWithValue("@num_warrior", captainPersisted.Num_warrior);
            getSQLiteCommand().Parameters.AddWithValue("@num_wizzard", captainPersisted.Num_wizzard);
            getSQLiteCommand().Parameters.AddWithValue("@isInTheTavern", captainPersisted.IsInTheTavern);
            getSQLiteCommand().ExecuteNonQuery();
        }

        public void saveCaptain(Context.InTheTavern.Captain captain){
            //CaptainPersisted captainPersisted = new CaptainPersisted(captain); 
            
            getSQLiteCommand().CommandText = "UPDATE captain SET isInTheTavern=@isInTheTavern, hasMission=@hasMission WHERE guid=@guid";
            getSQLiteCommand().Prepare();

            //getSQLiteCommand().Parameters.AddWithValue("@guid", captainPersisted.Guid);
            //getSQLiteCommand().Parameters.AddWithValue("@isInTheTavern", captainPersisted.IsInTheTavern);
            //getSQLiteCommand().Parameters.AddWithValue("@hasMission", captainPersisted.HasMission);
            getSQLiteCommand().ExecuteNonQuery();

        }

        public void saveCaptain(Context.InMission.Captain captain){
            CaptainPersisted captainPersisted = new CaptainPersisted(captain); 

            getSQLiteCommand().CommandText = "UPDATE captain SET hasMission=@hasMission, gold=@gold WHERE guid=@guid";
            getSQLiteCommand().Prepare();

            getSQLiteCommand().Parameters.AddWithValue("@guid", captainPersisted.Guid);
            getSQLiteCommand().Parameters.AddWithValue("@hasMission", captainPersisted.HasMission);
            getSQLiteCommand().Parameters.AddWithValue("@gold", captainPersisted.Gold);

            getSQLiteCommand().ExecuteNonQuery();
        }

        public CaptainPersisted getCaptain(string guid){
            CaptainPersisted captain = null;

            getSQLiteCommand().CommandText = "SELECT guid,num_warrior,num_wizzard,isInTheTavern,hasMission,gold FROM captain where guid=@guid";
            getSQLiteCommand().Prepare();

            getSQLiteCommand().Parameters.AddWithValue("@guid", guid);
            SQLiteDataReader rdr = getSQLiteCommand().ExecuteReader();

            while (rdr.Read()){                
                /*guid TEXT PRIMARY KEY,
                num_warrior INT,
                num_wizzard INT,
                isInTheTavern BOOL,
                hasMission BOOL*/
                captain = new CaptainPersisted(
                    rdr.GetString(0),
                    rdr.GetInt32(1),
                    rdr.GetInt32(2),
                    rdr.GetBoolean(3),
                    rdr.GetBoolean(4),
                    rdr.GetInt32(5)
                    );
            }
            return captain;
        }

        public Collection<CaptainPersisted> getCaptains(){
            Collection<CaptainPersisted> list =new Collection<CaptainPersisted>();
            
            getSQLiteCommand().CommandText = "SELECT guid,num_warrior,num_wizzard,isInTheTavern,hasMission,gold FROM captain";
            SQLiteDataReader rdr = getSQLiteCommand().ExecuteReader();

            while (rdr.Read()){                
                /*guid TEXT PRIMARY KEY,
                num_warrior INT,
                num_wizzard INT,
                isInTheTavern BOOL,
                hasMission BOOL*/
                list.Add(new CaptainPersisted(
                    rdr.GetString(0),
                    rdr.GetInt32(1),
                    rdr.GetInt32(2),
                    rdr.GetBoolean(3),
                    rdr.GetBoolean(4),
                    rdr.GetInt32(5)
                    ));
            }
            
            return list;

        }
        

    }
}