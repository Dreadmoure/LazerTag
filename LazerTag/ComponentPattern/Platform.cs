using LazerTag.ObserverPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class Platform : Component
    {
        private SpriteRenderer spriteRenderer;
        private Vector2 position; 

        public Platform(Vector2 position)
        {
            this.position = position; 
        }

        #region methods 
        public override void Awake()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRenderer.SetSprite("Platforms\\tile1");
            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;

            GameObject.Transform.Position = new Vector2(position.X * spriteRenderer.Sprite.Width, position.Y * spriteRenderer.Sprite.Height);
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }
        #endregion
    }
}
