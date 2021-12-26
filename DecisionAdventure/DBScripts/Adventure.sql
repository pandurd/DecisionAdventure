USE [Adventure]
GO

/****** Object:  Table [dbo].[Adventure]    Script Date: 12/26/2021 11:47:06 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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


