
/****** Object:  Table [dbo].[t_exitems]    Script Date: 2017-10-18 9:38:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_exitems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](100) NOT NULL,
	[Answer] [nvarchar](100) NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[Types] [nvarchar](50) NULL,
 CONSTRAINT [PK_t_exitems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[t_extype]    Script Date: 2017-10-18 9:39:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_extype](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](50) NOT NULL,
	[DETAILS] [nvarchar](100) NULL,
 CONSTRAINT [PK_t_extype] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
