using System.Collections.Generic;

namespace Framework
{
    public class FakeDatabase : IDatabase
    {
        public string[] getSongs(int numberOfSongs, EmotionSpaceDTO emotionSpaceDTO)
        {
            List<string> result = new List<string>();
            result.Add(@"D:\SVN\music-classifier\EmotionalGUI\Sample Music\Penitus.mp3");
            result.Add(@"D:\SVN\music-classifier\EmotionalGUI\Sample Music\Thunder Rolls.mp3");
            result.Add(@"D:\SVN\music-classifier\EmotionalGUI\Sample Music\Tricksome.mp3");
            result.Add(@"D:\SVN\music-classifier\EmotionalGUI\Sample Music\Upon The Rocks.mp3");
            return result.ToArray();
        }

        public string[] getSongsWithConstraints(int numberOfSongs, EmotionSpaceDTO minValueEmotionSpaceDTO, EmotionSpaceDTO maxValueEmotionSpaceDTO)
        {
            return null;
        }

        public void addSongToDatabase(string songpath, EmotionSpaceDTO emotionSpaceDTO)
        {
            return;
        }
    }
}
