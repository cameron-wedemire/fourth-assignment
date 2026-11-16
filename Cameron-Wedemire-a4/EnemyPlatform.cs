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
    public int missileTurretHp;
    public int gun1Hp;
    public int gun2Hp;
    public int gun3Hp;
    public int gun4Hp;

    //will fine toon later this is temp
    int missileTurretMaxHp = 100;
    int gunTurretMaxHp = 100;

    public void Setup(float spawnLocationX, float spawnLocationY)
    {
        //the reason for spawnFuel is so when starting the game it doesnt spawn all 5 at once, 50 fuel seems to be a good amount
        this.position.X = spawnLocationX;
        this.position.Y = spawnLocationY;
        this.distanceToPlayer = (300 - this.position.X) + (300 - this.position.Y);

        //this.is alive code to let weapon fire functions work
        this.missileTurretHp = 0;
        this.gun1Hp = 0;
        this.gun2Hp = 0;
        this.gun3Hp = 0;
        this.gun4Hp = 0;
    }

    public void Update(float speedX, float speedY, bool updateThisRoundTrueFalse)
    {
        //move the platform oposite of player speed to give appearence of player moving
        this.position.X -= speedX;
        this.position.Y -= speedY;

        //level 1 visual
        if (levelNumber == 1)
        {
            if (updateThisRoundTrueFalse == true)
            {
                this.gun1Hp = 100;
            }
            platformDraw(-10, 0, 0, 1, 3, -45);
            platformDraw(10, 0, 0, 1, 3, 135);
        }


        //level 2 visual
        if (levelNumber == 2)
        {
            platformDraw(0, 0, 0, 0, 3, 0);
        }


        //level 3 visual
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
        
        //temp level change for testing, later will be if all platform turret hp = 0 and level is less than game over number
        if (Input.IsKeyboardKeyPressed(KeyboardKey.H))
        {
            levelNumber += 1;
        }
    }

    public void turretAngleFinder(float offsetCenterX, float offsetCenterY) //later make it so that when its not able to aim at the enemy it goes to an idle angle
    {
        //tells the turrets the rotation needed to aim at the player
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
        Graphics.DrawSubset(platformGraphics, this.position + new Vector2(offsetCenterX * platformScale, offsetCenterY * platformScale), new Vector2(spriteLocationX * 64, spriteLocationY * 64), new Vector2(64, 64), new Vector2(32, 32));

        //draw turret ontop of the platform
        turretAngleFinder(offsetCenterX * platformScale, offsetCenterY * platformScale);
        Graphics.DrawSubset(platformGraphics, this.position + new Vector2(offsetCenterX * platformScale, offsetCenterY * platformScale), new Vector2(128, spriteLocationY * 64), new Vector2(64, 64), new Vector2(32, 32));
    }
}

//will need to increase levelNumber by 1 everytime all platform HP is at 0, on 1 past max level it will show a thing saying gameover you win