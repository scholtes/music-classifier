
namespace Framework
{
    interface IClassifier
    {
        /// <summary>
        /// Calls the clasifier
        /// </summary>
        /// <param name="songs">Fully qualified file paths to mp3 files</param>
        /// <returns>A large Json string</returns>
        string classifySongs(string[] songs);
    }
}
