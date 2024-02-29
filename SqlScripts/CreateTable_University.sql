USE [IServApp]
GO

/****** Object:  Table [dbo].[University]    Script Date: 29.02.2024 7:52:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[University](
	[UniversityId] [int] NOT NULL,
	[AlphaTwoCode] [nvarchar](max) NOT NULL,
	[StateProvince] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UniversityVersion] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_University] PRIMARY KEY CLUSTERED 
(
	[UniversityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[University] ADD  DEFAULT (NEXT VALUE FOR [UniversityIdSequence]) FOR [UniversityId]
GO


