using UnityEngine;

namespace Hacking.Mechanics
{
    public class DebugTestHackingMechanic : HackingMechanic
    {
        public override void Initialize()
        {
            Debug.Log("INITIALIZE");
        }

        public override void OnActivate()
        {
            Debug.Log("ACTIVATE");
        }

        public override void OnDeactivate()
        {
            Debug.Log("DEACTIVATE");
        }
    }
}