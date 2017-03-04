using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Framework
{
    public class ClassifierThread
    {
        public ClassifierThread(string[] songs)
        {
            this.songs = songs;
            backgroundworker = new Thread(Classify);
            backgroundworker.Start();
        }


        public void Classify()
        {
            string positivitySvmPath = System.IO.Path.Combine(Classifier.ExecutableInformation.getModelsDir(), "positivity_gaussian.svm");
            string energySvmPath = System.IO.Path.Combine(Classifier.ExecutableInformation.getModelsDir(), "energy_gaussian.svm");
            Classifier.SupportVectorMachine svm = new Classifier.SupportVectorMachine(positivitySvmPath, energySvmPath);
            foreach (string song in songs)
            {
                if(!ServerDatabase.Instance.DoesSongExist(song))
                {
                    //Don't try to reclassify the same song. Waste of time.
                   string output = svm.Classify(song);
                   Song classifiedSong = JsonDTOMapper.getJsonDTO(output).ClassifierResults[0].song;
                   EmotionSpaceDTO point = new EmotionSpaceDTO(classifiedSong.energy, classifiedSong.positivity);
                   ServerDatabase.Instance.addSongToDatabase(song, point);
                }
            }
        }


        string[] songs = null;
        Thread backgroundworker;
    }
}
