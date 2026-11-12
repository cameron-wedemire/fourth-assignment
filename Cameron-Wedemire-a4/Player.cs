using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Channels;

namespace MohawkGame2D;

public class Player
{
    //directories, can be in here or in main
    Texture2D playerAndMunitionGraphics = Graphics.LoadTexture("../../../../assets/graphics/64x64 sem1 E120 a4 sprite sheet -p1 player and munitions.png");

    // Variables local to this class
    //might need a variable later to state player xy distance from origin so its easier to calculate enemy stuff but that will be a later issue
    public Vector2 playerSpeed;
    public float playerRotation;
    public float playerTurretRotation;

    //constants
    int playerMaxSpeed = 15;

    public void Setup()
    {
        this.playerSpeed = new Vector2(0,0);
        //this.playerRotation = -90;
        this.playerRotation = 0;
        this.playerTurretRotation = 0;
    }

    public void Update()
    {
        //change player speed, after the if statments it will call the cap function that will lower the speed if its too high
        if (Input.IsKeyboardKeyDown(KeyboardKey.W))
        {
            this.playerSpeed.Y -= 1;
            //Graphics.Rotation = 0;
            
            //change player Rotation
            if (this.playerRotation > 0)
            {
                this.playerRotation -= 45;
            }
            else if (this.playerRotation < 0)
            {
                this.playerRotation += 45;
            }
        }
        else if (Input.IsKeyboardKeyDown(KeyboardKey.S))
        {
            //Graphics.Rotation = 180;
            this.playerSpeed.Y += 1;

            //change player Rotation
            if (this.playerRotation < 0 && this.playerRotation > -180)
            {
                this.playerRotation -= 45;
            }
            else if (this.playerRotation >= 0 && this.playerRotation < 180)
            {
                this.playerRotation += 45;
            }
        }

        if (Input.IsKeyboardKeyDown(KeyboardKey.A))
        {
            //Graphics.Rotation = -90;
            this.playerSpeed.X -= 1;

            //change player Rotation
            if (this.playerRotation > -90)
            {
                this.playerRotation -= 45;
            }
            else if (this.playerRotation < -90)
            {
                this.playerRotation += 45;
            }
        }
        else if (Input.IsKeyboardKeyDown(KeyboardKey.D)) //this one is not working right
        {
            //Graphics.Rotation = 90;
            this.playerSpeed.X += 1;

            ////change player Rotation
            //if (this.playerRotation > -90 && this.playerRotation > 90)
            //{
            //    this.playerRotation -= 45;
            //}
            //else if (this.playerRotation >= -90 && this.playerRotation < 90)
            //{
            //    this.playerRotation += 45;
            //}

            //change player Rotation
            if (this.playerRotation < 90)
            {
                this.playerRotation += 45;
            }
            else if (this.playerRotation > 90)
            {
                this.playerRotation -= 45;
            }
        }

        CapPlayerSpeed();

        //player draw //need to fix diagonal movement
        Graphics.Scale = 2;
        Graphics.Rotation = this.playerRotation;
        //Graphics.DrawSubset
        Graphics.DrawSubset(playerAndMunitionGraphics, new Vector2(300, 300), new Vector2(0, 0), new Vector2(64,64), new Vector2(32f, 32f)); //graphic starts from top left of the file.. thing to draw, location, fileLocation, file size, offset

        //draw turret (need to add rot to curser)
        //this.playerTurretRotation = MathF.Atan2(Input.GetMouseY(), Input.GetMouseX()) * 360; //(180 / MathF.PI);

        if (Input.IsKeyboardKeyDown(KeyboardKey.E))
        {
            this.playerTurretRotation += 5;
        }
        if (Input.IsKeyboardKeyDown(KeyboardKey.Q))
        {
            this.playerTurretRotation -= 5;
        }

        //reset angles of player to 0 if above 360
        
        if (Math.Abs(this.playerRotation) >= 360)
        {
            this.playerRotation = 0;
        }
        //reset angles of players turret to 0 if above 360
        if (Math.Abs(this.playerTurretRotation) >= 360)
        {
            this.playerTurretRotation = 0;
        }

        //draws players turret
        Graphics.Rotation = this.playerTurretRotation;
        Graphics.DrawSubset(playerAndMunitionGraphics, new Vector2(300, 300), new Vector2(64, 0), new Vector2(64, 64), new Vector2(32f, 32f));
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