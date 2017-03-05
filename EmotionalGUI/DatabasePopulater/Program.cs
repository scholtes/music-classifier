using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;

namespace DatabasePopulater
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Tuple<string, EmotionSpaceDTO>> records = new List<Tuple<string, EmotionSpaceDTO>>();
            //records.Add(tuple1);
            //records.Add(tuple2);
            //records.Add(tuple3);
            //records.Add(tuple4);
            //records.Add(tuple5);
            //records.Add(tuple6);

            var db = ServerDatabase.Instance;
            var list = db.getSongs(99999, new EmotionSpaceDTO(1, 1));
            foreach (string song in list)
            {
                db.removeSongFromDatabase(song);
                Console.WriteLine("Removed: {0}", song);
            }

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();

            //foreach (Tuple<string, EmotionSpaceDTO> tuple in records)
            //{
            //    db.addSongToDatabase(tuple.Item1, tuple.Item2);
            //    Console.WriteLine("Added {0}", tuple.Item1);
            //}
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

        //static public Tuple<string, EmotionSpaceDTO> tuple1 = new Tuple<string, EmotionSpaceDTO>(
        //    @"E:\Music\Misc Other\Game\Ori and the Blind Forest\(Gareth Coker) - Light of Nibel.mp3", new EmotionSpaceDTO(.6, .3));
        //static public Tuple<string, EmotionSpaceDTO> tuple2 = new Tuple<string, EmotionSpaceDTO>(
        //    @"E:\Music\Misc Other\Game\Kingdom Hearts\(Yoko Shimomura) - Kingdom Hearts - Dearly Beloved.mp3", new EmotionSpaceDTO(.2, .5));
        //static public Tuple<string, EmotionSpaceDTO> tuple3 = new Tuple<string, EmotionSpaceDTO>(
        //    @"E:\Music\Misc Other\Game\Last Remnant\(Tsuyoshi Sekito) - Last Remnant - The Gates of Hell.mp3", new EmotionSpaceDTO(.8, .3));
        //static public Tuple<string, EmotionSpaceDTO> tuple4 = new Tuple<string, EmotionSpaceDTO>(
        //    @"E:\Music\Misc Other\Game\Other\(Lifeformed) - Dustforce - Cider Time.mp3", new EmotionSpaceDTO(.4, .6));
        //static public Tuple<string, EmotionSpaceDTO> tuple5 = new Tuple<string, EmotionSpaceDTO>(
        //    @"E:\Music\Misc Other\Game\Other\(Opoona) - The Village Without Memories.mp3", new EmotionSpaceDTO(.3, .7));
        //static public Tuple<string, EmotionSpaceDTO> tuple6 = new Tuple<string, EmotionSpaceDTO>(
        //    @"E:\Music\Misc Other\Game\Final Fantasy\Final Fantasy XI\From Astral to Umbral\(FF XI) - Thunder Rolls.mp3", new EmotionSpaceDTO(.5, .6));

    }
}
