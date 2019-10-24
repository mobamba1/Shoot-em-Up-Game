using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#if WINDOWS_UAP
    using Windows.Storage;
#endif

namespace ShootShapesUp

{
    static class ScoreAndLives
    {
        private const float multiplierExpiryTime = 1f;
        private const int maxMultiplier = 2;
        public static int HighScore;

        public static string highScoreFilename = "highscore.txt";


        private static int lives;
        private static int score;
        private static int multiplier;
        private static bool isGameOver;

       
        


        //How many lives the player has
        public static int Lives
        {
            get
            {
                return lives;
            }
            private set
            {
                lives = value;
            }
        }

        //player score
        public static int Score
        {
            get
            {
                return score;
            }
            private set
            {
                score = value;
            }
        }

        // score mulitplier
        public static int Multiplier
        {
            get
            {
                return multiplier;
            }
            private set
            {
                multiplier = value;
            }
        }

        // if player runs out of lives
        public static bool IsGameOver
        {
            get
            {
                return isGameOver;

            }
            set
            {
                isGameOver = value;
            }
        }

        
        static ScoreAndLives()
        {
           
            HighScore = LoadHighScore();
            Reset();
        }

        // Resets everything
        public static void Reset()
        {
            
            if (Score > HighScore)
            {
                SaveHighScore(Score);
                HighScore = Score;
            }

            Score = 0;
            Multiplier = 2;
            Lives = 2;
          
        }

      
        private static int LoadHighScore()
        {
            int score;

         
            if (File.Exists(highScoreFilename) && int.TryParse(File.ReadAllText(highScoreFilename), out score))
            {
          
                return score;
            }
            else
            {
              
                return 0;
            }
        }

      
        private static void SaveHighScore(int score)
        {
           
            File.WriteAllText(highScoreFilename, score.ToString());
        }

        

        public static void AddPoints(int basePoints)
        {
            // check if player is alive
            if (PlayerShip.Instance.IsDead)
            {
                return;
            }

            // add score to multiplier
            Score += basePoints * Multiplier;

          
        }

        public static void IncreaseMultiplier()
        {
            
            if (PlayerShip.Instance.IsDead)
            {
                return;
            }

           
            if (Multiplier < maxMultiplier)
            {
                Multiplier += 1;
            }
        }

        
        public static void ResetMultiplier()
        {
            Multiplier = 1;
        }

        // removes lives when player is hit
        public static void RemoveLife()
        {
            Lives -= 1;
        }



    }
}