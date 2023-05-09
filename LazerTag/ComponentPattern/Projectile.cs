using LazerTag.ObserverPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class Projectile : Component, IGameListener
    {
        private SpriteRenderer spriteRenderer;
        private float speed;
        private Vector2 velocity;

        public Projectile()
        {

        }

        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        }

        public override void Update()
        {
            
        }

        private void Move()
        {

        }

        public void Notify(GameEvent gameEvent)
        {
            
        }
    }
}
