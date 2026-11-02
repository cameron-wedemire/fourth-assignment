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
        //Bullets[] Bullet = new Bullet[5];//array of bullets
        //Missiles[] Missile = new Missile[5];//array of bullets

        int levelNumber;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("space fight");
            Window.SetSize(600, 600);//change later if needed

            ////iterate over array, to setup each bullet
            //for (int i = 0; i < Bullets.Length; i++)
            //{
            //    //for each element in arrey, create new Bullet
            //    Bullets[i] = new Bullet();
            //    //setup the munition that was just made
            //    Bullets[i].Setup();
            //}

            ////iterate over array, to setup each missile
            //for (int i = 0; i < Missiles.Length; i++)
            //{
            //    //for each element in arrey, create new Missile
            //    Missiles[i] = new Missile();
            //    //setup the munition that was just made
            //    Missiles[i].Setup();
            //}

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
                dustParticle[i].Update(character.playerSpeedX, character.playerSpeedY);
            }
            
            //character stuff move later maybe
            character.Update();
        }
        public void movementCalls()
        {
            
        }
    }
}