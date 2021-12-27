# DecisionAdventure

# Running in local
Run via Docker config, Self hosted in Visual studio 2022 <br />

To start via Docker, run follwing command in root <br />
Azure SQL db is connected via hardcoded connection string (should be a secret injected via env variable) <br />

```
docker build . --tag decisionadventure
docker run -d -p 5050:80 decisionadventure:latest
```

Swagger Docker URL : http://localhost:5050/swagger/index.html   

![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/Swagger.jpg)

# UI
Used basic react UI to do minium code which utilizes API for following functionalities  <br />

1. Create/Design Adventure <br />
2. Take Adventure <br />
3. List adventures taken so far and show Decision tree once user reaches end <br />

User will not able to add more question to an answer (restricted in UI as one answer can point to another question) <br />
User can add add multiple answers (more than 2) <br />
User can add question to any level <br />

Tree has root node/label with adventure name. <br />
After that it has one child with first question. <br />

Show decision tree will show selected options highlighted in yellow color <br />

# Demo

# Creating Adventure (Designing)

Once app is started, app home screen list all available adventures and list of adventures taken by user (username input field is given to differntiate multiple runs/user)
<br />

 <strong>List page </strong>
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/ListOfAdventure%20and%20List%20of%20taken.jpg) 

To create adventure, click on create adventure button on top right nav
<br />

Provide a name for adventure and click create. User will be redirected to another create adventure page
 <br /><strong>Create Adventure Page</strong>
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/CreateAdventure%20-1.jpg)

A tree is shown with adventure name in root name.
<br />
 <strong>Create Adventure Decision tree Page</strong>
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/CreateAdventure%20-2.jpg)

User have to add first question now by clicking add button
  <br /> <strong>Add question/Answer dialog</strong>
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/CreateAdventure%20-%20AddQuestionwithAnswers.jpg)

Once user entre question, adds multiple answers if needed then click confirm
Once that is done answers will be added to tree

![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/CreateAdventure%20-%20QuestionAdded.jpg)


Add more questions for more paths,
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/CreateAdventure%20-%20AddMoreQuestionWithAnswer%20-%20End%20the%20path%20with%20question%20without%20answer.jpg)


Add leaf decision as Question without any answers(leave the answers empty) as below
 <br /><strong>Leaf Decision</strong><br />
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/StartAdventure%20-%20LastQuestion.jpg)
 

# Start

User can start an adventure by clicking button (start) from home page - 1st listing <br />
USer will be redirected to a page where user has to choose answers by clikcing on asnwers. <br />

Question will be underlined. Answrs will not be underlined. Clicking only enabled for answers. <br />

example new start, <br />
First question will be displayed <br />
 <br /> <strong>Start adventure path</strong>
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/StartAdventure%20-1.jpg)

User can choose answers. <br />
once choosen, more questions will be displayed.
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/StartAdventure%20-2.jpg)

# Decision Tree
Once final question/path is reached user will be redirected to a decision page (readonly) where no click can be done or no more answers cna be choosen. <br />
<br />
Selected answers will be highlighted in yellow 
  <br /> <strong>Decision Tree</strong>
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/LastQuestion-DecisonTree.jpg)

User can also see past adventures from home page (2nd listing)
 <br />   <strong>List Page</strong>
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DemoScreenshots/ListOfTakenUpdatedAfterLastQuestion.jpg)

# API
Web api is used with Dapper for DB queries <br />
Added inline queries as much as possible to finish faster. <br />
Added a one store proc as one example to calll store procs via dapper <br />
Entity framework/Ado.net is also nice to have but dapper is more efficient <br />

# DB
Please see DB diagram in root folder for table deisgns and all scripts are in DBSCripts folder inside Decison Adventure folder <br />
Azure DB is connected via Connection string hardcoded <br />

   <strong>DB Diagram</strong>
![name-of-you-image](https://github.com/pandurd/DecisionAdventure/raw/master/DBDiagram.jpg) 

#  Tests
Xunit used to do basic and important decision tree testing. <br />

