/****** Object:  UserDefinedTableType [dbo].[AdventurePathOption]    Script Date: 12/27/2021 1:28:37 AM ******/
CREATE TYPE [dbo].[AdventurePathOption] AS TABLE(
	[ID] [uniqueidentifier] NOT NULL,
	[AdventureID] [uniqueidentifier] NOT NULL,
	[Label] [varchar](200) NOT NULL
)
GO


