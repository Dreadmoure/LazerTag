using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class Animator : Component
    {
        #region fields
        private float timeElapsed;
        private SpriteRenderer spriteRenderer;
        //dictionaries for name and animation
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        private Animation currentAnimation;
        #endregion

        public int CurrentIndex { get; private set; }

        #region methods
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        }

        public override void Update()
        {
            timeElapsed += GameWorld.DeltaTime;

            CurrentIndex = (int)(timeElapsed * currentAnimation.FPS);

            if (CurrentIndex > currentAnimation.Sprites.Length - 1)
            {
                timeElapsed = 0;
                CurrentIndex = 0;
            }

            spriteRenderer.Sprite = currentAnimation.Sprites[CurrentIndex];
        }

        public void AddAnimation(Animation animation)
        {
            animations.Add(animation.Name, animation);

            if (currentAnimation == null)
            {
                currentAnimation = animation;
            }
        }

        public void PlayAnimation(string animationName)
        {
            if (animationName != currentAnimation.Name)
            {
                currentAnimation = animations[animationName];
                timeElapsed = 0;
                CurrentIndex = 0;
            }
        }

        #endregion
    }
}
