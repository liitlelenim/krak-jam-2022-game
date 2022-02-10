using System;
using UnityEngine;

namespace Hacking.Mechanics
{
    public abstract class HackingMechanic : MonoBehaviour
    {
        public virtual void Initialize(){}
        public virtual void OnActivate(){}
        public virtual void OnDeactivate(){}
    }
}