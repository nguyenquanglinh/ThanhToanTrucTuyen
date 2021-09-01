create database [ThanhToanTrucTuyen]
go
USE [ThanhToanTrucTuyen]
GO
/****** Object:  Table [dbo].[SoTaiKhoanThe]    Script Date: 8/24/2021 1:58:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SoTaiKhoanThe](
	[stk] [nvarchar](500) NOT NULL CONSTRAINT [DF_The_stk]  DEFAULT ((0.0)),
	[soDuHienTai] [nvarchar](500) NOT NULL CONSTRAINT [DF_The_soDuHienTai]  DEFAULT ((50000)),
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idTK] [int] NULL,
	[cks] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_The] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaiKhoanDangNhap]    Script Date: 8/24/2021 1:58:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoanDangNhap](
	[tenDangNhap] [nvarchar](500) NOT NULL CONSTRAINT [DF_TaiKhoan_tenDangNhap]  DEFAULT (N'test'),
	[matKhau] [nvarchar](500) NOT NULL CONSTRAINT [DF_TaiKhoan_matKhau]  DEFAULT (N'12345678aA@'),
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cks] [nvarchar](500) NULL,
	[maPin] [nvarchar](500) NULL CONSTRAINT [DF_TaiKhoan_maPin]  DEFAULT ((0)),
	[sdt] [nvarchar](500) NOT NULL,
	[quyen] [nvarchar](500) NULL CONSTRAINT [DF_TaiKhoan_quyen]  DEFAULT (user_name()),
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SoTaiKhoanThe]  WITH CHECK ADD  CONSTRAINT [FK_The_TaiKhoan] FOREIGN KEY([idTK])
REFERENCES [dbo].[TaiKhoanDangNhap] ([id])
GO
ALTER TABLE [dbo].[SoTaiKhoanThe] CHECK CONSTRAINT [FK_The_TaiKhoan]
GO
