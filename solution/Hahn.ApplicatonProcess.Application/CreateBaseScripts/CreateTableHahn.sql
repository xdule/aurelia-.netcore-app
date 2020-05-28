USE [Hahn]
GO

/****** Object:  Table [dbo].[Applicant]    Script Date: 5/28/2020 12:27:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Applicant](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[FamiliyName] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[CountryOfOrigin] [nvarchar](100) NOT NULL,
	[EMailAddress] [nvarchar](100) NOT NULL,
	[Age] [int] NOT NULL,
	[Hired] [bit] NOT NULL,
 CONSTRAINT [PK_Applicant] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


