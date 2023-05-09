using LazerTag.CommandPattern;
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
    public enum Direction { Left, Right };

    public class Character : Component, IGameListener
    {
        #region fields
        private SpriteRenderer spriteRenderer;
        private Animator animator;

        private float speed;
        private bool canShoot;
        private float shootTimer;
        private float shootTime; 

        private Vector2 gravity;
        private bool canJump;
        //private bool isJumping;
        private float jumpTime;
        #endregion

        public GameObject WeaponObject { get; private set; }

        public int AmmoCount { get; set; }

        public int CharacterId { get; set; }

        public bool IsWalking { get; set; } = false;
        public bool IsJumping { get; set; } = false;
        public Direction CharacterDirection { get; set; }

        //animation sprites
        public string[] walkSprites { get; set; }
        public string[] idleSprites { get; set; }

        #region methods
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            animator = GameObject.GetComponent<Animator>() as Animator;
            AmmoCount = 5;
            speed = 250;

            canShoot = true;
            shootTime = 0.5f;

            // set gravity, remember to multiply with speed 
            gravity = new Vector2(0, 0.9f) * speed;

            CharacterDirection = Direction.Right;

            SpawnWeapon();
        }

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
                if (jumpTime <= 0.15f)
                {
                    Vector2 jumpVelocity = new Vector2(0, -5) * speed;
                    GameObject.Transform.Translate(jumpVelocity * GameWorld.DeltaTime);
                }
                //else
                //{
                //    IsJumping = false; 
                //}
            }

            //weapon position
            Weapon weapon = WeaponObject.GetComponent<Weapon>() as Weapon;

            weapon.Move(GameObject.Transform.Position);

            
            shootTimer += GameWorld.DeltaTime;
            
        }

        public void Move(Vector2 velocity)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);


        }

        public void Aim(Vector2 aimDirection)
        {
            Weapon weapon = WeaponObject.GetComponent<Weapon>() as Weapon;

            weapon.Aim(aimDirection);
        }

        public void Shoot()
        {
            // instead of canShoot, use ammo count 
            if (AmmoCount > 0)
            {
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

                    // set position 
                    projectileObject.Transform.Position = weapon.ProjectileSpawnPosition;

                    // set velocity 
                    Projectile projectile = projectileObject.GetComponent<Projectile>() as Projectile;
                    projectile.Velocity = weapon.ProjectileVelocity;

                    // instantiate projectile in GameWorld 
                    GameWorld.Instance.Instantiate(projectileObject);

                    //canShoot = true; // ammocount -1
                    AmmoCount--;
                    shootTimer = 0; 
                }
            }
        }

        public void Jump()
        {
            if (canJump)
            {
                IsJumping = true;
                jumpTime = 0;

                canJump = false; 
            }
        }

        public void Notify(GameEvent gameEvent)
        {
            if(gameEvent is CollisionEvent)
            {
                GameObject other = (gameEvent as CollisionEvent).Other; 

                // check for pixel collision here 

                // check for other characters projectiles 
                if(other.GetComponent<Projectile>() != null && other.Tag != GameObject.Tag)
                {
                    GameWorld.Instance.Destroy(WeaponObject);

                    // destroy projectile 
                    GameWorld.Instance.Destroy(other);

                    // update other players score 
                    Player otherPlayer = GameWorld.Instance.FindPlayerByTag(other.Tag);
                    otherPlayer.Score += 100; 

                    // remove character from player 
                    Player player = GameWorld.Instance.FindPlayerByTag(GameObject.Tag);
                    player.RemoveCharacter(); 

                    GameWorld.Instance.Destroy(GameObject);
                }

                // check for pick ups 
                if(other.Tag == "PickUp")
                {

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
                }
            }
        }

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

        private void SpawnWeapon()
        {
            WeaponObject = WeaponFactory.Instance.Create(PlayerIndex.One);

            WeaponObject.Transform.Position = GameObject.Transform.Position;

            GameWorld.Instance.Instantiate(WeaponObject);

        }
        #endregion
    }
}
