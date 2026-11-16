using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MohawkGame2D;

public class Platform
{
    //directories
    Texture2D platformGraphics = Graphics.LoadTexture("../../../../assets/graphics/64x64 sem1 E120 a4 sprite sheet -p2 enemy sprites.png");

    //variables
    //platformType 1=gun, 2=missile
    int platformType;
    int levelNumber;

    public Vector2 position;
    public float distanceToPlayer;
    Vector2 RotToPlayerVector;
    float RotToPlayerFloat;
    Vector2 playerLocationFalse = new Vector2(300, 300); //just for calculations that need Vector2s

    public void Setup(float spawnLocationX, float spawnLocationY)
    {
        //the reason for spawnFuel is so when starting the game it doesnt spawn all 5 at once, 50 fuel seems to be a good amount
        this.position.X = spawnLocationX;
        this.position.Y = spawnLocationY;
        this.distanceToPlayer = (300 - this.position.X) + (300 - this.position.Y);
    }

    public void Update(float speedX, float speedY)
    {
        //move the platform oposite of player speed to give appearence of player moving
        this.position.X -= speedX;
        this.position.Y -= speedY;

        //temp level 1 visual
        if (levelNumber == 1)
        {
            platformDraw(-10, 0, 0, 1, 3, -45);
            platformDraw(10, 0, 0, 1, 3, 135);
        }


        //temp level 2 visual
        if (levelNumber == 2)
        {
            platformDraw(0, 0, 0, 0, 3, 0);
        }


        //temp level 3 visual
        if (levelNumber == 3)
        {
            //draw gun platforms
            platformDraw(-17, -17, 0, 1, 3, 0);
            platformDraw(17, -17, 0, 1, 3, 90);
            platformDraw(17, 17, 0, 1, 3, 180);
            platformDraw(-17, 17, 0, 1, 3, 270);
            
            //draw missile platform
            platformDraw(0, 0, 0, 0, 3, 0);
        }
        
        //temp level change for testing
        if (Input.IsKeyboardKeyPressed(KeyboardKey.H))
        {
            levelNumber += 1;
        }
    }

    public void turretAngleFinder(float offsetCenterX, float offsetCenterY) //later make it so that when its not able to aim at the enemy it goes to an idle angle
    {
        //needed for the turrets to know what is in their available field of fire
        this.RotToPlayerVector = Vector2.Normalize(playerLocationFalse - (this.position + new Vector2(offsetCenterX, offsetCenterY)));
        this.RotToPlayerFloat = MathF.Atan2(RotToPlayerVector.Y, RotToPlayerVector.X) * (180 / MathF.PI) + 90;

        //update the rotation
        Graphics.Rotation = RotToPlayerFloat;
    }

    public void platformDraw(float offsetCenterX, float offsetCenterY, int spriteLocationX, int spriteLocationY, float platformScale, float platformRotation)
    {
        //draw the platform
        Graphics.Scale = platformScale;
        Graphics.Rotation = platformRotation;
        Graphics.DrawSubset(platformGraphics, this.position + new Vector2(offsetCenterX * platformScale, offsetCenterY * platformScale), new Vector2(spriteLocationX * 64, spriteLocationY * 64), new Vector2(64, 64), new Vector2(32f, 32f));

        //draw turret ontop of the platform
        turretAngleFinder(offsetCenterX * platformScale, offsetCenterY * platformScale);
        Graphics.DrawSubset(platformGraphics, this.position + new Vector2(offsetCenterX * platformScale, offsetCenterY * platformScale), new Vector2(128, spriteLocationY * 64), new Vector2(64, 64), new Vector2(32f, 32f));
    }
}

//will need to increase levelNumber by 1 everytime its destroyed, on 1 past max level it will show a thing saying gameover you win