﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    /// <summary>
    /// Forfatter : Denni
    /// </summary>
    public class SpecialAmmo : PickUp
    {
        /// <summary>
        /// first method to be run, when the object is first initialized in GameWorld 
        /// </summary>
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRenderer.SetSprite("PickUps\\SpecialAmmo");
            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;
            GameObject.Tag = "SpecialAmmo";

            // remember to run super class method Start 
            base.Start();
        }
    }
}
