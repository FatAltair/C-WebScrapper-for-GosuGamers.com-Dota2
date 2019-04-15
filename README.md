# C-WebScrapper-for-GosuGamers.com-Dota2
A C# Webscrapper that obtains the information of the matches played by Professional DOTA 2 Teams from GosuGamers.com.

                                Dota2GamesInformation from GosuGamers.com/Dota2
The following software is a web scraper created, designed and done by FatAltair in 2016 as a way of obtaining information of all the matches played by Professional DOTA 2 Teams from Gosugamers.com it is made on C# in Visual Studio 2015 and I am releasing it for everyone and anyone to use and improve as they like whether here or as their own projects, it is not perfect but if this is the type of information that you are looking for it will get the job done, remember that if you have any suggestion or comment let me know as it will be useful to improve the code and my own coding skills.

The software was done as to obtain professional game information from Dota 2 professional teams and use that information on another software made in Matlab that is an Artificial Intelligence and train the AI using Supervised and Unsupervised Learning as well as Neural Networks to obtain the percentage of likelihood of a professional team wining against another professional team in a specified year, month, day, hour and minute that the game will be played on, after training the AI I found that the information of at least a month previous to a professional game is between 80-90% of what will determine the winner of a game, my system predicted the winner of a match with a confidence rate of 70%.

The web scraper searches for all the Professional Teams registered in Gosugamers.com and it creates a folder named Dota2GamesInformation in your documents and stores the rest of the folders and files inside it, then it creates a folder called TeamsClassification, in it you can find a file named  TeamsClassification in which it assigns a decimal number starting from 0.01 to each team as an identifier.

Team -> [Team]|0.01 <- Identifier

It then continues to search for professional matches that were played by the Professional Teams and records the information on several files, below is an example of the recorded details for each game, teams that played against each other, the year, month, hour and minutes of when the game started, unless otherwise specified it also creates a file with the win or lose status of the team.

[First team], [second team], [year], [month], [day], [hour], [minute]
[Win] or [Lose] = [1] or [0]

The next folders and files can be found on the folder TeamsFolders, there each team has a folder in which there are several files inside them, the first is [Team]Data the file has the information described previously but with the first two number separated by a coma as the teams identifiers, the first one is the identifier of the team from who you currently are inside its folder and the second one is the opponent.

First team identifier -> [0.01], [0.02] <- Second team identifier, [year], [month], [day], [hour], [minute]

The second file [Team]DataWithNames is the same as the previous one but with the names of the teams instead of the first two numbers.

[First team], [second team], [year], [month], [day], [hour], [minute]

[Team]Result is the result of the match.

[1] or [0]; 1 = Win, 0 = Lose

 [Team]ResultWithNames is the same as the previous one but with the Win/Loss words instead of 1/0.
 
[Win] or [Lose]

The software also creates combinations of previous folders, the first one named AllTeams_OnlyFirstTeam it has the same information as [Team]Data but instead of being just one single team its all the teams AllTeams_OnlyFirstTeamData, it also is accompanied by its respective file with the names of the teams AllTeams_OnlyFirstTeamDataWithNames as well as the files with the results, both in binary and with names, AllTeams_OnlyFirstTeamResult & AllTeams_OnlyFirstTeamResultWithNames.

The next combinations were made specifically for Neural Networks (NN), the folder can be found as AllTeams_NeuralNetworksFirstTeam it has the same information as All Teams_OnlyFirstTeamData but with the Result as part of the file, positioned at the end, right of minutes, the files are AllTeams_NeuralNetworksFirstTeamData & AllTeams_NeuralNetworksFirstTeamDataWithNames.
[First team], [second team], [month], [year], [day], [hour], [minutes], [Result]

The code also creates a folder called Date and a file inside it called DateStop in which it stores the last day that it did a search of games and teams, to not do everything again each time that is requested to perform a search by the user and only search from the next date that it has on the file.

File -> StopDate -> year: 2016 month: 2 day: 14 <- web scraper will search starting from day 15

If you which you can modify the file to skip a day or redo the search after a specified date or delete it so that you can perform the search from the beginning as a fresh start.

On the folder there is also a filled named DateDatabase so that you can see all the dates that have been searched on.
