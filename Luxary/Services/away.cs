using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Luxary.Service
{
    public class awaydata
    {
        string _awayUserFile = "awayusers.txt";

        public void AwayData()
        {
            FileCheck();
        }

        public void setAwayUser(Away awayInfo)
        {
            var awayUserList = new List<Away>();
            string[] fileImport = File.ReadAllLines(_awayUserFile);

            foreach (string line in fileImport)
            {
                //Create empty Away object
                var away = new Away();
                //Split the line in text file based on commands, keeping quotes
                var splitLine = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                //Remove quotes when actually using data                
                away.User = splitLine[0].Replace("\"", "");
                away.Message = splitLine[1].Replace("\"", "");
                away.Status = bool.Parse(splitLine[2].Replace("\"", ""));
                away.AwayTime = DateTime.Parse(splitLine[3].Replace("\"", ""));

                awayUserList.Add(away);
            }

            var userInList = awayUserList.Where(u => u.User == awayInfo.User).FirstOrDefault();
            if (userInList != null)
            {
                awayUserList.Where(u => u.User == userInList.User).FirstOrDefault().AwayTime = DateTime.Now;
                awayUserList.Where(u => u.User == userInList.User).FirstOrDefault().Message = awayInfo.Message;
                awayUserList.Where(u => u.User == userInList.User).FirstOrDefault().Status = awayInfo.Status;

            }
            else
            {
                awayUserList.Add(awayInfo);
            }

            var fileData = new List<String>();
            foreach (var awayUser in awayUserList)
            {
                string fileLine = "\"" + awayUser.User + "\"," + "\"" + awayUser.Message + "\"," + "\"" + awayUser.Status + "\"," + "\"" + awayUser.AwayTime + "\"";
                fileData.Add(fileLine);
            }

            File.WriteAllLines(_awayUserFile, fileData);
        }

        public Away getAwayUser(string discordUserName)
        {
            var userList = File.ReadAllLines(_awayUserFile);
            var away = new Away();
            if (string.IsNullOrEmpty(userList.ToString()))
            {
                return away;
            }
            else
            {
                foreach (string awayInfo in userList)
                {
                    var splitLine = Regex.Split(awayInfo, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    if (splitLine[0].Replace("\"", "") == discordUserName)
                    {
                        away.User = splitLine[0].Replace("\"", "");
                        away.Message = splitLine[1].Replace("\"", "");
                        away.Status = bool.Parse(splitLine[2].Replace("\"", ""));
                        away.AwayTime = DateTime.Parse(splitLine[3].Replace("\"", ""));
                        break;
                    }
                }
                return away;
            }
        }

        public void FileCheck()
        {
            if (!File.Exists("awayusers.txt"))
            {
                File.Create("awayusers.txt").Flush();
                Console.WriteLine("file created for away system! (awayusers.txt)");
            }
        }
    }
}
