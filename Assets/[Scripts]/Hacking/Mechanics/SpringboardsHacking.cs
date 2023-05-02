using UnityEngine;
using System.Collections.Generic;
using Enviroment;

namespace Hacking.Mechanics
{

    public class SpringboardsHacking : HackingMechanic
    {
        private Springboard[] _springboards;
        public override void Initialize()
        {
            GameObject[] springboardObjects = GameObject.FindGameObjectsWithTag("Springboard");
            List<Springboard> tempList = new List<Springboard>();
            for (int i = 0; i < springboardObjects.Length; i++)
                tempList.Add(springboardObjects[i].GetComponent<Springboard>());
            
            _springboards = tempList.ToArray();
        }
        public override void OnActivate()
        {
            foreach (Springboard springboard in _springboards)
                springboard.StateChange();
        } 

        public override void OnDeactivate()
        {
            foreach (Springboard springboard in _springboards)
                springboard.StateChange();
        }
    }
}