using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using Story_One_Coube.Models;
using Story_One_Coube.Models.Stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Story_One_Coube.Scene
{
    internal class Level
    {
        public Character MainCharacter;
        public double mainCharacterHP = 100;
        public CharacterEvents.Moves moveNow = CharacterEvents.Moves.STOP;

        public List<Character> Enemies = new List<Character>();

        public List<Sprite> TextureObjects = new List<Sprite>();

        public List<Stuff> Stuffs = new List<Stuff>();

        public int Score = 0;

        public double TimeToAirDrop { get; protected set; }
        public double InterfaceTimeToAirDrop { get; protected set; }
        protected DateTime timeNowAirDrop;

        public DateTime pauseTimeNow { get; protected set; }
        public double timeOnPause { get; protected set; }

        protected Random random = new Random();

        protected Music levelMusic;
        protected int numLevel;

        public virtual Level RestartLevel()
        {
            Program.MainMenuChooseNow = Program.MainMenuChoose.None;
            DeadScreen.DeadScreenChooseNow = DeadScreen.DeadScreenChoose.None;
            Program.windowModeNow = Program.WindowMode.Game;
            return null;
        }

        public virtual void Update(RenderWindow window)
        {
            CharacterEvents.UpdateChar(MainCharacter);

            CharacterEvents.UpdateMainChar(moveNow, MainCharacter);

            foreach (var enemy in Enemies.ToArray())
            {
                CharacterEvents.UpdateChar(enemy);
                CharacterEvents.UpdateEnemy(enemy);
            }

            foreach(var stuff in Stuffs.ToArray())
            {
                stuff.Update(window);
            }

            if((DateTime.Now - timeNowAirDrop).TotalSeconds - timeOnPause < TimeToAirDrop)
            {
                InterfaceTimeToAirDrop = (int)(TimeToAirDrop - (DateTime.Now - timeNowAirDrop).TotalSeconds + timeOnPause);
            }
            else
            {
                int choose = random.Next(2);

                switch (choose)
                {
                    case 0:
                        {
                            Stuffs.Add(new AidKit(new Vector2f(random.Next((int)Program.WidthWindow), 0)));
                            break;
                        }
                    case 1:
                        {
                            Stuffs.Add(new Ammo(new Vector2f(random.Next((int)Program.WidthWindow), 0)));
                            break;
                        }
                }

                timeNowAirDrop = DateTime.Now;
                timeOnPause = 0;
            }
        }

        public virtual void Draw(RenderWindow window)
        {
            foreach (var stuff in Stuffs.ToArray())
            {
                stuff.Draw(window);
            }

            CharacterEvents.Draw(window, MainCharacter);

            foreach (var enemy in Enemies.ToArray())
            {
                CharacterEvents.Draw(window, enemy);
            }

            Interface.Draw(window);
        }

        public virtual void Pause()
        {
            pauseTimeNow = DateTime.Now;
        }

        public virtual void Continue()
        {
            timeOnPause += (DateTime.Now - pauseTimeNow).TotalSeconds;

            if(!MainCharacter.gunNow.isReloated) MainCharacter.gunNow.timeOnPause += (DateTime.Now - pauseTimeNow).TotalSeconds;
        }

        public virtual void LoadStuff()
        {
            Program.musicNow.Stop();
            Program.musicNow = levelMusic;
            Program.musicNow.Play();

            Background.Set(numLevel);
        }
    }
}
