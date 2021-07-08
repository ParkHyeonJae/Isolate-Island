using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class RenderManager : IManagerInit, IManagerUpdate
    {
        SpriteRenderer[] spriteRenderers;

        public void OnInit()
        {
            
        }

        public void OnUpdate()
        {
            spriteRenderers = GameObject.FindObjectsOfType<SpriteRenderer>();

            foreach (var item in spriteRenderers)
            {
                item.sortingOrder = (int)item.transform.position.y * -100;
            }
        }
    }
}