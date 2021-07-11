using IsolateIsland.Runtime.Character;
using IsolateIsland.Runtime.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Interact
{
    internal interface IInteractable
    {
        void OnInteract(InteractEvent interactEvent);
    }

    
}