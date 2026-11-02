using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D;

public class backgroundStar
{
    // Variables local to this class
    Vector2 position;
    float starSize;

    public void Setup()
    {
        position.X = Random.Integer(5, 595);
        position.Y = Random.Integer(5, 595);
        starSize = Random.Float(2, 7);
    }

    public void Update(float speedX , float speedY) //the numbers are the player speed called in from game.cs
    {
        //finds new value when size=0
        if (starSize > 0)
        {
            starSize -= 0.1f;
            //to give impression of player moving the stars in the background will move with the oposite of the player speed
            position.X -= speedX;
            position.Y -= speedY;
        }
        else
        {
            position.X = Random.Float((speedX*5) + 5, (speedX * 5) + 595);
            position.Y = Random.Float((speedY * 5) + 5, (speedY * 5) + 595);
            starSize = Random.Float(2, 5);
        }

        //draws the star
        Draw.SetLineColor(255, 255, 255);
        Draw.Line(position.X - starSize / 2, position.Y - starSize / 2, position.X + starSize / 2, position.Y + starSize / 2);//top left to bottom right
        Draw.Line(position.X, position.Y - starSize, position.X, position.Y + starSize);//top to bottom
        Draw.Line(position.X - starSize / 2, position.Y + starSize / 2, position.X + starSize / 2, position.Y - starSize / 2);//top right to bottom right
    }

}