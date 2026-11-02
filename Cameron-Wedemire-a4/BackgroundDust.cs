using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D;

public class backgroundDust
{
    // Variables local to this class
    Vector2 position;
    float dustSize;
    float distanceToPlayer;

    public void Setup()
    {
        position.X = Random.Integer(5, 595);
        position.Y = Random.Integer(5, 595);
        dustSize = Random.Float(2, 7);
        this.distanceToPlayer = 0;
    }

    public void Update(float speedX , float speedY) //the numbers are the player speed called in from game.cs
    {
        //finds new value when size=0
        if (dustSize > 0)
        {
            //calculate distance to player
            this.distanceToPlayer = Math.Abs(300 - position.X) + Math.Abs(300 - position.Y);
            //Console.WriteLine(this.distanceToPlayer);

            dustSize -= 0.1f;
            //to give impression of player moving the stars in the background will move with the oposite of the player speed.. maybe make those closer to the player move faster than further out ones?
            position.X -= speedX * (this.distanceToPlayer / 300);
            position.Y -= speedY * (this.distanceToPlayer / 300);
        }
        else
        {
            position.X = Random.Float((speedX*5) + 5, (speedX * 5) + 595);
            position.Y = Random.Float((speedY * 5) + 5, (speedY * 5) + 595);
            dustSize = Random.Float(2, 5);
        }

        //draws the star
        Draw.SetLineColor(255, 255, 255);
        Draw.Line(position.X - dustSize / 2, position.Y - dustSize / 2, position.X + dustSize / 2, position.Y + dustSize / 2);//top left to bottom right
        Draw.Line(position.X, position.Y - dustSize, position.X, position.Y + dustSize);//top to bottom
        Draw.Line(position.X - dustSize / 2, position.Y + dustSize / 2, position.X + dustSize / 2, position.Y - dustSize / 2);//top right to bottom right
    }

}