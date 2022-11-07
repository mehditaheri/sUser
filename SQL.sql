USE [SteUser]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserInformation'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserInformation'
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserInformation'
GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF_User_UnsuccessfulLoginCount]
GO
/****** Object:  Table [dbo].[UserReady]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserReady]') AND type in (N'U'))
DROP TABLE [dbo].[UserReady]
GO
/****** Object:  Table [dbo].[ResumeSkill]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResumeSkill]') AND type in (N'U'))
DROP TABLE [dbo].[ResumeSkill]
GO
/****** Object:  Table [dbo].[ResumeLectureSubject]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResumeLectureSubject]') AND type in (N'U'))
DROP TABLE [dbo].[ResumeLectureSubject]
GO
/****** Object:  Table [dbo].[ResumeLectureLanguage]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResumeLectureLanguage]') AND type in (N'U'))
DROP TABLE [dbo].[ResumeLectureLanguage]
GO
/****** Object:  Table [dbo].[ResumeLectureGroup]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResumeLectureGroup]') AND type in (N'U'))
DROP TABLE [dbo].[ResumeLectureGroup]
GO
/****** Object:  Table [dbo].[ResumeLanguage]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResumeLanguage]') AND type in (N'U'))
DROP TABLE [dbo].[ResumeLanguage]
GO
/****** Object:  Table [dbo].[ResumeEducation]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResumeEducation]') AND type in (N'U'))
DROP TABLE [dbo].[ResumeEducation]
GO
/****** Object:  Table [dbo].[ResumeCertificate]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResumeCertificate]') AND type in (N'U'))
DROP TABLE [dbo].[ResumeCertificate]
GO
/****** Object:  Table [dbo].[RefreshToken_old]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RefreshToken_old]') AND type in (N'U'))
DROP TABLE [dbo].[RefreshToken_old]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RefreshToken]') AND type in (N'U'))
DROP TABLE [dbo].[RefreshToken]
GO
/****** Object:  Table [dbo].[Ready]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ready]') AND type in (N'U'))
DROP TABLE [dbo].[Ready]
GO
/****** Object:  Table [dbo].[BaseInformation]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseInformation]') AND type in (N'U'))
DROP TABLE [dbo].[BaseInformation]
GO
/****** Object:  View [dbo].[UserInformation]    Script Date: 10/29/2022 2:49:58 AM ******/
DROP VIEW [dbo].[UserInformation]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND type in (N'U'))
DROP TABLE [dbo].[UserProfile]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Resume]    Script Date: 10/29/2022 2:49:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resume]') AND type in (N'U'))
DROP TABLE [dbo].[Resume]
GO
/****** Object:  Table [dbo].[Resume]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resume](
	[UserId] [int] NOT NULL,
	[UserImageUrl] [nvarchar](1000) NULL,
	[Introduction] [nvarchar](4000) NULL,
	[SavabegheTadris] [nvarchar](4000) NULL,
	[SavabegheBargozari] [nvarchar](4000) NULL,
	[SavabegheTahghigh] [nvarchar](4000) NULL,
	[SavabegheEjra] [nvarchar](4000) NULL,
	[SuggestedTopics] [nvarchar](4000) NULL,
	[Video1Url] [nvarchar](1000) NULL,
	[Audio1Url] [nvarchar](1000) NULL,
	[Video2Url] [nvarchar](1000) NULL,
	[Audio2Url] [nvarchar](1000) NULL,
	[Plate] [nvarchar](50) NULL,
	[OtherLectureLanguage] [nvarchar](100) NULL,
	[Status] [bit] NULL,
	[RejectionReasons] [nvarchar](4000) NULL,
	[SetStatusAt] [datetime] NULL,
	[SetStatusUserId] [int] NULL,
	[SetStatusFullName] [nvarchar](500) NULL,
	[CenterStatus] [bit] NULL,
	[CenterSetStatusAt] [datetime] NULL,
	[CenterSetStatusUserId] [int] NULL,
	[CenterSetStatusFullName] [nvarchar](500) NULL,
	[CenterRejectionReasons] [nvarchar](4000) NULL,
	[ResumeStatus] [tinyint] NULL,
	[ResumeSetStatusAt] [datetime] NULL,
	[ResumeSetStatusUserId] [int] NULL,
	[ResumeSetStatusFullName] [nvarchar](500) NULL,
	[ResumeRejectionReasons] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Resume] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] NOT NULL,
	[ProvinceId] [int] NULL,
	[CountyId] [int] NULL,
	[LocationId] [bigint] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Family] [nvarchar](50) NOT NULL,
	[NationalCode] [bigint] NULL,
	[PhoneNumber] [bigint] NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[GenderId] [int] NULL,
	[Birthdate] [date] NULL,
	[Status] [int] NOT NULL,
	[UnsuccessfulLoginCount] [tinyint] NULL,
	[CreateAt] [datetime] NOT NULL,
	[Verified] [bit] NOT NULL,
	[RegisterFromGovernment] [bit] NOT NULL,
	[Temp] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] NOT NULL,
	[FatherName] [nvarchar](50) NULL,
	[IdNumber] [nvarchar](50) NULL,
	[IdNumberIssueLocation] [nvarchar](50) NULL,
	[SoldierStatusId] [int] NULL,
	[MarriedStatusId] [int] NULL,
	[DependentsCount] [int] NULL,
	[HomePhone] [nvarchar](50) NULL,
	[Address] [nvarchar](400) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[EmergencyPhone] [nvarchar](50) NULL,
	[BankAccountNumber] [nvarchar](50) NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[UserInformation]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserInformation]
AS
SELECT        dbo.[User].UserId, dbo.[User].Name, dbo.[User].Family, dbo.UserProfile.FatherName, dbo.[User].NationalCode, dbo.[User].PhoneNumber, dbo.[User].Username, dbo.[User].Password, dbo.[User].Status, dbo.[User].CreateAt, 
                         dbo.[User].UnsuccessfulLoginCount, dbo.[User].ProvinceId, dbo.[User].CountyId, dbo.[User].LocationId, dbo.[User].GenderId, dbo.[User].Birthdate, dbo.[User].Verified, dbo.[User].RegisterFromGovernment, 
                         dbo.Resume.ResumeStatus, dbo.Resume.CenterStatus
FROM            dbo.Resume INNER JOIN
                         dbo.UserProfile ON dbo.Resume.UserId = dbo.UserProfile.UserId INNER JOIN
                         dbo.[User] ON dbo.Resume.UserId = dbo.[User].UserId
GO
/****** Object:  Table [dbo].[BaseInformation]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseInformation](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_BaseInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ready]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ready](
	[ReadyId] [int] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Summary] [nvarchar](4000) NOT NULL,
	[Status] [bit] NOT NULL,
	[AcceptStartDate] [date] NOT NULL,
	[AcceptEndDate] [date] NOT NULL,
	[AtlasReportCalendarId] [int] NULL,
	[ShowOrder] [int] NOT NULL,
 CONSTRAINT [PK_Ready] PRIMARY KEY CLUSTERED 
(
	[ReadyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken]
(
	[RefreshTokenId] [uniqueidentifier] NOT NULL,
	[AccessTokenId] [uniqueidentifier] NOT NULL,
	[ExpireAt] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[Ip] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,

 CONSTRAINT [RefreshToken_primaryKey]  PRIMARY KEY NONCLUSTERED HASH 
(
	[RefreshTokenId]
)WITH ( BUCKET_COUNT = 64)
)WITH ( MEMORY_OPTIMIZED = ON , DURABILITY = SCHEMA_AND_DATA )
GO
/****** Object:  Table [dbo].[RefreshToken_old]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken_old](
	[RefreshTokenId] [uniqueidentifier] NOT NULL,
	[AccessTokenId] [uniqueidentifier] NOT NULL,
	[ExpireAt] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[Ip] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[RefreshTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeCertificate]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeCertificate](
	[ResumeCertificateId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[OrganizationTitle] [nvarchar](500) NOT NULL,
	[ImageUrl] [nvarchar](4000) NULL,
 CONSTRAINT [PK_ResumeCertificate] PRIMARY KEY CLUSTERED 
(
	[ResumeCertificateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeEducation]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeEducation](
	[ResumeEducationId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[LevelId] [int] NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Orientation] [nvarchar](100) NULL,
	[UniversityName] [nvarchar](100) NULL,
	[ImageUrl] [nvarchar](4000) NULL,
 CONSTRAINT [PK_ResumeEducation] PRIMARY KEY CLUSTERED 
(
	[ResumeEducationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeLanguage]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeLanguage](
	[UserId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[LevelId] [tinyint] NOT NULL,
 CONSTRAINT [PK_UserLanguage] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeLectureGroup]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeLectureGroup](
	[ResumeLectureGroupId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_ResumeLectureGroup] PRIMARY KEY CLUSTERED 
(
	[ResumeLectureGroupId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeLectureLanguage]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeLectureLanguage](
	[ResumeLectureLanguageId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_ResumeLectureLanguage] PRIMARY KEY CLUSTERED 
(
	[ResumeLectureLanguageId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeLectureSubject]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeLectureSubject](
	[ResumeLectureSubjectId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_ResumeLectureSubject] PRIMARY KEY CLUSTERED 
(
	[ResumeLectureSubjectId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResumeSkill]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResumeSkill](
	[ResumeSkillId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[SkillTitle] [nvarchar](500) NOT NULL,
	[LevelId] [tinyint] NOT NULL,
 CONSTRAINT [PK_UserSkill] PRIMARY KEY CLUSTERED 
(
	[ResumeSkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserReady]    Script Date: 10/29/2022 2:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserReady](
	[ReadyId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_UserReady] PRIMARY KEY CLUSTERED 
(
	[ReadyId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_UnsuccessfulLoginCount]  DEFAULT ((0)) FOR [UnsuccessfulLoginCount]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[31] 4[31] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Resume"
            Begin Extent = 
               Top = 23
               Left = 451
               Bottom = 185
               Right = 681
            End
            DisplayFlags = 280
            TopColumn = 12
         End
         Begin Table = "UserProfile"
            Begin Extent = 
               Top = 13
               Left = 811
               Bottom = 205
               Right = 1026
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 263
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 21
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2235
         Width = 1305
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1890
         Output = 1350
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserInformation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserInformation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserInformation'
GO
