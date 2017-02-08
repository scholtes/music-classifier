using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Framework
{
    public class ServerDatabase : IDatabase
    {
        private struct SongDistance
        {
            public string song;
            public double distance;
        }



        /// <summary>
        /// Make sure that the database is private and only one instance ever exists.
        /// </summary>
        private ServerDatabase()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            filePath = Path.Combine(path, "library.db");
            if (!File.Exists(filePath) || !VerifyDatabase())
            {
                CreateDatabase();
            }
        }


        private void CreateDatabase()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            SQLiteConnection.CreateFile(filePath);
            using (SQLiteConnection connection = Connect())
            {
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"CREATE TABLE SONGS
                                                (
                                                Path TEXT,
                                                Energy Real (0,1), 
                                                Postitvity Real (0,1),
                                                PRIMARY KEY (Path)
                                                );";
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
        }

        public bool VerifyDatabase()
        {
            bool wasValid = true;
            try
            {
                List<string> songsToDelete = new List<string>();
                Action<SQLiteDataReader> findSongsToDelete = (rdr) =>
                {
                    while (rdr.Read())
                    {
                        if (!File.Exists(rdr.GetString(0)))
                        {
                            songsToDelete.Add(rdr.GetString(0));
                        }
                    }
                };
                getAllSongs(findSongsToDelete);

                foreach (string song in songsToDelete)
                {
                    removeSongFromDatabase(song);
                }
            }
            catch (Exception)
            {
                wasValid = false;
            }
            return wasValid;
        }


        public string[] getSongs(int numberOfSongs, EmotionSpaceDTO emotionSpaceDTO)
        {
            Dictionary<string, EmotionSpaceDTO> songs = new Dictionary<string, EmotionSpaceDTO>();
            Action<SQLiteDataReader> addSongsToDict = (rdr) =>
            {
                while (rdr.Read())
                {
                    EmotionSpaceDTO point = new EmotionSpaceDTO();
                    point.Energy = rdr.GetDouble(1);
                    point.Positivity = rdr.GetDouble(2);
                    songs[rdr.GetString(0)] = point;
                }
            };
            getAllSongs(addSongsToDict);
            if (songs.Count < numberOfSongs)
            {
                numberOfSongs = songs.Count;
            }
            List<SongDistance> closeSongs = new List<SongDistance>(numberOfSongs);


            double farPoint = double.MinValue;
            foreach (KeyValuePair<string, EmotionSpaceDTO> kvp in songs)
            {
                SongDistance val = new SongDistance();
                val.distance = getDistance(emotionSpaceDTO, kvp.Value);
                val.song = kvp.Key;
                if (closeSongs.Count < numberOfSongs)
                {
                    closeSongs.Add(val);
                    if (farPoint < val.distance)
                    {
                        farPoint = val.distance;
                    }
                }
                else
                {
                    if (val.distance < farPoint)
                    {
                        closeSongs.Add(val);
                        closeSongs.Sort(compareDistance);
                        closeSongs.RemoveAt(closeSongs.Count - 1);
                    }
                }
            }

            List<string> ret = new List<string>();
            foreach (SongDistance finalVal in closeSongs)
            {
                ret.Add(finalVal.song);
            }
            return ret.ToArray();
        }


        private SQLiteConnection Connect()
        {
            try
            {
                SQLiteConnectionStringBuilder sb = new SQLiteConnectionStringBuilder();
                sb.DataSource = filePath;
                SQLiteConnection connection = new SQLiteConnection(sb.ConnectionString);
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Remove a song from the database
        /// </summary>
        /// <param name="songpath">A fully qualified path to an audio file</param>
        public void removeSongFromDatabase(string songpath)
        {
            using (SQLiteConnection connection = Connect())
            {
                using (SQLiteTransaction tranaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = tranaction;
                        command.CommandText = string.Format("DELETE FROM SONGS WHERE Path = \"{0}\";", songpath);
                        command.ExecuteNonQuery();
                    }
                    tranaction.Commit();
                }
            }
        }

        private void getAllSongs(Action<SQLiteDataReader> readerAction)
        {
            SQLiteDataReader reader = null;
            using (SQLiteConnection connection = Connect())
            {
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    SQLiteCommand command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = @"SELECT Path, Energy, Postitvity FROM SONGS;";
                    reader = command.ExecuteReader();
                    readerAction.Invoke(reader);
                }
            }
        }

        private double getDistance(EmotionSpaceDTO pointA, EmotionSpaceDTO pointB)
        {
            return Math.Sqrt(Math.Pow(pointA.Energy - pointB.Energy, 2) + Math.Pow(pointA.Positivity - pointB.Positivity, 2));
        }

        /// <summary>
        /// Adds a song to the database
        /// </summary>
        /// <param name="songpath">The location of the song</param>
        /// <param name="emotionSpaceDTO">The coordinates in the emotionspace</param>
        public void addSongToDatabase(string songpath, EmotionSpaceDTO emotionSpaceDTO)
        {
            using (SQLiteConnection connection = Connect())
            {
                using (SQLiteTransaction tranaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = tranaction;
                        command.CommandText = string.Format("INSERT INTO SONGS (Path, Energy, Postitvity) VALUES (\"{0}\",{1},{2})", songpath, emotionSpaceDTO.Energy, emotionSpaceDTO.Positivity);
                        command.ExecuteNonQuery();
                    }
                    tranaction.Commit();
                }
            }
        }

        private int compareDistance(SongDistance a, SongDistance b)
        {
            return a.distance.CompareTo(b.distance);
        }


        private readonly string filePath;
        public static ServerDatabase Instance = new ServerDatabase();
    }
}
