using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Linq;

namespace ShootShapesUp
{
    public class GameRoot : Game
    {
        // some helpful static properties
        public static GameRoot Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }

        public static Texture2D Player { get; private set; }
        public static Texture2D Seeker { get; private set; }
        public static Texture2D Bullet { get; private set; }
        public static Texture2D Pointer { get; private set; }
        public static Texture2D Wanderer { get; private set; }
        public static Texture2D Boss1 { get; private set; }
        public static Texture2D MMBB { get; private set; }

        public static Texture2D L2BB { get; private set; }

        public static Texture2D BFBB { get; private set; }
        public static Texture2D GWBB { get; private set; }

        public static Texture2D BG { get; private set; }

        public static Texture2D Rocket { get; private set; }
        public static Texture2D BossE { get; private set; }

        enum GameState
        {
            MainMenu,
            LevelOne,
            LevelOneComp,
            LevelTwo,
            LevelTwoComp,
            LevelThree,
            LevelThreeComp,
            EndOfGame,
        }

        GameState _state;


        public static SpriteFont Font { get; private set; }
        public static SpriteFont Font1 { get; private set; }

        public static Song Music { get; private set; }

        private static readonly Random rand = new Random();

        private static SoundEffect[] explosions;
        // return a random explosion sound
        public static SoundEffect Explosion { get { return explosions[rand.Next(explosions.Length)]; } }

        private static SoundEffect[] shots;
        public static SoundEffect Shot { get { return shots[rand.Next(shots.Length)]; } }

        private static SoundEffect[] spawns;
        public static SoundEffect Spawn { get { return spawns[rand.Next(spawns.Length)]; } }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont scoreDisplay;

        public GameRoot()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = @"Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            base.Initialize();

            EntityManager.Add(PlayerShip.Instance);

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(GameRoot.Music);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Player = Content.Load<Texture2D>("Art/Hero");
            Seeker = Content.Load<Texture2D>("Art/Seeker");
            Bullet = Content.Load<Texture2D>("Art/Bullet");
            Pointer = Content.Load<Texture2D>("Art/Pointer");
            Wanderer = Content.Load<Texture2D>("Art/Wanderer");
            Boss1 = Content.Load<Texture2D>("Art/Boss");
            BossE = Content.Load<Texture2D>("Art/Boss");
            Rocket = Content.Load<Texture2D>("Art/Rockets");
            MMBB = Content.Load<Texture2D>("Art/MainMenu");
            L2BB = Content.Load<Texture2D>("Art/L2BB");
            BFBB = Content.Load<Texture2D>("Art/BFBB");
            GWBB = Content.Load<Texture2D>("Art/GWBB");
            BG = Content.Load<Texture2D>("Art/BG");

            Font = Content.Load<SpriteFont>("Font");
            Font1 = Content.Load<SpriteFont>("Font1");
            


            Music = Content.Load<Song>("Sound/Music");

