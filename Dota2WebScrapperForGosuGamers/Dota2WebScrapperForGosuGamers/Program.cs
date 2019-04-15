using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Dota2WebScrapperForGosuGamers
{
    class Program
    {
        //List that storages all of the teams obtained from GosuGamers when read from the file GosuGamersFullTeamsList
        public static List<string> teams = new List<string>();

        static string url,
            main_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\",
            dota2GamesInformation = "Dota2GamesInformation",
            teamsFolders = "TeamsFolders";

        private static int stopDate_year,
                           stopDate_month,
                           stopDate_day,
                           game_year,
                           game_month,
                           game_day;

        static void Main(string[] args)
        {
            CreateMainFolders_Files();

            GetTeams_GosuGamersRankings();

            UpdateDateStop();
        }

        //Create the bare minimum main folders that we need.
        public static void CreateMainFolders_Files()
        {
            CreateFolder_Dota2GamesInformation();
            CreateFolder_Files_Date();
            CreateFolder_TeamsFolders();
            CreateFolder_File_TeamsClassification();
            CreateFolder_Files_OnlyFirstTeam();
            CreateFolder_Files_NeuralNetworksFirstTeam();
        }

        /* Create the main folder in which we will store all of the information obtained from GosuGamers.com"
       */
        static void CreateFolder_Dota2GamesInformation()
        {
            string dota2GamesInformation = "Dota2GamesInformation",
                dota2GamesInformation_FolderPath = main_path + dota2GamesInformation;

            if (!Directory.Exists(dota2GamesInformation_FolderPath))
            {
                Directory.CreateDirectory(dota2GamesInformation_FolderPath);
            }
        }

        /*Create a folder in  "C...\Dota2GamesInformation" in which we store EVERY date that we have searched for teams
         * and we also store the date in which the search will stop until it starts again.
        */
        static void CreateFolder_Files_Date()
        {
            string dateFolder = "Date",
                dateDatabase = "DateDatabase",
                dateStop = "DateStop";

            string dateFolderPath = main_path + dota2GamesInformation + @"\" + dateFolder,
                dateDatabaseFilePath = dateFolderPath + @"\" + dateDatabase + ".txt",
                dateStopFilePath = dateFolderPath + @"\" + dateStop + ".txt";
         

            if (!Directory.Exists(dateFolderPath))
            {
                Directory.CreateDirectory(dateFolderPath);
            }

            if (!File.Exists(dateDatabaseFilePath))
            {
                File.Create(dateDatabaseFilePath).Close();
            }

            //Random date set as default to when the search of Matches by Teams will stop
            if (!File.Exists(dateStopFilePath))
            {
                File.Create(dateStopFilePath).Close();

                StreamWriter swFirstDate = new StreamWriter(dateStopFilePath, true);

                swFirstDate.WriteLine("year: " + 2015 + " month: " + 12 + " day: " + 15);
                swFirstDate.Close();

                ReadDateStop();
                UpdateDateDatabase();
            }

            ReadDateStop();
        }

        /*Create a folder in  "C...\Dota2GamesInformation" in which we store EVERY Team folder that we obtain
        * from GosuGamers.com"
       */
        static void CreateFolder_TeamsFolders()
        {
            string teamsFolders = "TeamsFolders",
                teamsFoldersPath = main_path + dota2GamesInformation + @"\" + teamsFolders;

            if (!Directory.Exists(teamsFoldersPath))
            {
                Directory.CreateDirectory(teamsFoldersPath);
            }
        }

        /*Create the folder and file in  "C...\Dota2GamesInformation" in which we store
         *our own way of classification for the Teams that we obtained from GosuGamers.com"
        */
        static void CreateFolder_File_TeamsClassification()
        {
            string teamsClassification = "TeamsClassification",
                teamsClassificationFolderPath = main_path + dota2GamesInformation + @"\"
                                                + teamsClassification,
               filePath = teamsClassificationFolderPath + @"\" + teamsClassification + ".txt";

            if (!Directory.Exists(teamsClassificationFolderPath))
            {
                Directory.CreateDirectory(teamsClassificationFolderPath);
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        //Create the folder just for the first team information.
        static void CreateFolder_Files_OnlyFirstTeam()
        {
            string teamFoldersName = main_path + dota2GamesInformation + @"\" + teamsFolders + @"\",
                allTeams_OnlyFirstTeam = "AllTeams_OnlyFirstTeam",
                allTeams_OnlyFirstTeamFolderPath = teamFoldersName + allTeams_OnlyFirstTeam,
                filePathData = allTeams_OnlyFirstTeamFolderPath + @"\" + allTeams_OnlyFirstTeam + "Data.txt",
                filePathResult = allTeams_OnlyFirstTeamFolderPath + @"\" + allTeams_OnlyFirstTeam + "Result.txt",
                filePathDataWithNames = allTeams_OnlyFirstTeamFolderPath + @"\" + allTeams_OnlyFirstTeam + "DataWithNames.txt",
                filePathResultWithNames = allTeams_OnlyFirstTeamFolderPath + @"\" + allTeams_OnlyFirstTeam + "ResultWithNames.txt";

            if (!Directory.Exists(allTeams_OnlyFirstTeamFolderPath)) //Si no existe el directorio
            {
                Directory.CreateDirectory(allTeams_OnlyFirstTeamFolderPath); //Crea el folder con el camino dado.
            }

            if (!File.Exists(filePathData))
            {
                File.Create(filePathData).Close();
            }

            if (!File.Exists(filePathResult))
            {
                File.Create(filePathResult).Close();
            }

            if (!File.Exists(filePathDataWithNames))
            {
                File.Create(filePathDataWithNames).Close();
            }

            if (!File.Exists(filePathResultWithNames))
            {
                File.Create(filePathResultWithNames).Close();
            }
        }

        //Create the folder for the information of the first team used on Neural Networks.
        static void CreateFolder_Files_NeuralNetworksFirstTeam()
        {
            string teamFoldersName = main_path + dota2GamesInformation + @"\" + teamsFolders + @"\",
                allTeams_NeuralNetworksFirstTeam = "AllTeams_NeuralNetworksFirstTeam",
                allTeams_NeuralNetworksFirstTeamPath = teamFoldersName + allTeams_NeuralNetworksFirstTeam,
                filePathData = allTeams_NeuralNetworksFirstTeamPath + @"\" + allTeams_NeuralNetworksFirstTeam + "Data.txt",
                filePathDataWithNames = allTeams_NeuralNetworksFirstTeamPath + @"\" + allTeams_NeuralNetworksFirstTeam + "DataWithNames.txt";

            if (!Directory.Exists(allTeams_NeuralNetworksFirstTeamPath))
            {
                Directory.CreateDirectory(allTeams_NeuralNetworksFirstTeamPath);
            }

            if (!File.Exists(filePathData))
            {
                File.Create(filePathData).Close();
            }

            if (!File.Exists(filePathDataWithNames))
            {
                File.Create(filePathDataWithNames).Close();
            }
        }

        //Read the Date in the file DateStop which is the date we are stoping our search for teams.
        static void ReadDateStop()
        {
            string dateFolderName = main_path + dota2GamesInformation + @"\" + "Date",
            dateStop = "DateStop",
            filePathStop = dateFolderName + @"\" + dateStop + ".txt";

            StreamReader sr = new StreamReader(filePathStop);

            string read;
            if ((read = sr.ReadLine()) != null)
            {
                int yearFirst = read.IndexOf("year: ") + 6;
                int yearSecond = read.IndexOf(" ", yearFirst);

                string file_year = read.Substring(yearFirst, yearSecond - yearFirst);

                stopDate_year = Int32.Parse(file_year);

                int monthFirst = read.IndexOf("month: ") + 7,
                    monthSecond = read.IndexOf(" ", monthFirst);

                string file_month = read.Substring(monthFirst, monthSecond - monthFirst);

                stopDate_month = Int32.Parse(file_month);

                int dayFirst = read.IndexOf("day: ") + 5;
                string file_day = read.Substring(dayFirst);

                stopDate_day = Int32.Parse(file_day);
            }
            sr.Close();
        }

        //Update the database so you can keep track of the many dates you have done a search.
        static void UpdateDateDatabase()
        {
            string dateFolderName = main_path + dota2GamesInformation + @"\" + "Date",
                datesDatabase = "DateDatabase",
                filePathDatabase = dateFolderName + @"\" + datesDatabase + ".txt";

            StreamWriter sw = new StreamWriter(filePathDatabase, true);

            sw.WriteLine("year: " + stopDate_year + " month: " + stopDate_month +
            " day: " + stopDate_day);

            sw.Close();
        }

        //Update the DateStop file to know when the next stop date is going to be.
        static void UpdateDateStop()
        {
            string dateFolderName = main_path + dota2GamesInformation + @"\" + "Date",
            dateStop = "DateStop",
            filePathStop = dateFolderName + @"\" + dateStop + ".txt";

            stopDate_year = System.DateTime.Now.Year;
            stopDate_month = System.DateTime.Now.Month;
            stopDate_day = System.DateTime.Now.Day;

            StreamWriter sw = new StreamWriter(filePathStop, false);

            sw.WriteLine("year: " + stopDate_year + " month: " + stopDate_month +
           " day: " + stopDate_day);

            sw.Close();

            UpdateDateDatabase();
        }

        /* Method that starts the process of writing EVERY Team name to a file called TeamsClassification.txt
        *  that was found on the Rankings section from "GosuGamers.com/Dota2"
        */
        private static void GetTeams_GosuGamersRankings()
        {
            url = "http://www.gosugamers.net/dota2/rankings";

            List<string> pagesObtained = GG_Dota_URLsOfPages_Ranking(ObtainWebPageHTML(url));

            for (int i = 0; i < pagesObtained.Count; i++) //for para utilizar las paginas obtenidas.
            {
                WriteFullListOfTeamsToFile(pagesObtained[i]); //Metodo de escritura en el documento.
            }
        }

        /* Method to obtain the links for all the pages that GosuGamers has for all the 
         * Teams that are in their Rankings section. "Gosugamers.com/Dota2/Rankings"
        */
        private static List<string> GG_Dota_URLsOfPages_Ranking(string pagesHTML)
        {
            int pagesIndex = pagesHTML.IndexOf("<div class=\"pages\">");

            int pagesLastIndex = pagesHTML.IndexOf("<span>Next</span>", pagesIndex);

            string cutDown = pagesHTML.Substring(pagesIndex, pagesLastIndex - pagesIndex);

            List<string> links = new List<string>();

            while (cutDown.IndexOf("<a href=") != -1)
            {
                int linkIndex = cutDown.IndexOf("<a href=") + 9,
                    linkLastIndex = cutDown.IndexOf("\"", linkIndex);

                string link = cutDown.Substring(linkIndex, linkLastIndex - linkIndex),
                    fullLink = "http://gosugamers.net" + link;

                links.Add(fullLink);
                cutDown = cutDown.Replace("<a href=" + @"""" + link, "");
            }
            return links;
        }

        /* Method to obtain the HTML code of a web page by issuing a request to it,
        * obtaining a response and then reading the html code with a stream reader,
        * finalizing in the storage of said code on a string, that is then returned.
         */
        public static string ObtainWebPageHTML(string link)
        {
            AddExtensionHTTP();

            string html_webpage;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(link);

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());

            html_webpage = streamReader.ReadToEnd();

            streamReader.Close();
            webResponse.Close();

            return html_webpage;
        }

        //If by any chance you change the url and you don't add "http://" to it ,this method will do it for you.
        private static void AddExtensionHTTP()
        {
            if (!url.StartsWith("http://"))
            {
                url = "http://" + url;
            }
        }

        /* Method to write all of the Teams found in Rankings on "Gosugamers.com/Dota2/Rankings"
        *  on the file "C:...\Dota2GamesInformation\TeamsClassification\TeamsClassification.txt 
        */
        private static void WriteFullListOfTeamsToFile(string link)
        {
            //"Catches" what's inside <span>(Team)</span> which is actually the team name for every HTML page that is in Rankings
            MatchCollection teamMatchCollection = Regex.Matches(HTML_Of_TeamsRanking(link), @"<span>\s*(.+?)\s*</span>", RegexOptions.Singleline);

            //Won't erase previous lines in saved file due to context true.
            StreamWriter sw = new StreamWriter(@"C:\Users\Alejandro\Documents\Dota2GamesInformation\TeamsClassification\TeamsClassification.txt", true);

            foreach (Match teamMatch in teamMatchCollection)
            {
                string team = teamMatch.Groups[1].Value;
                sw.WriteLine(team);
                CreateFolders_Files_OfTeams(team);
            }
            sw.Close();
        }

        /*Method to obtain the HTML of EVERY page that we previously obtained its url and cut
         * the unnecessary parts of it, to make searching for teams names easier.
        */
        private static string HTML_Of_TeamsRanking(string link)
        {
            string teamsHTML = ObtainWebPageHTML(link);

            int startIndex = teamsHTML.IndexOf("main no-game"),
                lastIndex = teamsHTML.IndexOf("<td class=\"pagination\" colspan=\"4\">", startIndex);

            teamsHTML = teamsHTML.Substring(startIndex, lastIndex - startIndex);

            return teamsHTML;
        }

        /*Create a folder in "C:...\Dota2GamesInformation\TeamsFolders" for EVERY team 
         * that is send to the method when called
        */
        private static void CreateFolders_Files_OfTeams(string team)
        {
            string teamFoldersPath = main_path + dota2GamesInformation + @"\" + "TeamsFolders" + @"\",
                teamFolderPath = teamFoldersPath + team,
                teamFilePathData = teamFolderPath + @"\" + team + "Data.txt",
                teamFilePathResult = teamFolderPath + @"\" + team + "Result.txt",
                teamFilePathDataWithNames = teamFolderPath + @"\" + team + "DataWithNames.txt",
                teamFilePathResultWithNames = teamFolderPath + @"\" + team + "ResultWithNames.txt";

            if (!Directory.Exists(teamFolderPath))
            {
                Directory.CreateDirectory(teamFolderPath);
            }

            if (!File.Exists(teamFilePathData))
            {

                File.Create(teamFilePathData).Close();
            }

            if (!File.Exists(teamFilePathResult))
            {

                File.Create(teamFilePathResult).Close();
            }

            if (!File.Exists(teamFilePathDataWithNames))
            {

                File.Create(teamFilePathDataWithNames).Close();
            }

            if (!File.Exists(teamFilePathResultWithNames))
            {

                File.Create(teamFilePathResultWithNames).Close();
            }
        }

        /*Obtain the information of current matches that were played from the database 
         * that GosuGamers.com has on their bet section, which is a complete section 
         * dedicated to the latest games played 
        */
        private static void GetTeamMatchesInformation()
        {
            url = "http://www.gosugamers.net/dota2/gosubet";

            int currentPage = 1;

            TeamsGameLinks(ObtainWebPageHTML(url), currentPage);
        }

        /*Method to obtain the links of the games played in the bets section from GosuGamers.com
        */
        static void TeamsGameLinks(string currentPageHTML, int pageIndex)
        {
            pageIndex += 1;

            int startIndex = currentPageHTML.IndexOf("Dota 2 Recent Results"),
                lastIndex = currentPageHTML.IndexOf("</tbody>", startIndex);

            string cut = currentPageHTML.Substring(startIndex, lastIndex - startIndex);

            while (cut.IndexOf("href=\"") != -1)
            {
                int linkIndex = cut.IndexOf("href=\"") + 6,
                    linkLastIndex = cut.IndexOf("\"", linkIndex);

                string link = cut.Substring(linkIndex, linkLastIndex - linkIndex),
                    fullLink = "http://gosugamers.net" + link;

                cut = cut.Substring(linkLastIndex);

                if (fullLink.Contains("matches"))
                {
                    GetGameInfo(ObtainWebPageHTML(fullLink));
                }
            }

            if (game_year <= stopDate_year && game_month == stopDate_month)
            {
                if (game_day > stopDate_day)
                {
                    GetNextPage(currentPageHTML, pageIndex);
                }
            }
            else
            {
                GetNextPage(currentPageHTML, pageIndex);
            }
        }

        //Get the next page from the bets section
        static void GetNextPage(string currentPageHTML, int pageIndex)
        {
            string nextPageIndex = pageIndex.ToString(),
                nextPage = " ";

            int pageStartIndex = currentPageHTML.IndexOf("Dota 2 Recent Results"),
                pageMiddleIndex = currentPageHTML.IndexOf("<div class=\"pages\">", pageStartIndex) + 19;

            string cutMiddle = currentPageHTML.Substring(pageMiddleIndex);

            int pageLastIndex = cutMiddle.IndexOf("</div>");

            string cutDown = cutMiddle.Substring(0, pageLastIndex);

            while (cutDown.IndexOf("href=\"") != -1)
            {
                int linkIndex = cutDown.IndexOf("href=\"") + 6,
                    linkLastIndex = cutDown.IndexOf("\"", linkIndex);

                string link = cutDown.Substring(linkIndex, linkLastIndex - linkIndex),
                    fullLink = "http://gosugamers.net" + link;

                cutDown = cutDown.Substring(linkLastIndex);

                int findPage = link.IndexOf("page");

                string page = "";

                if (findPage != -1)
                {
                    page = link.Substring(findPage);
                }
                if (page.Contains(nextPageIndex))
                {
                    nextPage = fullLink;
                    cutDown = " ";
                }
            }

            if (nextPage.Contains("http://gosugamers.net/dota2/gosubet"))
            {
                string nextPageHMTL = ObtainWebPageHTML(nextPage);
                TeamsGameLinks(nextPageHMTL, pageIndex);
            }
            else
            {
                Console.WriteLine("Unable to find a game link");
            }
        }

        //Get the Game information of the match played
        static void GetGameInfo(string gameHTML)
        {
            int year = System.DateTime.Now.Year,
                cut = gameHTML.IndexOf("All Dota 2 matches");

            if (cut != -1)
            {
                string remove = gameHTML.Substring(cut);

                int vsTop = remove.IndexOf("<label class=\'opponents-h1-content\'>") + 61;
                string getTop = remove.Substring(vsTop);

                int indexFT = getTop.IndexOf(" vs");
                string firstTeam = getTop.Substring(0, indexFT),
                    cutFT = getTop.Substring(indexFT);

                int indexST = cutFT.IndexOf("vs ") + 3,
                    indexSTS = cutFT.IndexOf("  ") - 4;
                string secondTeam = cutFT.Substring(indexST, indexSTS);

                if (firstTeam.Contains(":"))
                {
                    int cutMistakeFT = firstTeam.IndexOf(":");

                    firstTeam = firstTeam.Substring(0, cutMistakeFT);
                }
                else if (secondTeam.Contains(":"))
                {
                    int cutMistakeST = secondTeam.IndexOf(":");

                    secondTeam = secondTeam.Substring(0, cutMistakeST);
                }

                int bottScore = cutFT.IndexOf("datetime");
                string scoreChart = cutFT.Substring(0, bottScore);

                int lost = scoreChart.IndexOf("loser\">") + 7,
                    won = scoreChart.IndexOf("winner\">") + 8,
                    draw;

                string getScoreFT,
                    getScoreST;

                if (lost > won)
                {
                    getScoreFT = scoreChart.Substring(won, 1);
                    getScoreST = scoreChart.Substring(lost, 1);
                }
                else
                {
                    getScoreFT = scoreChart.Substring(lost, 1);
                    getScoreST = scoreChart.Substring(won, 1);

                }

                int scoreFT = 0,
                    scoreST = 0;

                double month = 12.0,
                    day = 12.0,
                    hour = 12.0,
                    minutes = 12.0;

                int findDate = remove.IndexOf("datetime\">") + 127,
                    bottDate = remove.IndexOf("</p>", findDate);

                string getDate = remove.Substring(findDate, bottDate - findDate);

                int reachMonth = getDate.IndexOf(" ");

                string getMonth = getDate.Substring(0, reachMonth),
                    getDay = getDate.Substring(reachMonth + 1, 2);

                int reachNameDay = getDate.IndexOf(",");
                string tryReachHour = getDate.Substring(reachNameDay + 2);

                int reachHour = tryReachHour.IndexOf(" ") + 1;
                string getHour = tryReachHour.Substring(reachHour, 2);
                int reachSeparationHours = tryReachHour.IndexOf(":");

                string getMinutes = tryReachHour.Substring(reachSeparationHours + 1, 2);

                day = Double.Parse(getDay);
                game_day = Convert.ToInt32(day);

                string[] months = {"January", "February", "March", "April", "May",
                "June", "July", "August", "September", "October", "November", "December"};

                try
                {
                    scoreFT = Int32.Parse(getScoreFT);
                    scoreST = Int32.Parse(getScoreST);
                }
                catch (FormatException e)
                {
                    draw = scoreChart.IndexOf("draw\">") + 6;

                    getScoreFT = scoreChart.Substring(draw, 1);
                    getScoreST = getScoreFT;

                    scoreFT = Int32.Parse(getScoreFT);
                    scoreST = Int32.Parse(getScoreST);
                }

                for (int j = 0; j < months.Length; j++)
                {
                    if (getMonth.Equals(months[j]))
                    {
                        try
                        {
                            //(j + 1) since in the array January is months[0], so that we have the number of month January = 1,...,May = 5, June = 6, etc.
                            month = (j + 1);
                            hour = Double.Parse(getHour);
                            minutes = Double.Parse(getMinutes);

                            game_month = Convert.ToInt32(month);

                            j += months.Length;
                        }
                        catch (FormatException e)
                        {
                        }
                    }
                }
                if (year <= stopDate_year && game_month == stopDate_month)
                {
                    if (game_day > stopDate_day)
                    {
                        year /= 10000;
                        month /= 100;
                        day /= 100;
                        hour /= 100;
                        minutes /= 100;

                        CreateFolders_Files_OfTeams(firstTeam);
                        CreateFolders_Files_OfTeams(secondTeam);
                        WriteToFoldersAndFiles(firstTeam, secondTeam, scoreFT, scoreST, year, month, day, hour, minutes);
                    }
                }
                else
                {
                    year /= 10000;
                    month /= 100;
                    day /= 100;
                    hour /= 100;
                    minutes /= 100;

                    CreateFolders_Files_OfTeams(firstTeam);
                    CreateFolders_Files_OfTeams(secondTeam);
                    WriteToFoldersAndFiles(firstTeam, secondTeam, scoreFT, scoreST, year, month, day, hour, minutes);
                }
            }
        }

        /*This method is the master from where all of the method to write to certain files are called
         *and get the number assigned to both teams and add the number to the database.
        */
        static void WriteToFoldersAndFiles(string first_Team, string second_Team, int scoreFT, int scoreST, double year, double month, double day, double hour, double minutes)
        {
            double ftNumber = Team_ObtainNumber_NumberToDatabase(first_Team),
                stNumber = Team_ObtainNumber_NumberToDatabase(second_Team);

            WriteToFolder_OnlyFirstTeam(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber);
            WriteToFirstTeamFolder(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber);
            WriteToSecondTeamFolder(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber);
            WriteToFolder_NeuralNetworksFirstTeam(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber);
        }

        /*Assign a number to a team and add it to our database in
         *"C:\...\Dota2GamesInformation\TeamsClassification\TeamsClassification.txt"
        */
        public static double Team_ObtainNumber_NumberToDatabase(string team)
        {
            string path = main_path + dota2GamesInformation + @"\" + "TeamsClassification" + @"\" + "TeamsClassification.txt";
            double teamNumber = 0;

            StreamReader sr = new StreamReader(path);

            string read_team = sr.ReadLine();

            if (read_team == null)
            {
                sr.Close();

                StreamWriter sw = new StreamWriter(path, true);

                teamNumber = 0.01;
                sw.WriteLine(team + "|" + teamNumber);

                sw.Close();
            }
            else
            {
                double maxNumber = 0,
                    previousNumber = 0;

                bool foundTeam = false;

                while (read_team != null)
                {
                    int separationIndex = read_team.IndexOf("|"),
                        numberIndex = read_team.IndexOf("|") + 1;

                    string listTeam = read_team.Substring(0, separationIndex),
                        listNumber = read_team.Substring(numberIndex);

                    if (team.Equals(listTeam))
                    {
                        foundTeam = true;

                        teamNumber = Double.Parse(listNumber);
                    }

                    previousNumber = Double.Parse(listNumber);

                    if (previousNumber > maxNumber)
                    {
                        maxNumber = previousNumber;
                    }

                    read_team = sr.ReadLine();
                }

                if (foundTeam == false)
                {
                    sr.Close();

                    StreamWriter sWriter = new StreamWriter(path, true);

                    teamNumber = maxNumber + 0.01;
                    sWriter.WriteLine(team + "|" + teamNumber);

                    sWriter.Close();
                }
            }
            sr.Close();

            return teamNumber;
        }

        //Write to the folder OnlyFirstTeam located in TeamsFolders
        static void WriteToFolder_OnlyFirstTeam(string first_Team, string second_Team, int scoreFT, int scoreST, double year, double month, double day, double hour, double minutes, double ftNumber, double stNumber)
        {
            string teamFolderName = main_path + dota2GamesInformation + @"\" + "TeamsFolders" + @"\",
                allTeams_OnlyFirstTeam = "AllTeams_OnlyFirstTeam",
                folderPath = teamFolderName + allTeams_OnlyFirstTeam,
                filePathData = folderPath + @"\" + allTeams_OnlyFirstTeam + "Data.txt",
                filePathResult = folderPath + @"\" + allTeams_OnlyFirstTeam + "Result.txt",
                filePathDataWithNames = folderPath + @"\" + allTeams_OnlyFirstTeam + "DataWithNames.txt",
                filePathResultWithNames = folderPath + @"\" + allTeams_OnlyFirstTeam + "ResultWithNames.txt",
                score = "",
                scoreName = "";

            WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathData, score, scoreName);
            WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathDataWithNames, score, scoreName);

            if (scoreFT > scoreST)
            {
                score = "1";
                scoreName = "Win";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResult, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResultWithNames, score, scoreName);
            }
            else if (scoreST > scoreFT)
            {
                score = "0";
                scoreName = "Lose";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResult, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResultWithNames, score, scoreName);
            }
            else
            {
                score = "0";
                scoreName = "Draw";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResult, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResultWithNames, score, scoreName);
            }
        }

        /*Write to the folder of first_Team, which is one of the teams that played a game, 
         * the folder is located in AllTeamsFolder
        */
        static void WriteToFirstTeamFolder(string first_Team, string second_Team, int scoreFT, int scoreST, double year, double month, double day, double hour, double minutes, double ftNumber, double stNumber)
        {
            string teamFolderName = main_path + dota2GamesInformation + @"\" + "TeamsFolders" + @"\",
                folderPath = teamFolderName + first_Team,
                filePathData = folderPath + @"\" + first_Team + "Data.txt",
                filePathResult = folderPath + @"\" + first_Team + "Result.txt",
                filePathDataWithNames = folderPath + @"\" + first_Team + "DataWithNames.txt",
                filePathResultWithNames = folderPath + @"\" + first_Team + "ResultWithNames.txt",
                score = "",
                scoreName = "";

            WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathData, score, scoreName);
            WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathDataWithNames, score, scoreName);

            if (scoreFT > scoreST)
            {
                score = "1";
                scoreName = "Win";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResult, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResultWithNames, score, scoreName);
            }
            else if (scoreST > scoreFT)
            {
                score = "0";
                scoreName = "Lose";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResult, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResultWithNames, score, scoreName);
            }
            else
            {
                score = "0";
                scoreName = "Draw";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResult, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResultWithNames, score, scoreName);
            }

        }

        /*Write to the folder of second_Team, which is one of the teams that played a game, 
          * the folder is located in AllTeamsFolder
         */
        static void WriteToSecondTeamFolder(string first_Team, string second_Team, int scoreFT, int scoreST, double year, double month, double day, double hour, double minutes, double ftNumber, double stNumber)
        {
            string teamFolderName = main_path + dota2GamesInformation + @"\" + "TeamsFolders" + @"\",
                folderPath = teamFolderName + second_Team,
                filePathData = folderPath + @"\" + second_Team + "Data.txt",
                filePathResult = folderPath + @"\" + second_Team + "Result.txt",
                filePathDataWithNames = folderPath + @"\" + second_Team + "DataWithNames.txt",
                filePathResultWithNames = folderPath + @"\" + second_Team + "ResultWithNames.txt",
                score = "",
                scoreName = "";

            WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathData, score, scoreName);
            WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathDataWithNames, score, scoreName);

            if (scoreST > scoreFT)
            {
                score = "1";
                scoreName = "Win";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResult, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResultWithNames, score, scoreName);
            }
            else if (scoreST < scoreFT)
            {
                score = "0";
                scoreName = "Lose";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResult, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResultWithNames, score, scoreName);
            }
            else
            {
                score = "0";
                scoreName = "Draw";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResult, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathResultWithNames, score, scoreName);
            }
        }

        //Write to the folder OnlyNeuralNetworksFirstTeam located in TeamsFolders
        static void WriteToFolder_NeuralNetworksFirstTeam(string first_Team, string second_Team, int scoreFT, int scoreST, double year, double month, double day, double hour, double minutes, double ftNumber, double stNumber)
        {
            string teamFolderName = main_path + dota2GamesInformation + @"\" + "TeamsFolders" + @"\",
                allTeams_NeuralNetworksFirstTeam = "AllTeams_NeuralNetworksFirstTeam",
                folderPath = teamFolderName + allTeams_NeuralNetworksFirstTeam,
                filePathData = folderPath + @"\" + allTeams_NeuralNetworksFirstTeam + "Data.txt",
                filePathDataWithNames = folderPath + @"\" + allTeams_NeuralNetworksFirstTeam + "DataWithNames.txt",
                score = "",
                scoreName = "";

            if (scoreFT > scoreST)
            {
                score = "1";
                scoreName = "Win";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathData, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathDataWithNames, score, scoreName);
            }
            else if (scoreST > scoreFT)
            {
                score = "0";
                scoreName = "Lose";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathData, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathDataWithNames, score, scoreName);
            }
            else
            {
                score = "0";
                scoreName = "Draw";

                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathData, score, scoreName);
                WriteToFile(first_Team, second_Team, scoreFT, scoreST, year, month, day, hour, minutes, ftNumber, stNumber, filePathDataWithNames, score, scoreName);
            }
        }

        //This method determines to which file we shuld write and what we should write
        public static void WriteToFile(string first_Team, string second_Team, int scoreFT, int scoreST, double year, double month, double day, double hour, double minutes, double ftNumber, double stNumber, string filePath, string score, string scoreName)
        {
            string allTeams_OnlyFirstTeam = "AllTeams_OnlyFirstTeam",
                allTeams_NeuralNetworksFirstTeam = "AllTeams_NeuralNetworksFirstTeam",
                fileData = "Data.txt",
                fileResult = "Result.txt",
                fileDataWithNames = "DataWithNames.txt",
                fileResultWithNames = "ResultWithNames.txt";

            StreamWriter sw = new StreamWriter(filePath, true);

            if (filePath.Contains(allTeams_OnlyFirstTeam + fileData))
            {
                sw.WriteLine(ftNumber + "," + stNumber + "," + year + "," + month + "," + day + "," + hour + "," + minutes);
            }
            else if (filePath.Contains(allTeams_OnlyFirstTeam + fileResult))
            {
                sw.WriteLine(score);
            }
            else if (filePath.Contains(allTeams_OnlyFirstTeam + fileDataWithNames))
            {
                sw.WriteLine(first_Team + "," + second_Team + "," + year + "," + month + "," + day + "," + hour + "," + minutes);
            }
            else if (filePath.Contains(allTeams_OnlyFirstTeam + fileResultWithNames))
            {
                sw.WriteLine(scoreName);
            }
            ///////////////////////////
            else if (filePath.Contains(first_Team + fileData))
            {
                sw.WriteLine(ftNumber + "," + stNumber + "," + year + "," + month + "," + day + "," + hour + "," + minutes);
            }
            else if (filePath.Contains(first_Team + fileResult))
            {
                sw.WriteLine(score);
            }
            else if (filePath.Contains(first_Team + fileDataWithNames))
            {
                sw.WriteLine(first_Team + "," + second_Team + "," + year + "," + month + "," + day + "," + hour + "," + minutes);
            }
            else if (filePath.Contains(first_Team + fileResultWithNames))
            {
                sw.WriteLine(scoreName);
            }
            //////////////////////////////
            else if (filePath.Contains(second_Team + fileData))
            {
                sw.WriteLine(stNumber + "," + ftNumber + "," + year + "," + month + "," + day + "," + hour + "," + minutes);
            }
            else if (filePath.Contains(second_Team + fileResult))
            {
                sw.WriteLine(score);
            }
            else if (filePath.Contains(second_Team + fileDataWithNames))
            {
                sw.WriteLine(second_Team + "," + first_Team + "," + year + "," + month + "," + day + "," + hour + "," + minutes);
            }
            else if (filePath.Contains(second_Team + fileResultWithNames))
            {
                sw.WriteLine(scoreName);
            }
            /////////////////////////////
            else if (filePath.Contains(allTeams_NeuralNetworksFirstTeam + fileData))
            {
                sw.WriteLine(ftNumber + "," + stNumber + "," + year + "," + month + "," + day + "," + hour + "," + minutes + "," + score);
            }
            else if (filePath.Contains(allTeams_NeuralNetworksFirstTeam + fileDataWithNames))
            {
                sw.WriteLine(first_Team + "," + second_Team + "," + year + "," + month + "," + day + "," + hour + "," + minutes + "," + scoreName);
            }

            sw.Close();
        }

    }
}