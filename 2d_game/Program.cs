using Raylib_cs;
using System.Numerics;

const int screenwidth = 1024;
const int screenheight = 768;
Raylib.InitWindow(screenwidth, screenheight, "Escape The Dungeon");
Raylib.SetTargetFPS(60);


List<Rectangle> walls = new List<Rectangle>();
walls.Add(new Rectangle(1000, 600, 32, 100));

List<Enemy> enemies = new List<Enemy>();
enemies.Add(new Enemy());
enemies.Add(new Enemy());

float speed = 4.5f;
string currentScene = "start";
Texture2D avatarImage = Raylib.LoadTexture("avatar.png");
Texture2D startBackground = Raylib.LoadTexture("startbackground.png");
Texture2D gameBackground = Raylib.LoadTexture("gamebackground.png");


Rectangle character = new Rectangle(0, 0, avatarImage.width, avatarImage.height);

Vector2 enemyMovement = new Vector2(1, 0);
float enemySpeed = 3;

//////////////////////////////////////////////////////////////////////////////////////
while(!Raylib.WindowShouldClose()){
    
    //LOGIK

    if (currentScene == "start"){
        character.x = 0;
        character.y = 0;
        enemies[0].rect.x = 700;
        enemies[0].rect.y = 500;
        enemies[1].rect.x = 300;
        enemies[1].rect.y = 500;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
        }
    }

    else if (currentScene == "game")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && character.x < (screenwidth - 42) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && character.x < (screenwidth - 42))
        {
        character.x += speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && character.x > 0 || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && character.x > 0)
        {
        character.x -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && character.y + 96 < screenheight || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) && character.y + 96 < screenheight)
        {
        character.y += speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && character.y > 0 || Raylib.IsKeyDown(KeyboardKey.KEY_UP) && character.y > 0)
        {
        character.y -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ESCAPE)){
            System.Environment.Exit(1);
        }
        foreach(Enemy e in enemies){
            Vector2 playerPos = new Vector2(character.x, character.y);
            Vector2 enemyPos = new Vector2(e.rect.x, e.rect.y);
            Vector2 diff = playerPos - enemyPos;
            Vector2 enemyDirection = Vector2.Normalize(diff);
            enemyMovement = enemyDirection * enemySpeed;
            e.rect.x += enemyMovement.X;
            e.rect.y += enemyMovement.Y;

            if (Raylib.CheckCollisionRecs(character, e.rect)){
                currentScene = "gameOver";
            }
        }
        if (Raylib.CheckCollisionRecs(character, walls[0])){
            currentScene = "win";
        }
    }
    else if (currentScene == "gameOver"){
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)){
            currentScene = "start";
        }
    }
    else if (currentScene == "win"){
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
        foreach(Enemy e in enemies){
            Raylib.DrawTexture(e.enemyImage, (int)e.rect.x, (int)e.rect.y, Color.WHITE);
        }
        foreach (Rectangle wall in walls){
            Raylib.DrawRectangleRec(wall, Color.GOLD);
        }
    }
    else if (currentScene == "start")
    {
        Raylib.DrawTexture(startBackground, 0, 0, Color.WHITE);
        Raylib.DrawText("Escape The Dungeon", 250, 350, 50, Color.BLACK);
        Raylib.DrawText("Press ENTER to start", 315, 600, 32, Color.BLACK);
    }
    else if (currentScene == "gameOver"){
        Raylib.DrawText("Game over!", 315, 600, 50, Color.BLACK);
    }
    else if (currentScene == "win"){
        Raylib.DrawText("Congratulations!", 315, 400, 50, Color.BLACK);
    }

  Raylib.EndDrawing();
}