            // These linq expressions are just a fancy way loading all sounds of each category into an array.
            explosions = Enumerable.Range(1, 8).Select(x => Content.Load<SoundEffect>("Sound/explosion-0" + x)).ToArray();
            shots = Enumerable.Range(1, 4).Select(x => Content.Load<SoundEffect>("Sound/shoot-0" + x)).ToArray();
            spawns = Enumerable.Range(1, 8).Select(x => Content.Load<SoundEffect>("Sound/spawn-0" + x)).ToArray();
        }









        private void UpdateMainMenu(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _state = GameState.LevelOne;
            }
        }
        void UpdateLevelOne(GameTime deltaTime)
        {
            // Respond to user actions in the game .
            if (Input.WasButtonPressed(Buttons.Back) || Input.WasKeyPressed(Keys.Escape))
                this.Exit();
            if (ScoreAndLives.Score > 100)
            {

                _state = GameState.LevelOneComp;


            }

            EntityManager.Update();
            EnemySpawner.Update();


        }
        void UpdateLevelOneComp(GameTime deltaTime)
        {
            // Respond to user actions in the game .
            if (Input.WasButtonPressed(Buttons.Back) || Input.WasKeyPressed(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _state = GameState.LevelTwo;
            }


            EntityManager.Update();
            EnemySpawner.Update();


        }
        void UpdateLevelTwo(GameTime deltaTime)
        {
            // Respond to user actions in the game .
            if (Input.WasButtonPressed(Buttons.Back) || Input.WasKeyPressed(Keys.Escape))
                this.Exit();
            if (ScoreAndLives.Score > 300)
            {

                _state = GameState.LevelTwoComp;


            }

            EntityManager.Update();
            EnemySpawner.Update();
            WandererSpawn.Update();


        }
        void UpdateLevelTwoComp(GameTime deltaTime)
        {
            // Respond to user actions in the game .
            if (Input.WasButtonPressed(Buttons.Back) || Input.WasKeyPressed(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _state = GameState.LevelThree;
            }


            EntityManager.Update();
            EnemySpawner.Update();
            WandererSpawn.Update();


        }

        void UpdateLevelThree(GameTime deltaTime)
        {
            if (Input.WasButtonPressed(Buttons.Back) || Input.WasKeyPressed(Keys.Escape))
                this.Exit();
            if (ScoreAndLives.Score > 1000)
            {
                _state = GameState.LevelThreeComp;

            }
            EntityManager.Update();
            EnemySpawner.Reset();
            WandererSpawn.Reset();
           
            EntityManager.Update();
            Boss.Update();
            BossA.Update();
            BossB.Update();
            BossC.Update();
            BossD.Update();



        }
        void UpdateLevelThreeComp(GameTime deltaTime)
        {
            if (Input.WasButtonPressed(Buttons.Back) || Input.WasKeyPressed(Keys.Escape))
                this.Exit();
            if (ScoreAndLives.Score > 1000)
            {
                _state = GameState.EndOfGame;

            }

            EntityManager.Update();
            EnemySpawner.Reset();
            WandererSpawn.Reset();

        }
        void UpdateEndOfGame(GameTime deltaTime)
        {

        }
















        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            Input.Update();
            switch (_state)
            {
                case GameState.MainMenu:
                    UpdateMainMenu(gameTime);
                    break;
                case GameState.LevelOne:
                    UpdateLevelOne(gameTime);
                    break;
                case GameState.LevelOneComp:
                    UpdateLevelOneComp(gameTime);
                    break;
                case GameState.LevelTwo:
                    UpdateLevelTwo(gameTime);
                    break;
                case GameState.LevelTwoComp:
                    UpdateLevelTwoComp(gameTime);
                    break;
                case GameState.LevelThree:
                    UpdateLevelThree(gameTime);
                    break;
                case GameState.LevelThreeComp:
                    UpdateLevelThreeComp(gameTime);
                    break;

                case GameState.EndOfGame:
                    UpdateEndOfGame(gameTime);
                    break;
            }

            // Allows the game to exit

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw entities. Sort by texture for better batching.
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
            EntityManager.Draw(spriteBatch);
            spriteBatch.End();

            // Draw user interface
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            spriteBatch.Draw(GameRoot.Pointer, Input.MousePosition, Color.White);

            spriteBatch.DrawString(Font, string.Format("Star Legion Assault"), new Vector2(5), Color.White);
            spriteBatch.Draw(BG, new Rectangle(0, 0, 800, 800), Color.White);
            DrawRightAlignedString(String.Format("Score:  {0}", ScoreAndLives.Score), 5);
           

            spriteBatch.End();
            base.Draw(gameTime);
            switch (_state)
            {
                case GameState.MainMenu:
                    DrawMainMenu(gameTime);
                    break;
                case GameState.LevelOne:
                    DrawLevelOne(gameTime);
                    break;
                case GameState.LevelOneComp:
                    DrawLevelOneComp(gameTime);
                    break;

                case GameState.LevelTwo:
                    DrawLevelTwo(gameTime);
                    break;
                case GameState.LevelTwoComp:
                    DrawLevelTwoComp(gameTime);
                    break;
                case GameState.LevelThree:
                    DrawLevelThree(gameTime);
                    break;
                case GameState.LevelThreeComp:
                    DrawLevelThreeComp(gameTime);
                    break;

                case GameState.EndOfGame:
                    DrawEndOfGame(gameTime);
                    break;
            }

            base.Draw(gameTime);
        }

        private void DrawMainMenu(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Draw(MMBB, new Rectangle(0, 0, 800, 800), Color.White);
            spriteBatch.DrawString(Font, "The Star Legion has started a frontal assault to our colony, it is our job to", new Vector2(150,200), Color.White);
            spriteBatch.DrawString(Font, "prevent their suicide bombers from getting passed us and directly", new Vector2(155, 214), Color.White);
            spriteBatch.DrawString(Font, "attacking our Base", new Vector2(300, 228), Color.White);
            spriteBatch.DrawString(Font, "Press ENTER to Start", new Vector2(graphics.PreferredBackBufferWidth / 2.5f, graphics.PreferredBackBufferHeight / 2f), Color.White);
            spriteBatch.End();

        }
        void DrawLevelOne(GameTime deltaTime)
        {
            spriteBatch.Begin();
            //spriteBatch.DrawString(Font, "Boss Health: " + Boss.enemyHealth, new Vector2(350, graphics.PreferredBackBufferHeight / 9f), Color.White);
            spriteBatch.End();
            if (ScoreAndLives.Score < 20)
            {
                spriteBatch.Begin();

                spriteBatch.DrawString(Font, "Stage 1", new Vector2(graphics.PreferredBackBufferWidth / 2.2f, graphics.PreferredBackBufferHeight / 10f), Color.White);
                spriteBatch.End();
            }

            if (ScoreAndLives.Score >= 20)
            {
                spriteBatch.Begin();

                spriteBatch.DrawString(Font, "Keep it Up Rookie", new Vector2(350, graphics.PreferredBackBufferHeight / 10f), Color.White);
                

                spriteBatch.End();
            }
        }
        void DrawLevelOneComp(GameTime deltaTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(L2BB, new Rectangle(0, 0, 800, 800), Color.White);
            spriteBatch.DrawString(Font, "We Have More Enemy Coming", new Vector2(325, graphics.PreferredBackBufferHeight / 10f), Color.White);
            spriteBatch.DrawString(Font, "Press ENTER to Continue", new Vector2(graphics.PreferredBackBufferWidth / 2.5f, graphics.PreferredBackBufferHeight / 2f), Color.White);
            spriteBatch.End();
        }
        void DrawLevelTwo(GameTime deltaTime)
        {
            if (ScoreAndLives.Score <= 140)
            {

                spriteBatch.Begin();

                spriteBatch.DrawString(Font, "Stage 2", new Vector2(graphics.PreferredBackBufferWidth / 2.2f, graphics.PreferredBackBufferHeight / 10f), Color.White);
                spriteBatch.End();
            }
            if (ScoreAndLives.Score <= 200)
            {
                spriteBatch.Begin();

                spriteBatch.DrawString(Font, "Rookie Watch Out More Bogies In Your Perimeter ", new Vector2(250, graphics.PreferredBackBufferHeight / 8f), Color.White);
                spriteBatch.End();
            }

        }
        void DrawLevelTwoComp(GameTime deltaTime)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(BFBB, new Rectangle(0, 0, 800, 800), Color.White);
            spriteBatch.DrawString(Font, "Mothership Incoming", new Vector2(graphics.PreferredBackBufferWidth / 2.2f, graphics.PreferredBackBufferHeight / 10f), Color.White);
            spriteBatch.DrawString(Font, "Destroy and the Assault stops!!", new Vector2(325, 100), Color.White);
            spriteBatch.DrawString(Font, "Press ENTER to Continue", new Vector2(graphics.PreferredBackBufferWidth / 2.5f, graphics.PreferredBackBufferHeight / 2f), Color.White);
            spriteBatch.DrawString(Font1, "ACTIVE HYPERMODE!!!", new Vector2(250, 125), Color.White);
            spriteBatch.End();
        }
        void DrawLevelThree(GameTime deltaTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Boss1, new Rectangle(0, -50, 800, 300), Color.White);
            spriteBatch.DrawString(Font, "Boss Fight", new Vector2(graphics.PreferredBackBufferWidth / 2.2f, graphics.PreferredBackBufferHeight / 10f), Color.White);
            spriteBatch.DrawString(Font1, "Missiles Incoming!", new Vector2(250, 125), Color.White);
            spriteBatch.End();
        }
        void DrawLevelThreeComp(GameTime deltaTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(MMBB, new Rectangle(0, 0, 800, 800), Color.White);
            spriteBatch.DrawString(Font, "Boss Fight", new Vector2(graphics.PreferredBackBufferWidth / 2.2f, graphics.PreferredBackBufferHeight / 10f), Color.White);
            spriteBatch.Draw(Boss1, new Rectangle(0, 0, 200, 150), Color.White);
            spriteBatch.End();
        }


        void DrawEndOfGame(GameTime deltaTime)
        {
            // Draw text and scores
            // Draw menu for restarting level or going back to main menu
            spriteBatch.Begin();
            spriteBatch.Draw(GWBB, new Rectangle(0, 0, 800, 800), Color.White);
            spriteBatch.DrawString(Font, "You Won", new Vector2(graphics.PreferredBackBufferWidth / 2.2f, graphics.PreferredBackBufferHeight / 10f), Color.White);
            spriteBatch.End();
        }


        private void DrawRightAlignedString(string text, float y)
        {
            var textWidth = GameRoot.Font.MeasureString(text).X;
            spriteBatch.DrawString(GameRoot.Font, text, new Vector2(ScreenSize.X - textWidth - 5, y), Color.White);
        }





    }
}