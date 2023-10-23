USE [YourDBName]
GO

/****** Object:  Table [dbo].[Vendor] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Bill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderOf] [nvarchar](200) NOT NULL,
	[AccountId] [int] NOT NULL, # account numbers are not in this system
	[VendorId] [int] NOT NULL, # technically, it should be a FK to VENDOR table
	[Amount] [decimal(10, 2)] NOT NULL,
	[DueDate] [date] NULL,
	[Paid] [int] DEFAULT 0


 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
