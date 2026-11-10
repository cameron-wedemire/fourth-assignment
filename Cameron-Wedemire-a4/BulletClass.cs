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

    public void Setup(float spawnLocationX, float spawnLocationY, Vector2 spawnDirection, int spawnSpeed)
    {
        this.position.X = spawnLocationX;
        this.position.Y = spawnLocationY;
        this.bulletRotation = 0;
        this.bulletSpeed = Vector2.Normalize(spawnDirection) * spawnSpeed;
        Console.WriteLine(this.bulletSpeed);
    }

    public void Update(float speedX, float speedY)
    {
        this.bulletRotation += 5;
        this.position.X += this.bulletSpeed.X * speedX;
        this.position.Y += this.bulletSpeed.Y * speedY;

        //bullet draw //need to fix diagonal movement
        Graphics.Scale = 2;
        Graphics.Rotation = this.bulletRotation;
        //Graphics.DrawSubset
        Graphics.DrawSubset(playerAndMunitionGraphics, this.position, new Vector2(64, 64), new Vector2(64, 64), new Vector2(32f, 32f));
    }
}