using System;
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
    Vector2 lastPlatformCenter;
    
    Vector2 RotToPlayerVector;
    float RotToPlayerFloat;
    //just for calculations that need Vector2s
    Vector2 playerLocationFalse = new Vector2(300, 300); 
    
    float platformNotificationBlinkTimer;
    Vector2 platformNotificationBlinkLocation;

    //turret variables
    public int missileTurretHp;
    public bool missileTurretHit;
    public int gun1Hp;
    public bool gun1TurretHit;
    public int gun2Hp;
    public bool gun2TurretHit;
    public int gun3Hp;
    public bool gun3TurretHit;
    public int gun4Hp;
    public bool gun4TurretHit;
    int timeSinceHit;

    //constant reference points
    int missileTurretMaxHp = 25;
    int gunTurretMaxHp = 15;
    //desides when to show the end screen.. not implimented yet
    int maxLevel = 3; 

    public void Setup()
    {
        //find random spawn location OUTSIDE OF SCREEN
        //find random spawn X
        float tempLocation = 300;
        while ((tempLocation + 15 > 0) && (tempLocation - 15 < Window.Width))
        {
            tempLocation = Random.Float(-1500, 1500);
        }
        this.position.X = tempLocation;

        //find random spawn Y
        tempLocation = 300;
        while ((tempLocation + 15 > 0) && (tempLocation - 15 < Window.Height))
        {
            tempLocation = Random.Float(-1500, 1500);
        }
        this.position.Y = tempLocation;

        //this.isAlive code to let weapon fire functions work
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
        lastPlatformCenter.X -= speedX;
        lastPlatformCenter.Y -= speedY;

        //level 1 visual
        if (levelNumber == 1)
        {
            //set turrets hp to max on level change
            if (updateThisRoundTrueFalse == true)
            {
                this.gun1Hp = gunTurretMaxHp;
                this.gun2Hp = gunTurretMaxHp;
            }

            platformDraw(this.position, -10, 0, 1, 3, -45, this.gun1Hp, gun1TurretHit); //gun 1
            platformDraw(this.position, 10, 0, 1, 3, 135, this.gun2Hp, gun2TurretHit); //gun 2
        }


        //level 2 visual
        if (levelNumber == 2)
        {
            //set turrets hp to max on level change
            if (updateThisRoundTrueFalse == true)
            {
                this.missileTurretHp = missileTurretMaxHp;
                
            }

            platformDraw(this.position, 0, 0, 0, 3, 0, this.missileTurretHp, missileTurretHit); //missile

            //draw old platform so it doesnt dissapear on death BUT only for the next level as keepking all would cause lag if more levels are added
            platformDraw(lastPlatformCenter, - 10, 0, 1, 3, -45, 0, true); //old gun 1
            platformDraw(lastPlatformCenter, 10, 0, 1, 3, 135, 0, true); //old gun 2
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
            platformDraw(this.position , - 17, -17, 1, 3, 0, this.gun1Hp, gun1TurretHit); //gun 1
            platformDraw(this.position, 17, -17, 1, 3, 90, this.gun2Hp, gun2TurretHit);   //gun 2
            platformDraw(this.position, - 17, 17, 1, 3, 180, this.gun3Hp, gun3TurretHit); //gun 3
            platformDraw(this.position, 17, 17, 1, 3, 270, this.gun4Hp, gun4TurretHit);   //gun 4
            
            //draw missile platform
            platformDraw(this.position, 0, 0, 0, 3, 0, this.missileTurretHp, missileTurretHit); //missile

            //draw old platform so it doesnt dissapear on death BUT only for the next level as keepking all would cause lag if more levels are added
            platformDraw(lastPlatformCenter, 0, 0, 0, 3, 0, 0, true);
        }

        //end game screen
        if (levelNumber > maxLevel)
        {
            //draw old platform so it doesnt dissapear on death BUT only for the next level as keepking all would cause lag if more levels are added
            platformDraw(lastPlatformCenter, -17, -17, 1, 3, 0, 0, true); //old gun 1
            platformDraw(lastPlatformCenter, 17, -17, 1, 3, 90, 0, true); //old gun 2
            platformDraw(lastPlatformCenter, -17, 17, 1, 3, 180, 0, true);//old gun 3
            platformDraw(lastPlatformCenter, 17, 17, 1, 3, 270, 0, true); //old gun 4

            platformDraw(lastPlatformCenter, 0, 0, 0, 3, 0, 0, true); //old missile

            //end game text
            Text.Color = Color.Red;
            Text.Draw("GAME OVER \n \n \n \n YOU WIN", 225, 220);
        }

        //reset the turrets hit check so it flashes damage instead of being perma damaged
        if ((timeSinceHit < 0) && ((missileTurretHit == true) || (gun1TurretHit == true) || (gun2TurretHit == true) || (gun3TurretHit == true) || (gun4TurretHit == true)))
        {
            //this is sest to 5 as that will be decreased to 0 before the next player bullet is shot
            timeSinceHit = 5;
        }

        if (timeSinceHit > 0)
        {
            timeSinceHit -= 1;
        }
        else
        {
            missileTurretHit = false;
            gun1TurretHit = false;
            gun2TurretHit = false;
            gun3TurretHit = false;
            gun4TurretHit = false;
            timeSinceHit = -1;
        }

        //debug level changer
        if (Input.IsKeyboardKeyPressed(KeyboardKey.H))
        {
            this.missileTurretHp = 0;
            this.gun1Hp = 0; //gun 1
            this.gun2Hp = 0; //gun 2
            this.gun3Hp = 0; //gun 3
            this.gun4Hp = 0; //gun 4
        }

        //change level number if all turrets are at 0 HP and change updateThisRoundTrueFalse = true, otherwise set updateThisRoundTrueFalse = false so it doesnt constantly give turrets hp
        if ((this.missileTurretHp <= 0) && (this.gun1Hp <= 0) && (this.gun2Hp <= 0) && (this.gun3Hp <= 0) && (this.gun4Hp <= 0) && (levelNumber <= maxLevel))
        {
            levelNumber += 1;
            updateThisRoundTrueFalse = true;
            //kept this for when adding new levels, would have put on screen but I didnt want to clutter the UI too much
            Console.WriteLine("{0} {1}", "level # =", levelNumber);

            //set old location Vector2 for visual of the platform player just destroyed
            lastPlatformCenter = this.position;

            //find new location OUTSIDE OF SCREEN
            //find random spawn X
            float tempLocation = 300;
            while ((tempLocation + 15 > 0) && (tempLocation - 15 < Window.Width))
            {
                tempLocation = Random.Float(-1500, 1500);
            }
            this.position.X = tempLocation;

            //find random spawn Y
            tempLocation = 300;
            while ((tempLocation + 15 > 0) && (tempLocation - 15 < Window.Height))
            {
                tempLocation = Random.Float(-1500, 1500);
            }
            this.position.Y = tempLocation;
        }
        else
        {
            updateThisRoundTrueFalse = false;
        }

        //platformNotificationBlinker is for telling the player where the platform is incase they fly off into the void and get lost
        if ((this.position.X+15 > 0) && (this.position.X-15 < Window.Width) && (this.position.Y+15 > 0) && (this.position.Y-15 < Window.Height))
        {
            platformNotificationBlinkLocation.X = -50;
            platformNotificationBlinkLocation.Y = -50;

        }
        else
        {
            //keep notification's X on screen
            if (this.position.X > Window.Width)
            {
                platformNotificationBlinkLocation.X = Window.Width - 5;
            }
            else if (this.position.X < 0)
            {
                platformNotificationBlinkLocation.X = 5;
            }
            else
            {
                platformNotificationBlinkLocation.X = this.position.X;
            }

            //keep notification's Y on screen
            if (this.position.Y > Window.Height)
            {
                platformNotificationBlinkLocation.Y = Window.Height - 5;
            }
            else if (this.position.Y < 0)
            {
                platformNotificationBlinkLocation.Y = 5;
            }
            else
            {
                platformNotificationBlinkLocation.Y = this.position.Y;
            }
        }

        //timer to make the notification blink in and out
        platformNotificationBlinkTimer += 0.1f;

        if (platformNotificationBlinkTimer > 5)
        {
            platformNotificationBlinkTimer = 0;
        }
        if (platformNotificationBlinkTimer < 1)
        {
            Draw.SetLineColor(255, 0, 50);
            Draw.SetFillColor(0, 100, 200);
            Draw.Circle(platformNotificationBlinkLocation, 15);
        }
    }

    public void turretAngleFinder(float offsetCenterX, float offsetCenterY)
    {
        //tells the turrets the rotation needed to aim at the player
        this.RotToPlayerVector = Vector2.Normalize(playerLocationFalse - (this.position + new Vector2(offsetCenterX, offsetCenterY)));
        this.RotToPlayerFloat = MathF.Atan2(RotToPlayerVector.Y, RotToPlayerVector.X) * (180 / MathF.PI) + 90;

        //update the rotation
        Graphics.Rotation = RotToPlayerFloat;
    }

    public void platformDraw(Vector2 centerLocation, float offsetCenterX, float offsetCenterY, int spriteLocationY, float platformScale, float platformRotation, int platformHP, bool platformHitSinceLastCheck)
    {
        //draw the platform
        Graphics.Scale = platformScale;
        Graphics.Rotation = platformRotation;
        if ((platformHP > 0) && (platformHitSinceLastCheck == false))
        {
            Graphics.DrawSubset(platformGraphics, centerLocation + new Vector2(offsetCenterX * platformScale, offsetCenterY * platformScale), new Vector2(0, spriteLocationY * 64), new Vector2(64, 64), new Vector2(32, 32));
        }
        else
        {
            Graphics.DrawSubset(platformGraphics, centerLocation + new Vector2(offsetCenterX * platformScale, offsetCenterY * platformScale), new Vector2(64, spriteLocationY * 64), new Vector2(64, 64), new Vector2(32, 32));
        }

        //draw turret ontop of the platform
        if (platformHP > 0)
        {
            turretAngleFinder(offsetCenterX * platformScale, offsetCenterY * platformScale);
            Graphics.DrawSubset(platformGraphics, centerLocation + new Vector2(offsetCenterX * platformScale, offsetCenterY * platformScale), new Vector2(128, spriteLocationY * 64), new Vector2(64, 64), new Vector2(32, 32));
        }
    }
}