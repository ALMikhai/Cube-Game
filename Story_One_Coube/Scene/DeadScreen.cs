﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace Story_One_Coube.Scene
{
    class DeadScreen
    {

        Sprite preDeadScreenWords;

        Texture exitTexture;
        Sprite exitSprite;
        IntRect exitRect;

        Texture mainMenuTexture;
        Sprite mainMenuSprite;
        IntRect mainMenuRect;

        Texture restartTexture;
        Sprite restartSprite;
        IntRect restartRect;

        double wordAnimationTime;
        DateTime timeMow;

        public DeadScreen(RenderWindow window)
        {
            wordAnimationTime = 3;
            timeMow = DateTime.Now;

            preDeadScreenWords = new Sprite(new Texture("../../Texturs/PreDeadScreen.png", new IntRect(0, 96, 597, 84)));
            preDeadScreenWords.Origin = new Vector2f(preDeadScreenWords.Texture.Size.X / 2, preDeadScreenWords.Texture.Size.Y / 2);
            preDeadScreenWords.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            preDeadScreenWords.Scale = new Vector2f((float)0, (float)0);
            
            exitTexture = new Texture("../../Texturs/DeadScreenText.png", new IntRect(60, 13, 80, 27));
            exitSprite = new Sprite(exitTexture);
            exitSprite.Origin = new Vector2f(exitTexture.Size.X / 2, exitTexture.Size.Y / 2);
            exitSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2 + 35);
            exitRect = new IntRect((int)((exitSprite.Position.X) - (exitTexture.Size.X / 2)), (int)((exitSprite.Position.Y) - (exitTexture.Size.Y / 2)), (int)exitTexture.Size.X, (int)exitTexture.Size.Y);

            mainMenuTexture = new Texture("../../Texturs/DeadScreenText.png", new IntRect(6, 89, 189, 27));
            mainMenuSprite = new Sprite(mainMenuTexture);
            mainMenuSprite.Origin = new Vector2f(mainMenuTexture.Size.X / 2, mainMenuTexture.Size.Y / 2);
            mainMenuSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            mainMenuRect = new IntRect((int)((mainMenuSprite.Position.X) - (mainMenuTexture.Size.X / 2)), (int)((mainMenuSprite.Position.Y) - (mainMenuTexture.Size.Y / 2)), (int)mainMenuTexture.Size.X, (int)mainMenuTexture.Size.Y);

            restartTexture = new Texture("../../Texturs/DeadScreenText.png", new IntRect(25, 178, 149, 27));
            restartSprite = new Sprite(restartTexture);
            restartSprite.Origin = new Vector2f(restartTexture.Size.X / 2, restartTexture.Size.Y / 2);
            restartSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2 - 35);
            restartRect = new IntRect((int)((restartSprite.Position.X) - (restartTexture.Size.X / 2)), (int)((restartSprite.Position.Y) - (restartTexture.Size.Y / 2)), (int)restartTexture.Size.X, (int)restartTexture.Size.Y);
        }

        public void DrawAndUpdate(RenderWindow window)
        {
            while(wordAnimationTime > 0)
            {
                if(preDeadScreenWords.Scale.X < 1 || preDeadScreenWords.Scale.Y < 1)
                {
                    preDeadScreenWords.Scale = new Vector2f((float)(preDeadScreenWords.Scale.X + 0.01), (float)(preDeadScreenWords.Scale.Y + 0.01));
                }

                window.Draw(preDeadScreenWords);
                wordAnimationTime -= (DateTime.Now - timeMow).TotalSeconds;
                timeMow = DateTime.Now;
                return;
            }

            exitSprite.Color = Color.White;
            mainMenuSprite.Color = Color.White;
            restartSprite.Color = Color.White;
            Program.deadScreenChooseNow = Program.deadScreenChoose.None;

            if (exitRect.Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                Program.deadScreenChooseNow = Program.deadScreenChoose.Exit;
                exitSprite.Color = Color.Red;
            }

            if (mainMenuRect.Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                Program.deadScreenChooseNow = Program.deadScreenChoose.MainMenu;
                mainMenuSprite.Color = Color.Red;
            }

            if (restartRect.Contains((int)Program.LastMousePosition.X, (int)Program.LastMousePosition.Y))
            {
                Program.deadScreenChooseNow = Program.deadScreenChoose.Restart;
                restartSprite.Color = Color.Red;
            }

            window.Draw(restartSprite);
            window.Draw(mainMenuSprite);
            window.Draw(exitSprite);
        }
    }
}
