# DecisionAdventure

# Running in local
Run via Docker config, Self hosted in Visual studio 2022

To start via Docker, run follwing command in root
Azure SQL db is connected via hardcoded connection string (should be a secret injected via env variable)

docker build . --tag decisionadventure
docker run -d -p 5050:80 decisionadventure:latest


# UI
Used basic reat UI to do minium code which utilizes API and does all functionalities 

1. Create/Design Adventure
2. Take Adventure
3. List adventures taken so far and show Decision tree once user reaches end

User will not able to add more question to an answer (restricted in UI as one answer can point to another question)
User can add add multiple answers (more than 2)
User can add question to any level

Tree has root node/label with adventure name.
After that it has one child with first question.

Show decision tree will show selected options highlighted in yellow color




# API
Web api is used with Dapper for DB queries
Added inline queries as much as possible to finish faster.
Added a one store proc as one example to calll store procs via dapper
Entity framework/Ado.net is also nice to have but dapper is more efficient

# DB
Please see DB diagram in root folder for table deisgns and all scripts are in DBSCripts folder inside Decison Adventure folder
Azure DB is connected via Connection string hardcoded

![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DBDiagram.jpg)

#  tests
Xunit used to do basic and important decision tree testing.

