using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D;

public class Missile
{
    //directories
    Texture2D playerAndMunitionGraphics = Graphics.LoadTexture("../../../../assets/graphics/64x64 sem1 E120 a4 sprite sheet -p1 player and munitions.png");

    // Variables local to this class
    public Vector2 position;
    float fuel;
    public float distanceToPlayer;
    public float missileRotation;
    public Vector2 missileSpeed;
    Vector2 RotToPlayerVector;
    float RotToPlayerFloat;
    Vector2 playerLocationFalse = new Vector2(300, 300); //just for calculations that need Vector2s

    public void Setup(float spawnLocationX, float spawnLocationY, float spawnRotation) //the numbers are the location of the thing that spawned them in
    {
        this.position.X = spawnLocationX;
        this.position.Y = spawnLocationY;
        this.fuel = 50;
        this.distanceToPlayer = (300 - this.position.X) + (300 - this.position.Y);
        this.missileRotation = spawnRotation;
        missileSpeed = new Vector2(0,0);
    }

    public void Update(float speedX, float speedY) //the numbers are the player speed called in from game.cs
    {
        //moves the missile when it has fuel
        if (fuel > -20)
        {
            //calculate distance to player
            this.distanceToPlayer = Math.Abs(playerLocationFalse.X - this.position.X) + Math.Abs(playerLocationFalse.Y - this.position.Y);

            fuel -= 0.1f;

            //if fuel is above 0 allow thrust to increase and missile rotation to change
            if (fuel > 0)
            {
                //missile rotation towards player (would have needed a statement of target location but player doesnt have missiles so only need player location which is always (300, 300))
                RotToPlayerVector = Vector2.Normalize(playerLocationFalse - this.position);
                RotToPlayerFloat = MathF.Atan2(RotToPlayerVector.Y, RotToPlayerVector.X) * (180 / MathF.PI);

                if ((RotToPlayerFloat) < (this.missileRotation))
                {
                    this.missileRotation -= 5;
                }
                if ((RotToPlayerFloat) > (this.missileRotation))
                {
                    this.missileRotation += 5;
                }
                //reset missile rotation to 0 if over 360 as there is no need to go over 360
                if (Math.Abs(missileRotation) > 360)
                {
                    this.missileRotation = 0;
                }

                //moves towards player if angle is less than 15 degrees off forwards direction of missile
                if (MathF.Abs(RotToPlayerFloat - this.missileRotation) < 15)
                {
                    missileSpeed.X += (this.position.X - 300) / 1000;
                    missileSpeed.Y += (this.position.Y - 300) / 1000;

                    //cap speed so it doesnt go too fast as that can cause collision detection issues
                    if (missileSpeed.X != 0 || missileSpeed.Y != 0)
                    {
                        Vector2 missileMoveAngle = Vector2.Normalize(missileSpeed);
                        if (this.missileSpeed.Length() > 25)
                        {
                            this.missileSpeed = missileMoveAngle * 25;
                            //Console.WriteLine(missileSpeed.Length());
                        }
                    }
                }
            }

            //to give impression of player moving the missile will move with the oposite of the player speed
            this.position.X -= speedX + missileSpeed.X;
            this.position.Y -= speedY + missileSpeed.Y;
        }
        else
        {
            this.position.X = -50;
            this.position.Y = -50;
        }

        //draws the missile (need a fake thruster power to show when missile has fuel and is trying to move forwards)
        if (this.position != new Vector2(-50, -50))
        {
            Graphics.Scale = 2;
            Graphics.Rotation = this.missileRotation + 90;
            Graphics.DrawSubset(playerAndMunitionGraphics, this.position, new Vector2(0, 64), new Vector2(64, 64), new Vector2(32f, 32f));
        }
            
    }

}


//toDo:
//make missiles only spawn when player is within _ range
//make missiles continue floating but not increasing speed when out of fuel and then explode when they reach -__ fuel
//make missiles rotate towards player based on rotationToPlayer-180 or something



//code taken from eye example, will be used for the gun targeting later but for now its here to be used to help make movement stuff
////find the angle of eye to mouse
//Vector2 eyeToMouse = Input.GetMousePosition() - position;
////get the direction to the mouse from the eyes position
//Vector2 direction = Vector2.Normalize(eyeToMouse);
//float distance = eyeToMouse.Length(); //or vector2.distance(position, Input.GetMousePosition());