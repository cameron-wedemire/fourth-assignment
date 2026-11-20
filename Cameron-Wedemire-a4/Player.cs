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

    // Variables
    public Vector2 playerSpeed;
    public float playerRotation;
    public float playerTurretRotation;
    Vector2 RotToCursorVector = new Vector2(0, 0);
    Vector2 playerSpriteType;

    //constants
    int playerMaxSpeed = 15;
    public int playerHP;
    public bool hasBeenHitThisTurn;

    public void Setup()
    {
        this.playerSpeed = new Vector2(0,0);
        this.playerRotation = 0;
        this.playerTurretRotation = 0;

        this.playerHP = 50;//balence later
        this.hasBeenHitThisTurn = false;
    }

    public void Update()
    {
        //change player speed, after the if statments it will call the cap function that will lower the speed if its too high
        if (this.playerHP > 0)
        {
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
        }

        //player draw
        Graphics.Scale = 2;
        Graphics.Rotation = this.playerRotation;

        if (this.hasBeenHitThisTurn == false)
        {
            playerSpriteType = new Vector2(0, 0);
        }
        else
        {
            playerSpriteType = new Vector2(64, 128);
            if (this.playerHP > 0)
            {
                this.hasBeenHitThisTurn = false;
            }
            
        }

        Graphics.DrawSubset(playerAndMunitionGraphics, new Vector2(300, 300), playerSpriteType, new Vector2(64,64), new Vector2(32, 32));

        //draws players turret BUT ONLY IF THE PLAYER IS ALIVE
        if (this.playerHP > 0)
        {
            RotToCursorVector = Vector2.Normalize((Input.GetMousePosition()) - new Vector2(300, 300));
            this.playerTurretRotation = MathF.Atan2(RotToCursorVector.Y, RotToCursorVector.X) * (180 / MathF.PI) + 90;

            Graphics.Rotation = this.playerTurretRotation;
            Graphics.DrawSubset(playerAndMunitionGraphics, new Vector2(300, 300), new Vector2(64, 0), new Vector2(64, 64), new Vector2(32, 32));

            //draws player hp at the bottom left of the screen
            Draw.SetFillColor(250, 5, 0);
            Draw.SetLineColor(50, 100, 0);
            Draw.SetLineSize(3);

            Draw.Rectangle(0, Window.Height - 50, 100, 50);
            Text.Draw("HP:", 10, Window.Height - 35);
            
            Text.Draw($"{this.playerHP}", 60, Window.Height - 35);
        }
    }
    public void CapPlayerSpeed()
    {
        if (playerSpeed.X != 0 || playerSpeed.Y != 0)
        {
            Vector2 playerMoveAngle = Vector2.Normalize(playerSpeed);
            if (this.playerSpeed.Length() > 15)
            {
                this.playerSpeed = playerMoveAngle * this.playerMaxSpeed;
            }
        }
    }
}