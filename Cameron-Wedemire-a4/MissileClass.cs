using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D;

public class Missile
{
    // Variables local to this class
    public Vector2 position;
    float fuel;
    public float distanceToPlayer;
    public float missileRotation;
    public Vector2 missileSpeed;
    //Vector2 RotToPlayer;
    float RotToPlayer;
    Vector2 playerLocationFalse = new Vector2(300, 300); //just for calculations that need Vector2s

    public void Setup(float spawnLocationX, float spawnLocationY, float spawnRotation) //the numbers are the location of the thing that spawned them in
    {
        this.position.X = spawnLocationX;
        this.position.Y = spawnLocationY;
        this.fuel = 50;
        this.distanceToPlayer = (300 - this.position.X) + (300 - this.position.Y);
        this.missileRotation = spawnRotation + 90; //for some reason it needs a +90 to fly forwards instead of sideways
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
                //calculate missile rotation change based on angle to player
                //RotToPlayer = Vector2.Normalize(playerLocationFalse - this.position);
                RotToPlayer = MathF.Atan2((this.position.Y - playerLocationFalse.Y), (this.position.X - playerLocationFalse.X)) * 90 - 90; //y2 - y1 / x2 - x1 //worked but wasnt leading to the player so testing others
                //RotToPlayer = (float)Math //going to try normalization or something

                //Console.WriteLine(RotToPlayer);

                //temp instead of gradual rot change
                //missileRotation = RotToPlayer.X * 90;

                //missile rotation change based on if RotToPlayer is positive or negative
                if ((missileRotation + RotToPlayer) > 0) //its not propperly checking the rot requirment
                {
                    missileRotation += 5;
                    Console.WriteLine("rot1");
                }
                else if ((missileRotation - RotToPlayer) < 0)
                {
                    missileRotation -= 5;
                    Console.WriteLine("rot2");
                }

                if (Math.Abs(missileRotation) > 360)
                {
                    missileRotation = 0;
                }

                Console.WriteLine("{0}, {1}", RotToPlayer, missileRotation);
                //calculate missile X speed based on rotation of the missile
                this.missileSpeed.X += MathF.Cos(this.missileRotation * (MathF.PI / 180)) * 0.1f; //should be 0.5f
                //calculate missile Y speed based on rotation of the missile
                this.missileSpeed.Y += MathF.Sin(this.missileRotation * (MathF.PI / 180)) * 0.1f; //should be 0.5f
            }

            //to give impression of player moving the stars in the background will move with the oposite of the player speed.. maybe make those closer to the player move faster than further out ones?
            this.position.X -= speedX + missileSpeed.X;
            this.position.Y -= speedY + missileSpeed.Y;
        }
        else
        {
            this.position.X = -50;
            this.position.Y = -50;
        }

        //draws the missile (temp shape, will make better once i learn how to rotate).. also need a fake thruster power to show when missile has fuel and is trying to move forwards
        if (this.position != new Vector2(-50, -50))
        {
            Draw.SetLineColor(255, 0, 0);
            Draw.SetFillColor(0, 200, 0);
            Draw.Circle(position.X, position.Y, 5);
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