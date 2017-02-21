﻿using System.Collections.Generic;
using System;

namespace Classifier
{
    public abstract class BaseClassifierType
    {
        private List<SongDataDTO> songsAndFeatures;

        protected void ConvertToWav(string[] filePaths)
        {
            FFMpeg.ffmpegConversion(filePaths);
        }

        protected string ExtractFeaturesToFile(string mkcollectionPath)
        {
            return BExtract.featureExtraction(mkcollectionPath);
        }

        protected void LoadFeaturesFromFile(string arffFilePath)
        {
            songsAndFeatures = ArffParser.parseArff(arffFilePath);
        }

        public abstract void LoadClassifier();

        public abstract void SaveClassifier();
    }
}
