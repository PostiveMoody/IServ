USE [IServApp]
GO

/****** Object:  Table [dbo].[WebPageDomain]    Script Date: 29.02.2024 8:00:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WebPageDomain](
	[WebPageDomainId] [int] IDENTITY(1,1) NOT NULL,
	[WebPageDomainFullName] [nvarchar](max) NOT NULL,
	[WebPageDomainSecondLevel] [nvarchar](max) NOT NULL,
	[WebPageDomainRoot] [nvarchar](max) NOT NULL,
	[WebPageDomainProtocol] [nvarchar](max) NULL,
	[WebPageDomainFourthLevel] [nvarchar](max) NULL,
	[WebPageDomainThirdLevel] [nvarchar](max) NULL,
	[UniversityId] [int] NULL,
 CONSTRAINT [PK_WebPageDomain] PRIMARY KEY CLUSTERED 
(
	[WebPageDomainId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[WebPageDomain]  WITH CHECK ADD  CONSTRAINT [FK_WebPageDomain_University_UniversityId] FOREIGN KEY([UniversityId])
REFERENCES [dbo].[University] ([UniversityId])
GO

ALTER TABLE [dbo].[WebPageDomain] CHECK CONSTRAINT [FK_WebPageDomain_University_UniversityId]
GO


