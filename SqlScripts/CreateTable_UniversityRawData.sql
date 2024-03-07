USE [IServApp]
GO

/****** Object:  Table [dbo].[UniversityRawData]    Script Date: 07.03.2024 11:52:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UniversityRawData](
	[UniversityRawDataId] [int] NOT NULL,
	[AlphaTwoCode] [nvarchar](max) NULL,
	[StateProvince] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Country] [nvarchar](max) NOT NULL,
	[WebPages] [nvarchar](max) NOT NULL,
	[Domains] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UniversityRawData] PRIMARY KEY CLUSTERED 
(
	[UniversityRawDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[UniversityRawData] ADD  DEFAULT (NEXT VALUE FOR [UniversityRawDataIdSequence]) FOR [UniversityRawDataId]
GO


