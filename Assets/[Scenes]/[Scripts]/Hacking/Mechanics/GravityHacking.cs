using UnityEngine;
using System.Collections.Generic;

namespace Hacking.Mechanics
{
    
    public class GravityHacking : HackingMechanic
    {
        public override void Initialize()
        {
            Debug.Log("INITIALIZE");
        }

        public override void OnActivate()
        {
            Physics2D.gravity = new Vector2(0, 9.81f);
        }

        public override void OnDeactivate()
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
        }
    }
}