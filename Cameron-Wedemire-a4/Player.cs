using System;
using System.Numerics;

namespace MohawkGame2D;

public class Player
{
    //directories, can be in here or in main
    Texture2D playerAndMunitionGraphics = Graphics.LoadTexture("../../../../assets/graphics/64x64 sem1 E120 a4 sprite sheet -p1 player and munitions.png");

    // Variables
    public Vector2 playerSpeed;
    float playerRotation;
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
            //up/down first then left/right and has nested if statements for the 45 degree rotation so its a bit odd
            if (Input.IsKeyboardKeyDown(KeyboardKey.W))
            {
                this.playerSpeed.Y -= 1;
                
                if (Input.IsKeyboardKeyDown(KeyboardKey.A))
                {
                    this.playerSpeed.X -= 1;
                    this.playerRotation = -45;
                }
                else if (Input.IsKeyboardKeyDown(KeyboardKey.D))
                {
                    this.playerSpeed.X += 1;
                    this.playerRotation = 45;
                }
                else
                {
                    this.playerRotation = 0;
                }
            }
            else if (Input.IsKeyboardKeyDown(KeyboardKey.S))
            {
                this.playerSpeed.Y += 1;

                if (Input.IsKeyboardKeyDown(KeyboardKey.A))
                {
                    this.playerSpeed.X -= 1;
                    this.playerRotation = -135;
                }
                else if (Input.IsKeyboardKeyDown(KeyboardKey.D))
                {
                    this.playerSpeed.X += 1;
                    this.playerRotation = 135;
                }
                else
                {
                    this.playerRotation = 180;
                }
            }
            //if w/s is not pressed but a/d is
            else if (Input.IsKeyboardKeyDown(KeyboardKey.A))
            {
                this.playerSpeed.X -= 1;
                this.playerRotation = -90;
            }
            else if (Input.IsKeyboardKeyDown(KeyboardKey.D)) //this one is not working right
            {
                this.playerSpeed.X += 1;
                this.playerRotation = 90;
            }

            CapPlayerSpeed();
        }

        //deside how to draw the player (as in if they are being hit or if they are dead they will be inverted color)
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
            Text.Color = Color.Black;
            Text.Draw("HP:", 10, Window.Height - 35);
            
            Text.Draw($"{this.playerHP}", 60, Window.Height - 35);
        }
        else
        {
            //draw game over screen if you die
            Text.Color = Color.Red;
            Text.Draw("GAME OVER \n \n \n \n YOU LOSE", 225, 220);

            //draw hp box but saying EJECT
            Draw.SetFillColor(250, 5, 0);
            Draw.SetLineColor(50, 100, 0);
            Draw.SetLineSize(3);

            Text.Color = Color.Black;
            Draw.Rectangle(0, Window.Height - 50, 100, 50);
            Text.Draw("EJECT", 9, Window.Height - 37);
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