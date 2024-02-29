USE [IServApp]
GO

/****** Object:  Table [dbo].[WebPage]    Script Date: 29.02.2024 7:57:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WebPage](
	[WebPageId] [int] IDENTITY(1,1) NOT NULL,
	[WebPageUrlAddress] [nvarchar](max) NOT NULL,
	[WebPageName] [nvarchar](max) NULL,
	[UniversityId] [int] NULL,
 CONSTRAINT [PK_WebPage] PRIMARY KEY CLUSTERED 
(
	[WebPageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[WebPage]  WITH CHECK ADD  CONSTRAINT [FK_WebPage_University_UniversityId] FOREIGN KEY([UniversityId])
REFERENCES [dbo].[University] ([UniversityId])
GO

ALTER TABLE [dbo].[WebPage] CHECK CONSTRAINT [FK_WebPage_University_UniversityId]
GO


