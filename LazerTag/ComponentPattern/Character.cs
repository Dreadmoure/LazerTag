﻿using LazerTag.CommandPattern;
using LazerTag.CreationalPattern;
using LazerTag.ObserverPattern;
using LazerTag.ObserverPattern.PlatformCollisionEvents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    /// <summary>
    /// enum Direction, for defining which direction the player should be turning 
    /// </summary>
    public enum Direction { Left, Right };

    public class Character : Component, IGameListener
    {
        #region fields
        private SpriteRenderer spriteRenderer;
        private Vector2 spriteSize;
        private Collider collider; 
        private Animator animator;

        private float speed;
        private float shootTimer;
        private float shootTime; 

        private Vector2 gravity;
        private bool canJump;
        private float jumpTime;

        // iframes 
        private float iframeTimer;
        private float iframeTime; 
        #endregion

        #region properties
        /// <summary>
        /// property for getting and setting the character index 
        /// </summary>
        public PlayerIndex CharacterIndex { get; set; }

        /// <summary>
        /// property for getting, and privately setting, a weapon gameobject 
        /// </summary>
        public GameObject WeaponObject { get; private set; }

        /// <summary>
        /// property for getting and setting the characters ammocount 
        /// </summary>
        public int AmmoCount { get; set; }

        /// <summary>
        /// property for getting and setting whether a character is walking
        /// </summary>
        public bool IsWalking { get; set; } = false;

        /// <summary>
        /// property for getting and setting whether a character is jumping
        /// </summary>
        public bool IsJumping { get; set; } = false;

        /// <summary>
        /// property for getting and setting the direction the character is turning 
        /// </summary>
        public Direction CharacterDirection { get; set; }

        //animation sprites
        /// <summary>
        /// property for getting and setting the walking sprites 
        /// </summary>
        public string[] WalkSprites { get; set; }

        /// <summary>
        /// property for getting and setting the idle sprites 
        /// </summary>
        public string[] IdleSprites { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// first method to run when character is initialized, sets all the common start parameters for the character 
        /// </summary>
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            spriteSize = new Vector2(spriteRenderer.Sprite.Width, spriteRenderer.Sprite.Height);

            animator = GameObject.GetComponent<Animator>() as Animator;
            AmmoCount = 5;
            speed = 250;

            shootTime = 0.5f;

            // iframes 
            iframeTime = 0.7f;
            iframeTimer = 0; 

            // set gravity, remember to multiply with speed 
            gravity = new Vector2(0, 0.9f) * speed;

            CharacterDirection = Direction.Right;

            SpawnWeapon();

            // set CollisionBox 
            collider = GameObject.GetComponent<Collider>() as Collider;
            collider.CollisionBox = new Rectangle(
                                                  (int)(GameObject.Transform.Position.X - spriteSize.X / 2),
                                                  (int)(GameObject.Transform.Position.Y - spriteSize.Y / 2),
                                                  (int)spriteSize.X,
                                                  (int)spriteSize.Y
                                                  );
        }

        /// <summary>
        /// method for updating the character during runtime 
        /// </summary>
        public override void Update()
        {
            //handles input
            InputHandler.Instance.Execute(this);
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            //walking animations
            if (IsWalking && CharacterDirection == Direction.Left)
            {
                animator.PlayAnimation("Walk");
                spriteRenderer.Flip = SpriteEffects.FlipHorizontally;
            }
            if (IsWalking && CharacterDirection == Direction.Right)
            {
                animator.PlayAnimation("Walk");
                spriteRenderer.Flip = SpriteEffects.None;
            }

            //idle animations
            if (!IsWalking && CharacterDirection == Direction.Left)
            {
                animator.PlayAnimation("Idle");
            }
            if (!IsWalking && CharacterDirection == Direction.Right)
            {
                animator.PlayAnimation("Idle");
            }

            // make character fall using gravity 
            GameObject.Transform.Translate(gravity * GameWorld.DeltaTime);

            // when character has jumped 
            if (IsJumping)
            {
                jumpTime += GameWorld.DeltaTime; 
                if (jumpTime <= 0.185f)
                {
                    Vector2 jumpVelocity = new Vector2(0, -5) * speed;
                    GameObject.Transform.Translate(jumpVelocity * GameWorld.DeltaTime);
                }
            }

            // update characters weapon position
            Weapon weapon = WeaponObject.GetComponent<Weapon>() as Weapon;
            weapon.Move(GameObject.Transform.Position);
            
            // update timers 
            shootTimer += GameWorld.DeltaTime;
            iframeTimer += GameWorld.DeltaTime;

            // update CollisionBox 
            if(collider.CollisionBox.X != GameObject.Transform.Position.X + spriteRenderer.Sprite.Width / 2 ||
               collider.CollisionBox.Y != GameObject.Transform.Position.Y + spriteRenderer.Sprite.Height / 2)
            {
                UpdateCollisionBox(); 
            }
        }

        /// <summary>
        /// private method for updating the characters collisionbox 
        /// </summary>
        private void UpdateCollisionBox()
        {
            collider.CollisionBox = new Rectangle(
                                                  (int)(GameObject.Transform.Position.X - spriteSize.X / 2),
                                                  (int)(GameObject.Transform.Position.Y - spriteSize.Y / 2),
                                                  (int)spriteSize.X,
                                                  (int)spriteSize.Y
                                                  );
        }

        /// <summary>
        /// method for handling the move command given as input 
        /// </summary>
        /// <param name="velocity">the direction the character should move in</param>
        public void Move(Vector2 velocity)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);
        }

        /// <summary>
        /// method for handling the aim command given as input 
        /// </summary>
        /// <param name="aimDirection">the direction the weapon should aim</param>
        public void Aim(Vector2 aimDirection)
        {
            // parse info along to the weapon 
            Weapon weapon = WeaponObject.GetComponent<Weapon>() as Weapon;
            weapon.Aim(aimDirection);
        }

        /// <summary>
        /// method for handling the shoot command given as input 
        /// </summary>
        public void Shoot()
        {
            // character can only shoot when they have ammo
            if (AmmoCount > 0)
            {
                // only shoot after a given time 
                if (shootTimer > shootTime)
                {
                    Weapon weapon = WeaponObject.GetComponent<Weapon>() as Weapon;

                    GameObject projectileObject = new GameObject();

                    if (weapon.ProjectileDirection == ProjectileDirection.Horizontal)
                    {
                        projectileObject = ProjectileFactory.Instance.Create(ProjectileDirection.Horizontal);
                    }
                    else
                    {
                        projectileObject = ProjectileFactory.Instance.Create(ProjectileDirection.Vertical);
                    }

                    projectileObject.Tag = GameObject.Tag;

                    // set projectiles position 
                    projectileObject.Transform.Position = weapon.ProjectileSpawnPosition;

                    // set projectiles velocity 
                    Projectile projectile = projectileObject.GetComponent<Projectile>() as Projectile;
                    projectile.Velocity = weapon.ProjectileVelocity;

                    // instantiate projectile in GameWorld 
                    GameWorld.Instance.Instantiate(projectileObject);

                    // decrease ammo, and reset timer 
                    AmmoCount--;
                    shootTimer = 0; 
                }
            }
        }

        /// <summary>
        /// method for handling the jump command given as input
        /// </summary>
        public void Jump()
        {
            // just check if the character can jump, if they are on the top of a platform 
            // the actual jump happens in Update method 
            if (canJump)
            {
                IsJumping = true;
                jumpTime = 0;

                canJump = false; 
            }
        }

        /// <summary>
        /// method for checking if an event happens that the character listens to 
        /// </summary>
        /// <param name="gameEvent">the gameevent that happened</param>
        public void Notify(GameEvent gameEvent)
        {
            if(gameEvent is CollisionEvent)
            {
                GameObject other = (gameEvent as CollisionEvent).Other; 

                // check for other characters projectiles 
                if(other.GetComponent<Projectile>() != null && other.Tag != GameObject.Tag)
                {
                    // destroy projectile 
                    GameWorld.Instance.Destroy(other);

                    // only destroy self, when iframe is over 
                    if (iframeTimer > iframeTime)
                    {
                        // update other players score 
                        Player otherPlayer = GameWorld.Instance.FindPlayerByTag(other.Tag);
                        otherPlayer.Score += 100;

                        // remove character from player 
                        Player player = GameWorld.Instance.FindPlayerByTag(GameObject.Tag);
                        player.RemoveCharacter();

                        // destroy weapon 
                        GameWorld.Instance.Destroy(WeaponObject);

                        // destroy self lastly
                        GameWorld.Instance.Destroy(GameObject);
                    }
                }

                // check for pick ups 
                if(other.Tag == "Battery")
                {
                    if(AmmoCount < 5)
                    {
                        AmmoCount = 5;

                        GameWorld.Instance.Destroy(other);
                    }
                }
            }

            // platform side collision events 
            if(gameEvent is TopCollisionEvent)
            {
                GameObject other = (gameEvent as TopCollisionEvent).Other;

                if(other.Tag == "Platform")
                {
                    // when character touches the top a platform, it can jump 
                    canJump = true; 

                    // get the platforms spriterenderer 
                    SpriteRenderer otherSpriteRenderer = other.GetComponent<SpriteRenderer>() as SpriteRenderer;

                    // set character to be on top of the platform, so it does not fall through 
                    GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, 
                                                                other.Transform.Position.Y - (otherSpriteRenderer.Origin.Y + spriteRenderer.Origin.Y));
                    
                    // since the character is moved, remember to update its collisionbox 
                    UpdateCollisionBox();
                }
            }
            if (gameEvent is BottomCollisionEvent)
            {
                GameObject other = (gameEvent as BottomCollisionEvent).Other;

                if (other.Tag == "Platform")
                {
                    // make sure character can not jump through platform 
                    IsJumping = false; 

                    // get the platforms spriterenderer 
                    SpriteRenderer otherSpriteRenderer = other.GetComponent<SpriteRenderer>() as SpriteRenderer;

                    // make sure character can not get up through platform 
                    GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X,
                                                                other.Transform.Position.Y + (otherSpriteRenderer.Origin.Y + spriteRenderer.Origin.Y));

                    // since the character is moved, remember to update its collisionbox 
                    UpdateCollisionBox();
                }
            }
            if (gameEvent is LeftCollisionEvent)
            {
                GameObject other = (gameEvent as LeftCollisionEvent).Other;

                if (other.Tag == "Platform")
                {
                    // get the platforms spriterenderer 
                    SpriteRenderer otherSpriteRenderer = other.GetComponent<SpriteRenderer>() as SpriteRenderer;

                    // make sure character can not move right into platform 
                    GameObject.Transform.Position = new Vector2(other.Transform.Position.X - (otherSpriteRenderer.Origin.X + spriteRenderer.Origin.X),
                                                                GameObject.Transform.Position.Y);

                    // since the character is moved, remember to update its collisionbox 
                    UpdateCollisionBox();
                }
            }
            if (gameEvent is RightCollisionEvent)
            {
                GameObject other = (gameEvent as RightCollisionEvent).Other;

                if (other.Tag == "Platform")
                {
                    // get the platforms spriterenderer 
                    SpriteRenderer otherSpriteRenderer = other.GetComponent<SpriteRenderer>() as SpriteRenderer;

                    // make sure character can not move left into platform 
                    GameObject.Transform.Position = new Vector2(other.Transform.Position.X + (otherSpriteRenderer.Origin.X + spriteRenderer.Origin.X),
                                                                GameObject.Transform.Position.Y);

                    // since the character is moved, remember to update its collisionbox 
                    UpdateCollisionBox();
                }
            }
        }

        /// <summary>
        /// method for building the characters animation 
        /// </summary>
        /// <param name="fps">frames per second</param>
        /// <param name="animationName">the string name of the animation</param>
        /// <param name="spriteNames">the sprites that should be used</param>
        /// <returns>returns the animation that was build</returns>
        public Animation BuildAnimation(int fps, string animationName, string[] spriteNames)
        {
            Texture2D[] sprites = new Texture2D[spriteNames.Length];

            for (int i = 0; i < spriteNames.Length; i++)
            {
                sprites[i] = GameWorld.Instance.Content.Load<Texture2D>(spriteNames[i]);
            }

            Animation animation = new Animation(fps, animationName, sprites);

            return animation;
        }

        /// <summary>
        /// method for spawning weapon for character 
        /// </summary>
        private void SpawnWeapon()
        {
            WeaponObject = WeaponFactory.Instance.Create(PlayerIndex.One);

            WeaponObject.Transform.Position = GameObject.Transform.Position;

            GameWorld.Instance.Instantiate(WeaponObject);
        }
        #endregion
    }
}
