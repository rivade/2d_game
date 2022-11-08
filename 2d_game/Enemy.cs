using System;
using Raylib_cs;



public class Enemy
{
    public Texture2D enemyImage  = Raylib.LoadTexture("enemy.png");
    public Rectangle rect = new Rectangle(0, 0, 60, 72);
}
