using Raylib_cs;

public class GameManager
{
    public const int SCREEN_WIDTH = 800;
    public const int SCREEN_HEIGHT = 600;
    private float _spawnTimer;
    private float _spawnInterval;

    private string _title;
    private List<GameObject> _gameObjects = new List<GameObject>();
    private Random _random = new Random();
    private int _score;
    private int _playerLives;
    private bool _isGameOver;
    private Music _backgroundSound;
    private Sound _treasureSound;
    private Sound _bombSound;

    public GameManager()
    {
        _title = "CSE 210 Game";
    }

    /// <summary>
    /// The overall loop that controls the game. It calls functions to
    /// handle interactions, update game elements, and draw the screen.
    /// </summary>
    public void Run()
    {
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, _title);
        // If using sound, un-comment the lines to init and close the audio device
        Raylib.InitAudioDevice();

        InitializeGame();

        while (!Raylib.WindowShouldClose())
        {
            if (!_isGameOver)
            {
                Raylib.UpdateMusicStream(_backgroundSound);
                HandleInput();
                ProcessActions();
            }
            else
            {
                if (Raylib.IsKeyPressed(KeyboardKey.Space))
                {
                    ReinitializeGame();
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            DrawElements();

            Raylib.EndDrawing();
        }
        UnloadGame();
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    private void UnloadGame()
    {
        Raylib.UnloadMusicStream(_backgroundSound);
        Raylib.UnloadSound(_treasureSound);
        Raylib.UnloadSound(_bombSound);

    }


    public void SpawnNewSubject()
    {
        int randomX = _random.Next(0, SCREEN_WIDTH - 30);
        int spawnY = -30;
        if (_random.Next(0, 2) == 0)
        {
            Treasure newTreasure = new Treasure(randomX, spawnY, this);
            _gameObjects.Add(newTreasure);

        }
        else
        {
            Bomb newBomb = new Bomb(randomX, spawnY, this);
            _gameObjects.Add(newBomb);

        }


    }

    /// <summary>
    /// Sets up the initial conditions for the game.
    /// </summary>
    private void InitializeGame()
    {
        _isGameOver = false;
        _spawnTimer = 0f;
        _spawnInterval = 0.5f;
        _score = 0;
        _playerLives = 3;
        _backgroundSound = Raylib.LoadMusicStream("wav/pianos-by-jtwayne-7-174717.mp3");
        _treasureSound = Raylib.LoadSound("smooth-ac-guitar-loop-93bpm-137706.mp3");
        Console.WriteLine($"Treasure sound loaded {_treasureSound}"); // Check if ID is not 0 (invalid)
        _bombSound = Raylib.LoadSound("sunflower-street-drumloop-85bpm-163900.mp3");
        Console.WriteLine($"Bomb sound loaded {_bombSound}"); // Check if ID is not 0 (invalid)
        Raylib.PlayMusicStream(_backgroundSound);

        // use Raylib.GetFrameTime() in order to get the last frame
        // float deltaTime = Raylib.GetFrameTime();
        // _spawnTimer += deltaTime;
        // // Check if spawnTimer is equal to spawnInterval
        // if (_spawnTimer >= _spawnInterval)
        // {
        //     SpawnNewSubject();
        //     _spawnTimer -= _spawnInterval;
        // }

        // create player and add to list
        Player p = new Player(SCREEN_WIDTH / 2, SCREEN_HEIGHT - 50);
        _gameObjects.Add(p);
    }

    private void ReinitializeGame()
    {
        _isGameOver = false;
        _spawnTimer = 0f;
        _spawnInterval = 0.5f;
        _score = 0;
        _playerLives = 3;
        _gameObjects.RemoveAll(gameObject => gameObject is Treasure);
        _gameObjects.RemoveAll(gameObject => gameObject is Bomb);
    }

    public void IncreaseSpawnInterval()
    {
        if (_spawnInterval < 0.65f)
        {
            _spawnInterval -= 0.01f;
        }
    }
    public void DecreaseSpawnInterval()
    {
        _spawnInterval += 0.05f;

    }


    /// <summary>
    /// Responds to any input from the user.
    /// </summary>
    private void HandleInput()
    {

    }

    public bool IsCollision(GameObject first, GameObject second)
    {
        if (first.GetLeftEdge() <= second.GetRightEdge() && first.GetRightEdge() >= second.GetLeftEdge()
        && first.GetBottomEdge() >= second.GetTopEdge() && first.GetTopEdge() <= second.GetBottomEdge())
        {
            return true;
        }
        else return false;
    }

    public void IncrementScore(int _value)
    {
        _score += _value;
        Console.WriteLine($"Score incremented to: {_score}");
    }

    public int GetScore()
    {
        return _score;
    }

    public void DecreaseLives()
    {
        _playerLives -= 1;
        Console.WriteLine($"Lives decreased to: {_playerLives}");
    }
    public int GetLives()
    {
        return _playerLives;
    }

    /// <summary>
    /// Processes any actions such as moving objects or handling collisions.
    /// </summary>
    private void ProcessActions()
    {
        if (!_isGameOver)
        {
            foreach (GameObject item in _gameObjects)
            {
                item.ProcessActions();
            }

            float deltaTime = Raylib.GetFrameTime();
            _spawnTimer += deltaTime;
            // Check if spawnTimer is equal to spawnInterval
            if (_spawnTimer >= _spawnInterval)
            {
                SpawnNewSubject();
                _spawnTimer -= _spawnInterval;
            }

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                for (int j = i + 1; j < _gameObjects.Count; j++)
                {
                    GameObject first = _gameObjects[i];
                    GameObject second = _gameObjects[j];

                    if (IsCollision(first, second))
                    {
                        if (first is Player && second is Treasure)
                        {
                            _gameObjects.Remove(second);
                            second.CollideWith(first);
                            Raylib.PlaySound(_treasureSound);
                            // Console.WriteLine("Player collided with Treasure - attempting removal.");
                            break;
                        }
                        else if (second is Player && first is Treasure)
                        {
                            // Console.WriteLine(first.GetType().Name);
                            // Console.WriteLine(second.GetType().Name);
                            _gameObjects.Remove(first);
                            second.CollideWith(first);
                            Raylib.PlaySound(_treasureSound);
                            Console.WriteLine("Player collided with Treasure - attempting removal.");
                            break;


                        }
                        else if (first is Player && second is Bomb)
                        {
                            // Console.WriteLine(first.GetType().Name);
                            // Console.WriteLine(second.GetType().Name);
                            _gameObjects.Remove(second);
                            second.CollideWith(first);
                            Raylib.PlaySound(_bombSound);

                            Console.WriteLine("Player collided with Bomb - attempting removal.");
                            if (_playerLives <= 0)
                            {
                                _isGameOver = true;
                                // DrawEnd();
                            }
                            break;

                        }
                        else if (second is Player && first is Bomb)
                        {
                            Console.WriteLine(first.GetType().Name);
                            Console.WriteLine(second.GetType().Name);
                            _gameObjects.Remove(first);
                            second.CollideWith(first);
                            Raylib.PlaySound(_bombSound);
                            Console.WriteLine("Player collided with Bomb - attempting removal.");
                            break;

                        }

                    }
                }
            }
        }
        else
        {

        }
    }

    public void DrawEnd()
    {
        Raylib.DrawText("Game Over", SCREEN_WIDTH / 2 - 145, SCREEN_HEIGHT / 2 - 70, 60, Color.DarkBrown);
        Raylib.DrawText($"Final Score: {_score.ToString()}", SCREEN_WIDTH / 2 - 70, SCREEN_HEIGHT / 2, 20, Color.DarkBrown);
        Raylib.DrawText($"Press SPACE to restart", SCREEN_WIDTH / 2 - 120, SCREEN_HEIGHT / 2 + 30, 20, Color.DarkBlue);
    }

    public void DrawLives()
    {
        int centerX = 90;
        int centerY = 30;
        int radius = 7;
        int spaceBetween = 15;

        for (int i = 0; i < _playerLives; i++)
        {
            int positionX = centerX + (i * (radius * 2 + spaceBetween));
            Raylib.DrawCircle(positionX, centerY, radius, Color.Red);
        }
    }

    /// <summary>
    /// Draws all elements on the screen.
    /// </summary>
    private void DrawElements()
    {
        if (!_isGameOver)
        {
            Raylib.DrawText($"Score. {_score.ToString()}", SCREEN_WIDTH - 110, 20, 20, Color.Black);
            Raylib.DrawText($"Lives.", 20, 20, 20, Color.Black);
            DrawLives();
            foreach (GameObject item in _gameObjects)
            {
                item.Draw();
            }
        }
        else
        {
            DrawEnd();
        }
    }

    // private void DrawElements()
    // {
    //     foreach (GameObject item in _gameObjects)
    //     {
    //         Console.WriteLine($"{item.GetType().Name} - Left: {item.GetLeftEdge()}, Right: {item.GetRightEdge()}, Top: {item.GetTopEdge()}, Bottom: {item.GetBottomEdge()}");
    //         item.Draw();
    //     }
    // }
}

// TODO: Make something happen when the objects collide with the player. 
// TODO: Make the objects appear continuously until the end of the game.