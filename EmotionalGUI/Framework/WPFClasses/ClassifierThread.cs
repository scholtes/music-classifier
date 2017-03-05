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
        string[] songs = null;
        Thread backgroundworker;
        private string positiveClassificationMethod = "positivity_gaussian.svm";
        private string energyClassificationMethod = "energy_gaussian.svm";

        public ClassifierThread(string[] songs)
        {
            this.songs = songs;
            backgroundworker = new Thread(ClassifySongsTogether);
            backgroundworker.Start();
        }


        public void ClassifySongsIndividually()
        {
            string positivitySvmPath = System.IO.Path.Combine(Classifier.ExecutableInformation.getModelsDir(), positiveClassificationMethod);
            string energySvmPath = System.IO.Path.Combine(Classifier.ExecutableInformation.getModelsDir(), energyClassificationMethod);
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

        public void ClassifySongsTogether()
        {
            string positivitySvmPath = System.IO.Path.Combine(Classifier.ExecutableInformation.getModelsDir(), positiveClassificationMethod);
            string energySvmPath = System.IO.Path.Combine(Classifier.ExecutableInformation.getModelsDir(), energyClassificationMethod);
            Classifier.SupportVectorMachine svm = new Classifier.SupportVectorMachine(positivitySvmPath, energySvmPath);
            List<string> unclassifiedSongs = new List<string>();
            foreach (string song in songs)
            {
                if (!ServerDatabase.Instance.DoesSongExist(song))
                {
                    unclassifiedSongs.Add(song);
                }
            }
            //Don't try to reclassify the same song. Waste of time.
            string output = svm.Classify(unclassifiedSongs);
            foreach (ClassifierResult result in JsonDTOMapper.getJsonDTO(output).ClassifierResults)
            {
                Song classifiedSong = result.song;
                EmotionSpaceDTO point = new EmotionSpaceDTO(classifiedSong.energy, classifiedSong.positivity);
                ServerDatabase.Instance.addSongToDatabase(classifiedSong.title, point);
            }

        }
    }
}
