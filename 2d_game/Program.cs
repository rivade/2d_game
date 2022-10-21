using Raylib_cs;
using System.Numerics;


Raylib.InitWindow(1024, 768, "Escape The Dungeon");
Raylib.SetTargetFPS(60);

float speed = 4.5f;
string currentScene = "start";
Texture2D avatarImage = Raylib.LoadTexture("avatar.png");
Texture2D startBackground = Raylib.LoadTexture("startbackground.png");
Texture2D gameBackground = Raylib.LoadTexture("gamebackground.png");


Rectangle character = new Rectangle(0, 0, avatarImage.width, avatarImage.height);
Rectangle enemyRect = new Rectangle(700, 500, 96, 96);

Vector2 enemyMovement = new Vector2(1, 0);
float enemySpeed = 2;

//////////////////////////////////////////////////////////////////////////////////////
while(!Raylib.WindowShouldClose()){
    
    //LOGIK

    if (currentScene == "start"){
        character.x = 0;
        character.y = 0;
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

        Vector2 playerPos = new Vector2(character.x, character.y);
        Vector2 enemyPos = new Vector2(enemyRect.x, enemyRect.y);
        Vector2 diff = playerPos - enemyPos;
        Vector2 enemyDirection = Vector2.Normalize(diff);


        enemyMovement = enemyDirection * enemySpeed;

        enemyRect.x += enemyMovement.X;
        enemyRect.y += enemyMovement.Y;

        if (Raylib.CheckCollisionRecs(character, enemyRect)){
            currentScene = "gameOver";
        }
    }
    else if (currentScene == "gameOver"){
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)){
            currentScene = "start";
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
        Raylib.DrawRectangleRec(enemyRect, Color.RED);
    }
    else if (currentScene == "start")
    {
        Raylib.DrawTexture(startBackground, 0, 0, Color.WHITE);
        Raylib.DrawText("Escape The Dungeon", 250, 350, 50, Color.BLACK);
        Raylib.DrawText("Press ENTER to start", 315, 600, 32, Color.BLACK);
    }
    else if (currentScene == "gameOver"){
        Raylib.DrawText("Game over!", 315, 600, 32, Color.BLACK);
    }

  Raylib.EndDrawing();
}
