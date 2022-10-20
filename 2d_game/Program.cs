using Raylib_cs;
using System.Numerics;


Raylib.InitWindow(1024, 768, "Escape The Dungeon");
Raylib.SetTargetFPS(60);

float speed = 4.5f;
string currentScene = "start";
Texture2D avatarImage = Raylib.LoadTexture("avatar.png");
Texture2D startBackground = Raylib.LoadTexture("startbackground.png");
Texture2D gameBackground = Raylib.LoadTexture("gamebackground.png");


Rectangle character = new Rectangle(0, 0, 100, 100);

while(!Raylib.WindowShouldClose()){
    
    //LOGIK

    if (currentScene == "start")
    {
    if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
    {
      currentScene = "game";
    }
    }

    else if (currentScene == "game")
    {
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
    {
      character.x += speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
    {
      character.x -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
    {
      character.y += speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_UP))
    {
      character.y -= speed;
    }
    }


    //GRAFIK
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    if (currentScene == "game")
    {
    Raylib.DrawTexture(gameBackground, 0, 0, Color.WHITE);
    Raylib.DrawTexture(avatarImage,
            (int)character.x,
            (int)character.y,
            Color.WHITE
          );
    }
    else if (currentScene == "start")
    {
        Raylib.DrawTexture(startBackground, 0, 0, Color.WHITE);
        Raylib.DrawText("Escape The Dungeon", 250, 350, 50, Color.BLACK);
        Raylib.DrawText("Press ENTER to start", 315, 600, 32, Color.BLACK);
    }

  Raylib.EndDrawing();
}
