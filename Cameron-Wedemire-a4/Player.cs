using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Channels;

namespace MohawkGame2D;

public class Player
{
    // Variables local to this class
    //might need a variable later to state player xy distance from origin so its easier to calculate enemy stuff but that will be a later issue
    //public float playerSpeedX;
    //public float playerSpeedY;
    public Vector2 playerSpeed;
    //public Vector2 PlayerSpeedRatio; //do normalization
    //public float playerRotation;

    //constants
    int playerMaxSpeed = 15;

    public void Setup()
    {
        //this.playerSpeedX = 0;
        //this.playerSpeedY = 0;
        this.playerSpeed = new Vector2(0,0);
        //this.PlayerSpeedRatio = new Vector2(0.5f, 0.5f);
        //this.playerRotation = -90; //im not sure how i did it but i managed to get the player rotation to work but it needs 90 degree offset
    }

    public void Update()
    {
        //change player speed, after the if statments it will call the cap function that will lower the speed if its too high
        if (Input.IsKeyboardKeyDown(KeyboardKey.W))
        {
            this.playerSpeed.Y -= 1;
        }
        else if (Input.IsKeyboardKeyDown(KeyboardKey.S))
        {
            this.playerSpeed.Y += 1;
        }

        if (Input.IsKeyboardKeyDown(KeyboardKey.A))
        {
            this.playerSpeed.X -= 1;
        }
        else if (Input.IsKeyboardKeyDown(KeyboardKey.D))
        {
            this.playerSpeed.X += 1;
        }
        CapPlayerSpeed();

        //draws the player //find how to rotate later, this is temp player
        Draw.SetLineColor(255, 50, 50);
        Draw.SetFillColor(50, 50, 50);
        Draw.Rectangle(296, 290, 14, 20);

        //will need to rotate the player based on what direction the player is trying to move in later
    }
    public void CapPlayerSpeed() //PlayerSpeedRatio
    {
        if (playerSpeed.X != 0 || playerSpeed.Y != 0)
        {
            Vector2 playerMoveAngle = Vector2.Normalize(playerSpeed);
            if (this.playerSpeed.Length() > 15)
            {
                this.playerSpeed = playerMoveAngle * this.playerMaxSpeed;
                //Console.WriteLine(this.playerSpeed);
            }
        }
    }
}


//failed code:

///change ship angle
////rotate player left
//if (Input.IsKeyboardKeyDown(KeyboardKey.A))
//{
//   this.playerRotation -= 5;
//}
////rotate player right
//if (Input.IsKeyboardKeyDown(KeyboardKey.D))
//{
//    this.playerRotation += 5;
//}
//if (Math.Abs(this.playerRotation) > 360)
//{
//    //reset roation to 0 if either direction is above 360 degrees,
//    this.playerRotation = 0;
//}

////move forward based on ship angle
//if (Input.IsKeyboardKeyDown(KeyboardKey.W))
//{
//    //calculate player X speed based on rotation of the ship
//    this.playerSpeed.X += MathF.Cos(this.playerRotation* (MathF.PI / 180)) * 5;

//    //calculate player Y speed based on rotation of the ship
//    this.playerSpeed.Y += MathF.Sin(this.playerRotation * (MathF.PI / 180)) * 5;

////cap the speed by doing the inverse, also didnt work
////if (Math.Abs(this.playerSpeedX) > 15)
////{
////    this.playerSpeedX -= MathF.Cos(this.playerRotation * (MathF.PI / 180)) * 5;
////}
////if (Math.Abs(this.playerSpeedY) > 15)
////{
////    this.playerSpeedY -= MathF.Sin(this.playerRotation * (MathF.PI / 180)) * 5;
////}
//}

//if (Math.Abs(this.playerSpeed.X) > 15)
//{
//    if (this.playerSpeed.X > 15)
//    {
//        this.playerSpeed = new Vector2(15, this.playerSpeed.Y);
//    }
//    if (this.playerSpeed.X < -15)
//    {
//        this.playerSpeed = new Vector2(-15, this.playerSpeed.Y);
//    }
//}
//if (Math.Abs(this.playerSpeed.Y) > 15)
//{
//    if (this.playerSpeed.Y > 15)
//    {
//        this.playerSpeed = new Vector2(this.playerSpeed.X, 15);
//    }
//    if (this.playerSpeed.Y < -15)
//    {
//        this.playerSpeed = new Vector2(this.playerSpeed.X, -15);
//    }
//}