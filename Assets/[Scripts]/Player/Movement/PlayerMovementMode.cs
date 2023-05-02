using System;
using UnityEngine;

//Abstract class that indicates you can use class that uses it as player movemnt mode
namespace Player.Movement
{
    public abstract class PlayerMovementMode : MonoBehaviour
    {
        protected virtual void Awake()
        {
            throw new NotImplementedException();
        }
    }
}
