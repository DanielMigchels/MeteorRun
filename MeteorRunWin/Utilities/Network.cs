using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.Utilities
{
    public class Network
    {
        public static List<string> GetScore()
        {
            List<string> scores = new List<string>();

            try
            {
                using (var client = new WebClient())
                {
                    var json = client.DownloadString("http://i387743.hera.fhict.nl/score/highscorecopy.php");
                    dynamic serializer = JsonConvert.DeserializeObject(json);

                    int i = 1;
                    foreach (dynamic obj in serializer.data)
                    {
                        string score = obj.Score.ToString();
                        string name = obj.Name.ToString();
                        scores.Add(i + ". " + name + " " + score);
                        i++;
                    }

                }
            }
            catch { }

            return scores;
        }

        public static void PutScore(string name, int score)
        {
            try
            {
                using (var client = new WebClient())
                {
                    string ciphername = Encryption.Encrypt(name.ToString());
                    string cipherscore = Encryption.Encrypt(score.ToString());
                    string timestamp = Encryption.Encrypt((DateTimeOffset.Now.UtcTicks - 621355968000000000).ToString().Substring(0, 10));
                    string crc = CRC.Calculate(name.ToString() + score.ToString()).ToString();
                    
                    string url = "http://i387743.hera.fhict.nl/score/highscorecopy.php?Score=" + cipherscore + "&Name=" + ciphername + "&Time=" + timestamp + "&crc=" + crc;
                    var json = client.DownloadString(url);
                }
            }
            catch { }
        }
    }
}
