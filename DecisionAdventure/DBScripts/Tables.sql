/****** Object:  Table [dbo].[Adventure]    Script Date: 12/27/2021 1:19:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Adventure](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Adventure] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO





USE [Adventure]
GO

/****** Object:  Table [dbo].[AdventurePath]    Script Date: 12/26/2021 11:49:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AdventurePath](
	[ID] [uniqueidentifier] NOT NULL,
	[AdventureID] [uniqueidentifier] NOT NULL,
	[PreviousAnswer] [uniqueidentifier] NULL,
	[Question] [varchar](200) NOT NULL,
	[Level] [int] NOT NULL,
 CONSTRAINT [PK_AdventurePath] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdventurePath]  WITH CHECK ADD  CONSTRAINT [FK_AdventurePath_Adventure] FOREIGN KEY([AdventureID])
REFERENCES [dbo].[Adventure] ([ID])
GO

ALTER TABLE [dbo].[AdventurePath] CHECK CONSTRAINT [FK_AdventurePath_Adventure]
GO


USE [Adventure]
GO

/****** Object:  Table [dbo].[AdventurePathOption]    Script Date: 12/26/2021 11:49:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AdventurePathOption](
	[ID] [uniqueidentifier] NOT NULL,
	[PathID] [uniqueidentifier] NOT NULL,
	[Label] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AdventurePathOption] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdventurePathOption]  WITH CHECK ADD  CONSTRAINT [FK_AdventurePathOption_AdventurePath] FOREIGN KEY([PathID])
REFERENCES [dbo].[AdventurePath] ([ID])
GO

ALTER TABLE [dbo].[AdventurePathOption] CHECK CONSTRAINT [FK_AdventurePathOption_AdventurePath]
GO



/****** Object:  Table [dbo].[UserAdventure]    Script Date: 12/27/2021 1:23:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserAdventure](
	[ID] [uniqueidentifier] NOT NULL,
	[UserJourneyID] [uniqueidentifier] NOT NULL,
	[PathID] [uniqueidentifier] NOT NULL,
	[OptionID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_UserAdventure] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserAdventure]  WITH CHECK ADD  CONSTRAINT [FK_UserAdventure_AdventurePathOption] FOREIGN KEY([OptionID])
REFERENCES [dbo].[AdventurePathOption] ([ID])
GO

ALTER TABLE [dbo].[UserAdventure] CHECK CONSTRAINT [FK_UserAdventure_AdventurePathOption]
GO

ALTER TABLE [dbo].[UserAdventure]  WITH CHECK ADD  CONSTRAINT [FK_UserAdventure_UserAdventure] FOREIGN KEY([PathID])
REFERENCES [dbo].[AdventurePath] ([ID])
GO

ALTER TABLE [dbo].[UserAdventure] CHECK CONSTRAINT [FK_UserAdventure_UserAdventure]
GO

ALTER TABLE [dbo].[UserAdventure]  WITH CHECK ADD  CONSTRAINT [FK_UserAdventure_UserJourney] FOREIGN KEY([UserJourneyID])
REFERENCES [dbo].[UserJourney] ([ID])
GO

ALTER TABLE [dbo].[UserAdventure] CHECK CONSTRAINT [FK_UserAdventure_UserJourney]
GO


/****** Object:  Table [dbo].[UserJourney]    Script Date: 12/27/2021 1:26:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserJourney](
	[ID] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[AdventureID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserJourney] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserJourney]  WITH CHECK ADD  CONSTRAINT [FK_UserJourney_Adventure] FOREIGN KEY([AdventureID])
REFERENCES [dbo].[Adventure] ([ID])
GO

ALTER TABLE [dbo].[UserJourney] CHECK CONSTRAINT [FK_UserJourney_Adventure]
GO


