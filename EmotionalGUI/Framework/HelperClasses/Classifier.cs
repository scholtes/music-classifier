using System.Diagnostics;
using System.Web.Script.Serialization;

namespace Framework
{
    class Classifier : IClassifier
    {
        public string classifySongs(string[] songpaths)
        {
            JsonDTO json = new JsonDTO();
            json.ClassifierResults = new System.Collections.Generic.List<ClassifierResult>();
            foreach(string song in songpaths)
            {
                string classifierJson = classify(song);
                JsonDTO dto = JsonDTOMapper.getJsonDTO(classifierJson);
                json.ClassifierResults.Add(dto.ClassifierResults[0]);
            }
            string answer = new JavaScriptSerializer().Serialize(json);
            return answer;
        }

        private string classify(string songpath)
        {
            string output = null;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"D:\Desktop\emotify\emotify.exe";
            startInfo.Arguments += "\"" + songpath + "\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            //startInfo.RedirectStandardError = true;
            var proc = new Process();
            proc.StartInfo = startInfo;
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                output = proc.StandardOutput.ReadLine();
            }

            return output;
        }
    }
}
