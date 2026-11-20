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
        //class calls
        //array of background dust particles
        backgroundDust[] dustParticle = new backgroundDust[50]; 
        //the player character
        Player character = new Player();

        //munitions, friend and foe in same group but each has its own this.friend number (add later)
        Bullet[] Bullet = new Bullet[5];//array of bullets
        Missile[] Missile = new Missile[5];//array of missiles
        Platform enemyPlatform = new Platform();

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

        //for platform bullet spawn
        Vector2 tempRotValue;
        Vector2 tempPosition;
        Vector2 offsetByLevel;

        //quick change for bullet and missile numbers so the index reset funciton at the bottom of the main update area doesnt break
        int numberOfBullets = 5;
        int numberOfMissiles = 5;

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
                Bullet[i].Setup(-50, -50, new Vector2(1, 1), 0, 0, false); //replace the first 3 numbers with the location of the thing that spawned them in later
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

            enemyPlatform.Setup();
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
                if (Missile[i].hasHitTarget == true)
                {
                    character.playerHP -= 10;//balence later
                    character.hasBeenHitThisTurn = true;
                    Missile[i].hasHitTarget = false;
                }
            }

            //character update
            character.Update();

            //spawn player bullets
            if (Input.IsMouseButtonDown(MouseButton.Left))
            {
                if (playerGunCooldown == 0 && character.playerHP > 0)
                {
                    Bullet[bulletSpawnIndex].Setup(300, 300, new Vector2(MathF.Cos((character.playerTurretRotation + 90) * (MathF.PI / 180)), MathF.Sin((character.playerTurretRotation + 90) * (MathF.PI / 180))), 7, 50, true);
                    bulletSpawnIndex += 1;

                    //reset bullet index if they are too high
                    if (bulletSpawnIndex >= numberOfBullets)
                    {
                        bulletSpawnIndex = 0;
                    }
                }
                playerGunCooldown += 1;
                if (playerGunCooldown > 40)
                {
                    playerGunCooldown = 0;
                }
            }

            //deside offset amount for platform guns on level number.. note this area needs to be redone whenever the platform scale gets changed in the code as fixing that would not be worth the lines of code
            if (enemyPlatform.levelNumber == 1)
            {
                offsetByLevel = new Vector2(30, 0);
            }
            else if (enemyPlatform.levelNumber == 3)
            {
                offsetByLevel = new Vector2(51, 51);
            }
            else
            {
                offsetByLevel = new Vector2(0, 0);
            }

            //move all the bullets
            for (int i = 0; i < Bullet.Length; i++)
            {
                if (Bullet[i].position != new Vector2(-50, -50))
                {
                    //call the bullet update function for each bullet in the array
                    Bullet[i].Update(character.playerSpeed.X, character.playerSpeed.Y);

                    //checks if it hits its target
                    if (Bullet[i].isPlayerBullet == false)
                    {
                        Bullet[i].hitTargetCheck(new Vector2(300, 300), new Vector2(0, 0), 60, character.playerHP);
                        //reset bullet and damage player if it has hit
                        if (Bullet[i].hasHitTarget == true)
                        {
                            character.playerHP -= 3;
                            Bullet[i].bulletLifeTime = 0;
                            character.hasBeenHitThisTurn = true;
                            Bullet[i].hasHitTarget = false;
                        }
                    }
                    else
                    {
                        //NOTE NEED TO FIX HIT RADIUS OF PLATFORMS

                        //check each platform
                        //check missile hub platform
                        Bullet[i].hitTargetCheck(enemyPlatform.position, new Vector2(0, 0), 50, enemyPlatform.missileTurretHp);
                        //reset bullet and damage hit platform if it hit
                        if (Bullet[i].hasHitTarget == true)
                        {
                            enemyPlatform.missileTurretHp -= 3;
                            Bullet[i].bulletLifeTime = 0;
                            enemyPlatform.missileTurretHit = true;
                            Bullet[i].hasHitTarget = false;
                        }

                        //check gun platform 1
                        Bullet[i].hitTargetCheck(enemyPlatform.position, new Vector2(-offsetByLevel.X, -offsetByLevel.Y), 25, enemyPlatform.gun1Hp);
                        //reset bullet and damage hit platform if it hit
                        if (Bullet[i].hasHitTarget == true)
                        {
                            enemyPlatform.gun1Hp -= 3;
                            Bullet[i].bulletLifeTime = 0;
                            enemyPlatform.gun1TurretHit = true;
                            Bullet[i].hasHitTarget = false;
                        }
                        //check gun platform 2
                        Bullet[i].hitTargetCheck(enemyPlatform.position, new Vector2(offsetByLevel.X, -offsetByLevel.Y), 25, enemyPlatform.gun2Hp);
                        //reset bullet and damage hit platform if it hit
                        if (Bullet[i].hasHitTarget == true)
                        {
                            enemyPlatform.gun2Hp -= 3;
                            Bullet[i].bulletLifeTime = 0;
                            enemyPlatform.gun2TurretHit = true;
                            Bullet[i].hasHitTarget = false;
                        }
                        //check gun platform 3
                        Bullet[i].hitTargetCheck(enemyPlatform.position, new Vector2(-offsetByLevel.X, offsetByLevel.Y), 25, enemyPlatform.gun3Hp);
                        //reset bullet and damage hit platform if it hit
                        if (Bullet[i].hasHitTarget == true)
                        {
                            enemyPlatform.gun3Hp -= 3;
                            Bullet[i].bulletLifeTime = 0;
                            enemyPlatform.gun3TurretHit = true;
                            Bullet[i].hasHitTarget = false;
                        }
                        //check gun platform 4
                        Bullet[i].hitTargetCheck(enemyPlatform.position, new Vector2(offsetByLevel.X, offsetByLevel.Y), 25, enemyPlatform.gun4Hp);
                        //reset bullet and damage hit platform if it hit
                        if (Bullet[i].hasHitTarget == true)
                        {
                            enemyPlatform.gun4Hp -= 3;
                            Bullet[i].bulletLifeTime = 0;
                            enemyPlatform.gun4TurretHit = true;
                            Bullet[i].hasHitTarget = false;
                        }
                    }
                    
                }
            }

            //draw the enemy platform which changes based on level
            enemyPlatform.Update(character.playerSpeed.X, character.playerSpeed.Y);

            //if i had multiple platforms at once i would have made this use platform[] in some way
            platformTurrets();
        }

        //advances weapon cooldowns on alive platform turrets
        public void platformTurrets()
        {
            //checks if turrets exist and then uses cooldown values, not nested as that would use more lines of code with else if statements
            //fire platform missile if it exists
            if (enemyPlatform.missileTurretHp > 0)
            {
                if (platformMissileCooldown == 0)
                {
                    Missile[missileSpawnIndex].Setup(enemyPlatform.position.X, enemyPlatform.position.Y, 50);
                    missileSpawnIndex += 1;
                    //reset missile index if they are too high
                    if (missileSpawnIndex >= numberOfMissiles)
                    {
                        missileSpawnIndex = 0;
                    }
                }
                platformMissileCooldown += 1;
                if (platformMissileCooldown > 300)
                {
                    platformMissileCooldown = 0;
                }
            }

            //the offset amount used in this area is desided before the bullet move function in the main update area

            //fire platform gun 1 if it exists
            if (enemyPlatform.gun1Hp > 0)
            {
                if (platformGunCooldown1 == 0)
                {
                    platformGunTurretsPositionAndGunSpawn(new Vector2(-offsetByLevel.X, -offsetByLevel.Y));
                }

                platformGunCooldown1 += 1;
                if (platformGunCooldown1 > 50)
                {
                    platformGunCooldown1 = 0;
                }
            }

            //fire platform gun 2 if it exists
            if (enemyPlatform.gun2Hp > 0)
            {
                if (platformGunCooldown2 == 0)
                {
                    platformGunTurretsPositionAndGunSpawn(new Vector2(offsetByLevel.X, -offsetByLevel.Y));
                }

                platformGunCooldown2 += 1;
                if (platformGunCooldown2 > 50)
                {
                    platformGunCooldown2 = 0;
                }
            }

            //fire platform gun 3 if it exists
            if (enemyPlatform.gun3Hp > 0)
            {
                if (platformGunCooldown3 == 0)
                {
                    platformGunTurretsPositionAndGunSpawn(new Vector2(-offsetByLevel.X, offsetByLevel.Y));
                }

                platformGunCooldown3 += 1;
                if (platformGunCooldown3 > 50)
                {
                    platformGunCooldown3 = 0;
                }
            }

            //fire platform gun 4 if it exists
            if (enemyPlatform.gun4Hp > 0)
            {
                if (platformGunCooldown4 == 0)
                {
                    platformGunTurretsPositionAndGunSpawn(new Vector2(offsetByLevel.X, offsetByLevel.Y));
                }

                platformGunCooldown4 += 1;
                if (platformGunCooldown4 > 50)
                {
                    platformGunCooldown4 = 0;
                }
            }
        }

        //this function is because it is the same thing for every gun platform unlike the code in the funciton above
        public void platformGunTurretsPositionAndGunSpawn(Vector2 offsetAmount)
        {
            //tempPosition = new Vector2(enemyPlatform.position.X - 10 - (enemyPlatform.levelNumber * 17), enemyPlatform.position.Y + (enemyPlatform.levelNumber * 17));
            //tempRotValue = Vector2.Normalize(tempPosition - new Vector2(300, 300));

            //trying to fix above comment
            tempPosition = new Vector2(enemyPlatform.position.X + offsetAmount.X, enemyPlatform.position.Y + offsetAmount.Y);
            tempRotValue = Vector2.Normalize(tempPosition - new Vector2(300, 300));

            Bullet[bulletSpawnIndex].Setup(tempPosition.X, tempPosition.Y, tempRotValue, 5, 40, false);//need target direction
            bulletSpawnIndex += 1;
            //reset bullet index if they are too high
            if (bulletSpawnIndex >= numberOfBullets)
            {
                bulletSpawnIndex = 0;
            }
        }
    }
}

//ToDo:
//fix public variables so only those that need to be public are public.. also do the same for the this. ones as i think some arnt needed to be that way
//if you have enough time try making the turrets not rotate through their own platform group... not gonna do

//things i might change;
//impliment window.width and height more