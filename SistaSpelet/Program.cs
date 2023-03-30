using Raylib_cs;

Raylib.InitWindow (1000, 800, "Plattform");
Raylib.SetTargetFPS (60);

Texture2D PlayerImage = Raylib.LoadTexture("Player.png");
Rectangle playerRect = new Rectangle(0, 0, PlayerImage.width, PlayerImage.height);



string currentScene = "start";
float speed = 10f;
while (!Raylib.WindowShouldClose())
{
//    Logik
    if (currentScene == "game")
    {
     if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            playerRect.x += speed;
        }

        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            playerRect.x -= speed;
        }

      
    }
    else if (currentScene == "start")
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
        }  
    }
}

//Grafik
Raylib.BeginDrawing();
Raylib.ClearBackground(Color.WHITE);

if (currentScene == "game")
{
    Raylib.DrawTexture(PlayerImage,(int)playerRect.x,(int) playerRect.y, Color.WHITE);
}
else if (currentScene == "start")
    {
        Raylib.DrawText("Press enter to start", 50, 560, 50, Color.WHITE);
    }
    else if (currentScene == "end")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("Game Over!", 50, 560, 50, Color.WHITE);
        //Avslutar spelet med en svart skärm och en vit text
    }

Raylib.ClearBackground(Color.WHITE);
    
Raylib.EndDrawing(); 
