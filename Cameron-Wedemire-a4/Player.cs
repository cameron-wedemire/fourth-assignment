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
    public float playerSpeedX;
    public float playerSpeedY;

    public void Setup()
    {
        this.playerSpeedX = 0;
        this.playerSpeedY = 0;
    }

    public void Update()
    {


        //draws the star //find how to rotate later, this is temp player
        Draw.SetLineColor(255, 50, 50);
        Draw.SetFillColor(50, 50, 50);
        Draw.Rectangle(296, 290, 14, 20);
    }

}