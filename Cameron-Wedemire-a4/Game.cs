// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        //variables
        backgroundDust[] dustParticle = new backgroundDust[50]; //array of background dust particles
        Player character = new Player(); //the player character

        //these give guns and missile launchers a way to know which munition they are to move, each gun turret has 6 bullets and each missile turret has 2 missiles //NOT IMPLIMENTED YET
        public int bulletSpawnIndex = 0;
        public int missileSpawnIndex = 0;

        //gun cooldowns are used to fire after the cooling is done (so player can hold down fire and enemies can fire correctly)
        int playerGunCooldown = 0;
        int platformMissileCooldown = 0;
        int platformGunCooldown1 = 0;
        int platformGunCooldown2 = 0;
        int platformGunCooldown3 = 0;
        int platformGunCooldown4 = 0;

        //quick change for bullet and missile numbers so the index reset funciton at the bottom of the main update area doesnt break
        int numberOfBullets = 5;
        int numberOfMissiles = 5;

        //munitions, friend and foe in same group but each has its own this.friend number (add later)
        Bullet[] Bullet = new Bullet[5];//array of bullets
        Missile[] Missile = new Missile[5];//array of missiles
        Platform enemyPlatform = new Platform();

        ////the spawn index is for the missle and bullet spawn functions so it can tell which munition is ready to be spawned
        //int spawnIndex;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("space fight");
            Window.SetSize(600, 600);//change later if needed

            string cwd = System.IO.Directory.GetCurrentDirectory(); //cwd = current working directory
            Console.WriteLine($"CWD:{cwd}");

            //iterate over array, to setup each bullet
            for (int i = 0; i < Bullet.Length; i++)
            {
                //for each element in arrey, create new Bullet
                Bullet[i] = new Bullet();
                //setup the munition that was just made
                Bullet[i].Setup(-50, -50, new Vector2(1, 1), 0, 0); //replace the first 3 numbers with the location of the thing that spawned them in later
            }

            //iterate over array, to setup each missile
            for (int i = 0; i < Missile.Length; i++)
            {
                //for each element in arrey, create new Missile
                Missile[i] = new Missile();
                //setup the munition that was just made
                Missile[i].Setup(-50, -50, -21); //replace the numbers with the location of the thing that spawned them in later
            }

            //itterate over array, to setup each dust particle
            for (int i = 0; i < dustParticle.Length; i++)
            {
                //for each element in arrey, create new dust particle
                dustParticle[i] = new backgroundDust();
                //setup the dust particle that was just made
                dustParticle[i].Setup();
            }
            character.Setup();

            enemyPlatform.Setup(0, 0);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(25, 25, 25);

            //draw the background
            for (int i = 0; i < dustParticle.Length; i++)
            {
                //call the background update function for each dust particle in the array
                dustParticle[i].Update(character.playerSpeed.X, character.playerSpeed.Y);
            }

            //move all the missiles
            for (int i = 0; i < Missile.Length; i++)
            {
                //call the missile update function for each missile in the array
                Missile[i].Update(character.playerSpeed.X, character.playerSpeed.Y);
            }

            //character update and spawn bullets
            character.Update();

            if (Input.IsMouseButtonDown(MouseButton.Left))
            {
                if (playerGunCooldown == 0)
                {
                    Bullet[bulletSpawnIndex].Setup(300, 300, new Vector2(MathF.Cos((character.playerTurretRotation + 90) * (MathF.PI / 180)), MathF.Sin((character.playerTurretRotation + 90) * (MathF.PI / 180))), 7, 50);
                    bulletSpawnIndex += 1;
                }
                playerGunCooldown += 1;
                if (playerGunCooldown > 40)
                {
                    playerGunCooldown = 0;
                }
            }

            //move all the bullets
            for (int i = 0; i < Bullet.Length; i++)
            {
                if (Bullet[i].position != new Vector2(-50, -50))
                {
                    //call the bullet update function for each bullet in the array
                    Bullet[i].Update(character.playerSpeed.X, character.playerSpeed.Y);
                }
            }


            ////temp for testing
            ////spawn missiles
            //if (Input.IsKeyboardKeyPressed(KeyboardKey.K))
            //{
            //    //check all the missiles to find the first one that is ready to be spawned
            //    Missile[missileSpawnIndex].Setup(Random.Float(0,Window.Width), Random.Float(0, Window.Width), 50);
            //    missileSpawnIndex += 1;
            //}



            //draw the enemy platform which changes based on level
            enemyPlatform.Update(character.playerSpeed.X, character.playerSpeed.Y, updateThisRoundTrueFalse);

            //if i had multiple platforms at once i would have made this use platform[] in some way
            platformTurrets();

            //reset bullet and missile indexes if they are too high
            if (bulletSpawnIndex >= numberOfBullets)
            {
                bulletSpawnIndex = 0;
            }
            if (missileSpawnIndex >= numberOfMissiles)
            {
                missileSpawnIndex = 0;
            }
        }

        //advances weapon cooldowns on alive platform turrets
        public void platformTurrets()
        {
            //platformMissileCooldown, platformGunCooldown1
            //checks if turrets exist and then uses cooldown values, not nested as that would use more lines of code with else if statements
            //fire platform missile if it exists
            if (enemyPlatform.missileTurretHp != 0)
            {
                if (platformMissileCooldown == 0)
                {
                    Missile[missileSpawnIndex].Setup(enemyPlatform.position.X, enemyPlatform.position.Y, 50);
                    missileSpawnIndex += 1;
                }
                platformMissileCooldown += 1;
                if (platformMissileCooldown > 300)
                {
                    platformMissileCooldown = 0;
                }
            }
            
        }
    }
}

//ToDo:
//fix public variables so only those that need to be public are public
//add the missile dot thing to the position of the platform
//if you have enough time try to make a loop function that also teleports the bullets? either that or make a homing beacon to show you the direction to the enemy base
//if you have enough time try making the turrets not rotate through their own platform group

//give the enemies and player timers for weapon fire that tell it when it can fire (weapon cooldowns, maybe have the guns fire in 3 round bursts? for 3 round bursts you would need 6 bullets per gun..)
    //give the bullet class an index for which bullets have been fired

//things i might change;
//maybe turn the player aiming into mouse control instead of Q/E as I now have code for the enemy turrets that use vectors so it would be easier to impliment now
//impliment window.width and height more