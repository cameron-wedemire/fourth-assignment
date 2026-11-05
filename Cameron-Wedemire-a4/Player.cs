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
    public Vector2 playerSpeed;
    //public float playerRotation;

    //constants
    int playerMaxSpeed = 15;

    public void Setup()
    {
        this.playerSpeed = new Vector2(0,0);
        //this.playerRotation = -90;
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