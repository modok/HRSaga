using System;
using System.Collections.Generic;
using HRSaga.Artefacts;

namespace HRSaga.Context.OverTheRealm
{
    public class Squad  : ValueObject
    {
        //private SquadId _squadId;
        private static int _maxSquadSize = 5;
        private List<Warrior> _warriorList;
        private List<Wizard> _wizardList;

        public Squad(){
            _warriorList = new List<Warrior>();
            _wizardList = new List<Wizard>();
        }

        public Squad(int numWarriors, int numWizard){
            _warriorList = new List<Warrior>();
            _wizardList = new List<Wizard>();
            while(numWarriors>0){
                hire(new Warrior());
                numWarriors--;
            }
            while(numWizard>0){
                hire(new Wizard());
                numWizard--;
            }            
        }

        public void hire(Wizard wizard){
            if(isTheSquadFull()){
                throw new InvalidOperationException("The squad is full");
            }
            _wizardList.Add(wizard);
        }
        public void hire(Warrior warrior){
            if(isTheSquadFull()){
                throw new InvalidOperationException("The squad is full");
            }
            _warriorList.Add(warrior);
        }
        public Boolean isTheSquadFull(){
            if(this.size() < _maxSquadSize){
                return false;
            }
            return true;
        }

        public int size(){
            return _warriorList.Count + _wizardList.Count;
        }

        public int numWarriors(){
            return _warriorList.Count;
        }

        public int numWizard(){
            return _wizardList.Count;
        }

    }
}