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
    public int levelNumber;

    //if level changed this update it will state it for the next update
    bool updateThisRoundTrueFalse;

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
    int missileTurretMaxHp = 25;
    int gunTurretMaxHp = 15;
    int maxLevel = 3; //i think its 3, might be diff so fix later

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

        this.levelNumber = 0;
    }

    public void Update(float speedX, float speedY)
    {
        //move the platform oposite of player speed to give appearence of player moving
        this.position.X -= speedX;
        this.position.Y -= speedY;

        //level 1 visual
        if (levelNumber == 1)
        {
            //set turrets hp to max on level change
            if (updateThisRoundTrueFalse == true)
            {
                this.gun1Hp = gunTurretMaxHp;
                this.gun2Hp = gunTurretMaxHp;
            }

            platformDraw(-10, 0, 1, 3, -45, this.gun1Hp); //gun 1
            platformDraw(10, 0, 1, 3, 135, this.gun2Hp); //gun 2
        }


        //level 2 visual
        if (levelNumber == 2)
        {
            //set turrets hp to max on level change
            if (updateThisRoundTrueFalse == true)
            {
                this.missileTurretHp = missileTurretMaxHp;
                
            }

            platformDraw(0, 0, 0, 3, 0, this.missileTurretHp);
        }


        //level 3 visual
        if (levelNumber == 3)
        {
            //set turrets hp to max on level change
            if (updateThisRoundTrueFalse == true)
            {
                this.missileTurretHp = missileTurretMaxHp;
                this.gun1Hp = gunTurretMaxHp; //gun 1
                this.gun2Hp = gunTurretMaxHp; //gun 2
                this.gun3Hp = gunTurretMaxHp; //gun 3
                this.gun4Hp = gunTurretMaxHp; //gun 4
            }

            //draw gun platforms
            platformDraw(-17, -17, 1, 3, 0, this.gun1Hp);
            platformDraw(17, -17, 1, 3, 90, this.gun2Hp);
            platformDraw(17, 17, 1, 3, 180, this.gun3Hp);
            platformDraw(-17, 17, 1, 3, 270, this.gun4Hp);
            
            //draw missile platform
            platformDraw(0, 0, 0, 3, 0, this.missileTurretHp);
        }


        //debug level changer
        if (Input.IsKeyboardKeyDown(KeyboardKey.H))
        {
            this.missileTurretHp = 0;
            this.gun1Hp = 0; //gun 1
            this.gun2Hp = 0; //gun 2
            this.gun3Hp = 0; //gun 3
            this.gun4Hp = 0; //gun 4
        }

        //change level number if all turrets are at 0 HP and change updateThisRoundTrueFalse = true, otherwise set updateThisRoundTrueFalse = false so it doesnt constantly give turrets hp
        if ((this.missileTurretHp <= 0) && (this.gun1Hp <= 0) && (this.gun2Hp <= 0) && (this.gun3Hp <= 0) && (this.gun4Hp <= 0) && (levelNumber < maxLevel))
        {
            levelNumber += 1;
            updateThisRoundTrueFalse = true;
            Console.WriteLine("{0} {1}", "level # =", levelNumber);
        }
        else
        {
            updateThisRoundTrueFalse = false;
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

    public void platformDraw(float offsetCenterX, float offsetCenterY, int spriteLocationY, float platformScale, float platformRotation, int platformHP)
    {
        //need to make them decide if they are dead or not (hp <= 0) later

        //draw the platform
        Graphics.Scale = platformScale;
        Graphics.Rotation = platformRotation;
        if (platformHP > 0)
        {
            Graphics.DrawSubset(platformGraphics, this.position + new Vector2(offsetCenterX * platformScale, offsetCenterY * platformScale), new Vector2(0, spriteLocationY * 64), new Vector2(64, 64), new Vector2(32, 32));
        }
        else
        {
            Graphics.DrawSubset(platformGraphics, this.position + new Vector2(offsetCenterX * platformScale, offsetCenterY * platformScale), new Vector2(64, spriteLocationY * 64), new Vector2(64, 64), new Vector2(32, 32));
        }

        //draw turret ontop of the platform
        if (platformHP > 0)
        {
            turretAngleFinder(offsetCenterX * platformScale, offsetCenterY * platformScale);
            Graphics.DrawSubset(platformGraphics, this.position + new Vector2(offsetCenterX * platformScale, offsetCenterY * platformScale), new Vector2(128, spriteLocationY * 64), new Vector2(64, 64), new Vector2(32, 32));
        }
    }
}

//will need to increase levelNumber by 1 everytime all platform HP is at 0, on 1 past max level it will show a thing saying gameover you win