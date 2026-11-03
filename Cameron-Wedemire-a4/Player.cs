using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D;

public class Player
{
    // Variables local to this class
    //might need a variable later to state player xy distance from origin so its easier to calculate enemy stuff but that will be a later issue
    //public float playerSpeedX;
    //public float playerSpeedY;
    public Vector2 playerSpeed;
    public float playerRotation;

    public void Setup()
    {
        //this.playerSpeedX = 0;
        //this.playerSpeedY = 0;
        this.playerSpeed = new Vector2(0,0);
        this.playerRotation = -90; //im not sure how i did it but i managed to get the player rotation to work but it needs 90 degree offset
    }

    public void Update()
    {
        ////temp player movement, will change to go based on rotation later but that depends on if i get rotation taught to me, if not then ill do it based on mouse location
        //if (Input.IsKeyboardKeyDown(KeyboardKey.W))
        //{
        //    //cap player speed
        //    if (this.playerSpeedY > -15)
        //    {
        //        this.playerSpeedY -= 1;
        //    }
        //}
        //else if (Input.IsKeyboardKeyDown(KeyboardKey.S))
        //{
        //    //cap player speed
        //    if (this.playerSpeedY < 15)
        //    {
        //        this.playerSpeedY += 1;
        //    }
        //}

        //if (Input.IsKeyboardKeyDown(KeyboardKey.A))
        //{
        //    //cap player speed
        //    if (this.playerSpeedX > -15)
        //    {
        //        this.playerSpeedX -= 1;
        //    }
        //}
        //else if (Input.IsKeyboardKeyDown(KeyboardKey.D))
        //{
        //    //cap player speed
        //    if (this.playerSpeedX < 15)
        //    {
        //        this.playerSpeedX += 1;
        //    }
        //}


        //code taken from eye example, will be used for the gun targeting later but for now its here to be used to help make movement stuff
        ////find the angle of eye to mouse
        //Vector2 eyeToMouse = Input.GetMousePosition() - position;
        ////get the direction to the mouse from the eyes position
        //Vector2 direction = Vector2.Normalize(eyeToMouse);
        //float distance = eyeToMouse.Length(); //or vector2.distance(position, Input.GetMousePosition());

        //change ship angle
        //rotate player left
        if (Input.IsKeyboardKeyDown(KeyboardKey.A))
        {
            this.playerRotation -= 5;
        }
        //rotate player right
        if (Input.IsKeyboardKeyDown(KeyboardKey.D))
        {
            this.playerRotation += 5;
        }
        if (Math.Abs(this.playerRotation) > 360)
        {
            //reset roation to 0 if either direction is above 360 degrees,
            this.playerRotation = 0;
        }

        //move forward based on ship angle
        if (Input.IsKeyboardKeyDown(KeyboardKey.Space))
        {
            //calculate player X speed based on rotation of the ship
            this.playerSpeed.X += MathF.Cos(this.playerRotation * (MathF.PI / 180)) * 5;

            //calculate player Y speed based on rotation of the ship
            this.playerSpeed.Y += MathF.Sin(this.playerRotation * (MathF.PI / 180)) * 5;

            //cap the speed by doing the inverse, also didnt work
            //if (Math.Abs(this.playerSpeedX) > 15)
            //{
            //    this.playerSpeedX -= MathF.Cos(this.playerRotation * (MathF.PI / 180)) * 5;
            //}
            //if (Math.Abs(this.playerSpeedY) > 15)
            //{
            //    this.playerSpeedY -= MathF.Sin(this.playerRotation * (MathF.PI / 180)) * 5;
            //}
        }

        //cap player speed.. this isnt right...
        ////X speed
        //if (this.playerSpeedX > 15)
        //{
        //    this.playerSpeedX = 15;
        //}
        //else if (this.playerSpeedX < -15)
        //{
        //    this.playerSpeedX = -15;
        //}

        ////Y speed
        //if (this.playerSpeedY > 15)
        //{
        //    this.playerSpeedY = 15;
        //}
        //else if (this.playerSpeedY < -15)
        //{
        //    this.playerSpeedY = -15;
        //}

        if (Math.Abs(this.playerSpeed.X) > 15)
        {
            if (this.playerSpeed.X > 15)
            {
                this.playerSpeed = new Vector2(15, this.playerSpeed.Y);
            }
            if (this.playerSpeed.X < -15)
            {
                this.playerSpeed = new Vector2(-15, this.playerSpeed.Y);
            }
            Console.WriteLine("oi");
        }
        if (Math.Abs(this.playerSpeed.Y) > 15)
        {
            if (this.playerSpeed.Y > 15)
            {
                this.playerSpeed = new Vector2(this.playerSpeed.X, 15);
            }
            if (this.playerSpeed.Y < -15)
            {
                this.playerSpeed = new Vector2(this.playerSpeed.X, -15);
            }
        }
        

        //Console.WriteLine("{0}, {1}, {2}", this.playerRotation, this.playerSpeedX, this.playerSpeedY);
        //calculate playerSpeed impact by thrust based on ship angle

        //draws the star //find how to rotate later, this is temp player
        Draw.SetLineColor(255, 50, 50);
        Draw.SetFillColor(50, 50, 50);
        Draw.Rectangle(296, 290, 14, 20);

        //draw temp player rotation visualizer
        //Draw.Circle(300 + this.playerSpeedX, 300 + this.playerSpeedY, 10); //failed
    }

}