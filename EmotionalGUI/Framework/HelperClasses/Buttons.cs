using System;
using System.Windows.Forms;
using System.Collections.Generic;


namespace Framework
{
    public static class Buttons
    {
        public static List<Button> getButtons(PlayList playlist)
        {
            var result = new List<Button>();

            Button but = new Button();
            but.Text = "Load Playlist Test";
            but.Click += (s, e) => (new ButtonFunctions()).CallFakeDatabase(playlist);
            result.Add(but);

            for (int i = 0; i < 30; i++)
            {

                but = new Button();
                but.Text = "Button Example " + i.ToString();
                but.Click += (s, e) => { System.Console.Beep(); };
                result.Add(but);
            }

            return result;
        }
    }

    public class ButtonFunctions
    {
        public void CallFakeDatabase(PlayList playlist)
        {
            EmotionSpaceDTO esDTO = new EmotionSpaceDTO(){Energy = 0,Positivity = 0};
            int songs = 999;

            FakeDatabase fdb = new FakeDatabase();
            playlist.loadSongs(fdb.getSongs(songs, esDTO));
        }
    }
}
