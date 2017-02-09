
namespace Framework
{
    public static class Database
    {
        public static void addResults(JsonDTO json, IDatabase database)
        {
            foreach (ClassifierResult entry in json.ClassifierResults)
            {
                EmotionSpaceDTO dto = new EmotionSpaceDTO()
                {
                    Energy = entry.song.energy,
                    Positivity = entry.song.positivity
                };
                database.addSongToDatabase(entry.song.title, dto);
            }
        }
    }
}
