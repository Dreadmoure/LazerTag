using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class Battery : PickUp
    {
        private SpriteRenderer spriteRenderer;

        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRenderer.SetSprite("PickUps\\PickUpBattery");
            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;
            GameObject.Tag = "Battery";
        }

        public override void Update()
        {
            
        }
    }
}
