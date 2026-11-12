using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D;

public class Bullet
{
    //directories, can be in here or in main
    Texture2D playerAndMunitionGraphics = Graphics.LoadTexture("../../../../assets/graphics/64x64 sem1 E120 a4 sprite sheet -p1 player and munitions.png");
    
    // Variables local to this class
    public Vector2 position;
    public float bulletRotation;
    public Vector2 bulletSpeed;
    float bulletLifeTime;

    public void Setup(float spawnLocationX, float spawnLocationY, Vector2 targetDirection, float spawnSpeed)
    {
        this.position.X = spawnLocationX;
        this.position.Y = spawnLocationY;
        this.bulletRotation = 0;
        this.bulletSpeed = Vector2.Normalize((new Vector2 (spawnLocationX + spawnLocationY) * targetDirection)) * spawnSpeed;
        this.bulletLifeTime = 50;
        Console.WriteLine(this.bulletSpeed);
        Console.WriteLine("");
        Console.WriteLine(targetDirection);
    }

    public void Update(float speedX, float speedY)
    {
        //movement not working
        this.bulletRotation += 5;
        this.position.X -= speedX + this.bulletSpeed.X;
        this.position.Y -= speedY + this.bulletSpeed.Y;

        bulletLifeTime -= 0.2f;
        if (this.bulletLifeTime <= 0)
        {
            this.position = new Vector2(-50, -50);
            this.bulletSpeed = new Vector2(0, 0);
        }


        //bullet draw //need to fix diagonal movement
        Graphics.Scale = 2;
        Graphics.Rotation = this.bulletRotation;
        //Graphics.DrawSubset
        Graphics.DrawSubset(playerAndMunitionGraphics, this.position, new Vector2(64, 64), new Vector2(64, 64), new Vector2(32f, 32f));
    }
}