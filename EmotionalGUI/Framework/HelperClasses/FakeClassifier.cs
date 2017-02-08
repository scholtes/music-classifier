using System;
using System.Runtime.InteropServices;

namespace Framework
{
    public class FakeClassifier : IClassifier
    {
        public string classifySongs(string[] songs)
        {

            //return ("{\"ClassifierResults\": [{\"song\": {\"title\": \"let it go\",\"energy\": 0.7,\"positivity\": 0.9}}, {\"song\": {\"title\": \"Cooking by the book\",\"energy\": 0.9,\"positivity\": 0.9}}]}");
            return ("{\"ClassifierResults\": [{\"song\": {  \"title\": \"E:\\\\Music\\\\Flyleaf\\\\(Flyleaf) - Alternative - All Around Me.mp3\",\"energy\": 0.7,\"positivity\": 0.7}}, {\"song\": {\"title\": \"E:\\\\Music\\\\Sixpence\\\\(Sixpence None The Richer) - The Best Of - Kiss Me.mp3\",\"energy\": 0.4,\"positivity\": 0.6}}, {\"song\": {\"title\": \"E:\\\\Music\\\\Cranberries\\\\(The Cranberries) - Stars - Dreams.mp3\",\"energy\": 0.4,\"positivity\": 0.5}}, {\"song\": {\"title\": \"E:\\\\Music\\\\Breaking Benjamin\\\\(Breaking Benjamin) - Phobia - Breath.mp3\",\"energy\": 0.7,\"positivity\": 0.5}}, {\"song\": {\"title\": \"E:\\\\Music\\\\Misc Other\\\\Game\\\\Final Fantasy\\\\Final Fantasy X\\\\(Nobuo Uematsu) - Final Fantasy X - Someday The Dream Will End.mp3\",\"energy\": 0.4,\"positivity\": 0.2}}, {\"song\": {\"title\": \"E:\\\\Music\\\\Misc Other\\\\Game\\\\Chrono Cross\\\\(Yasunori Mitsuda) - Chrono Cross - Garden of the Gods.mp3\",\"energy\": 0.2,\"positivity\": 0.6}}, {\"song\": {\"title\": \"E:\\\\Music\\\\Misc Other\\\\Game\\\\Metal Gear\\\\Metal Gear Revengeance\\\\(Metal Gear Revengeance) - It Has To Be This Way (Original).mp3\",\"energy\": 0.9,\"positivity\": 0.4}}]}");
        }
    }
}
