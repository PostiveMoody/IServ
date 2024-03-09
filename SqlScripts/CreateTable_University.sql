USE [IServApp]
GO

/****** Object:  Table [dbo].[University]    Script Date: 09.03.2024 7:41:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[University](
	[UniversityId] [int] NOT NULL,
	[Country] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[WebPages] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_University] PRIMARY KEY CLUSTERED 
(
	[UniversityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[University] ADD  DEFAULT (NEXT VALUE FOR [UniversityIdSequence]) FOR [UniversityId]
GO