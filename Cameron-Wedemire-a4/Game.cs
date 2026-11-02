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
        backgroundStar[] backgroundStars = new backgroundStar[50]; //array of background stars

        //munitions, friend and foe in same group but each has its own this.friend number (add later)
        //Bullets[] Bullet = new Bullet[5];//array of bullets
        //Missiles[] Missile = new Missile[5];//array of bullets

        int levelNumber;
        Vector2 tempPLayerSpeed = new Vector2(0, 0); // will be deleted once line below is implimented in player class
        //Vector2 playerSpeed = new Vector2(0, 0); // this will be put in the player class later

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

            //itterate over array, to setup each star
            for (int i = 0; i < backgroundStars.Length; i++)
            {
                //for each element in arrey, create new star
                backgroundStars[i] = new backgroundStar();
                //setup the star that was just made
                backgroundStars[i].Setup();
            }

            //character.Setup();
            tempPLayerSpeed.X = 0; // will be deleted once line below is implimented in player class
            tempPLayerSpeed.Y = 0;

            levelNumber = 1;
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(25, 25, 25);

            //draw the background
            for (int i = 0; i < backgroundStars.Length; i++)
            {
                //call the background update function for each star in the array

                tempPLayerSpeed.X += Random.Integer(-1, 2); // will be deleted once line below is implimented in player class
                tempPLayerSpeed.Y += Random.Integer(-1, 2);

                backgroundStars[i].Update(tempPLayerSpeed.X, tempPLayerSpeed.Y); //the numbers are temp for speed testing and will be using the player.speed later
            }

        }
        public void movementCalls()
        {
            
        }
    }
}