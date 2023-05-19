using LazerTag.MenuStates;
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

        /// <summary>
        /// property for getting, and private set, for the CurrentIndex of which sprite to use 
        /// </summary>
        public int CurrentIndex { get; private set; }

        #region methods
        /// <summary>
        /// first method to run, when the component is first initialized 
        /// </summary>
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        }

        /// <summary>
        /// updates the component each frame 
        /// </summary>
        public override void Update()
        {
            timeElapsed += GameState.DeltaTime;

            CurrentIndex = (int)(timeElapsed * currentAnimation.FPS);

            if (CurrentIndex > currentAnimation.Sprites.Length - 1)
            {
                timeElapsed = 0;
                CurrentIndex = 0;
            }

            spriteRenderer.Sprite = currentAnimation.Sprites[CurrentIndex];
        }

        /// <summary>
        /// method that adds an animation to the animations dictionary 
        /// </summary>
        /// <param name="animation">the animation that should be added</param>
        public void AddAnimation(Animation animation)
        {
            animations.Add(animation.Name, animation);

            if (currentAnimation == null)
            {
                currentAnimation = animation;
            }
        }

        /// <summary>
        /// method for playing a given animation 
        /// </summary>
        /// <param name="animationName">the name of the animation to play</param>
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
