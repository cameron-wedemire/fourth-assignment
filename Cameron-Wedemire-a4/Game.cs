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

        //munitions, friend and foe in same group but each has its own this.friend number (add later)
        Bullet[] Bullet = new Bullet[5];//array of bullets
        Missile[] Missile = new Missile[5];//array of missiles

        int levelNumber;

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
                Bullet[i].Setup(50, 50, new Vector2(300, 300), 5); //replace the first 3 numbers with the location of the thing that spawned them in later
            }

            //iterate over array, to setup each missile
            for (int i = 0; i < Missile.Length; i++)
            {
                //for each element in arrey, create new Missile
                Missile[i] = new Missile();
                //setup the munition that was just made
                Missile[i].Setup(50, 50, 0); //replace the numbers with the location of the thing that spawned them in later
            }

            //itterate over array, to setup each dust particle
            for (int i = 0; i < dustParticle.Length; i++)
            {
                //for each element in arrey, create new dust particle
                dustParticle[i] = new backgroundDust();
                //setup the dust particle that was just made
                dustParticle[i].Setup();
            }

            //character.Setup();
            character.Setup();

            levelNumber = 1;
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

            //move all the bullets
            for (int i = 0; i < Bullet.Length; i++)
            {
                //call the bullet update function for each bullet in the array
                Bullet[i].Update(character.playerSpeed.X, character.playerSpeed.Y);
            }

            //move all the missiles
            for (int i = 0; i < Missile.Length; i++)
            {
                //call the missile update function for each missile in the array
                Missile[i].Update(character.playerSpeed.X, character.playerSpeed.Y);
            }

            //character stuff move later maybe
            character.Update();

            //spawn player bullets
            if (Input.IsKeyboardKeyDown(KeyboardKey.Space))
            {
                foreach (Bullet daBullet in Bullet)
                {
                    if (daBullet.position != new Vector2(-50, -50))
                    {

                    }
                }
            }
        }
    }
}