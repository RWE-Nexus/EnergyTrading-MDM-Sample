USE [MDM_Sample]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Location](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[ParentLocationId] [int] NULL,
	[Name] [varchar](200) NOT NULL,
	[Start] [datetime] NOT NULL,
	[Finish] [datetime] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[Type] [nvarchar](200) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [CK_Location] UNIQUE NONCLUSTERED 
(
	[Name] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LocationAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LocationAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NULL,
	[Name] [varchar](200) NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [binary](8) NOT NULL,
	[ParentLocationId] [int] NULL,
	[Type] [nvarchar](200) NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_LocationAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LocationMapping]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LocationMapping](
	[LocationMappingId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[SourceSystemId] [int] NOT NULL,
	[MappingValue] [varchar](200) NOT NULL,
	[IsMaster] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Start] [datetime] NOT NULL,
	[Finish] [datetime] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_LocationMapping] PRIMARY KEY CLUSTERED 
(
	[LocationMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LocationMappingAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LocationMappingAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[LocationMappingId] [int] NULL,
	[LocationId] [int] NULL,
	[SourceSystemId] [int] NULL,
	[MappingValue] [varchar](200) NULL,
	[IsMaster] [bit] NULL,
	[IsDefault] [bit] NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [binary](8) NOT NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_LocationMappingAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Party]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Party](
	[PartyId] [int] IDENTITY(1,1) NOT NULL,
	[Version] [timestamp] NULL,
 CONSTRAINT [PK_Party] PRIMARY KEY CLUSTERED 
(
	[PartyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PartyDetails]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PartyDetails](
	[PartyDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[PartyId] [int] NOT NULL,
	[Name] [varchar](200) NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
	[Start] [datetime] NOT NULL,
	[Finish] [datetime] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[IsInternal] [bit] NOT NULL,
 CONSTRAINT [PK_PartyDetails] PRIMARY KEY CLUSTERED 
(
	[PartyDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PartyDetailsAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PartyDetailsAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[PartyDetailsId] [int] NULL,
	[PartyId] [int] NULL,
	[Name] [varchar](200) NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [binary](8) NOT NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
	[IsInternal] [bit] NULL,
 CONSTRAINT [PK_PartyDetailsAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PartyMapping]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PartyMapping](
	[PartyMappingId] [int] IDENTITY(1,1) NOT NULL,
	[PartyId] [int] NOT NULL,
	[SourceSystemId] [int] NOT NULL,
	[MappingValue] [varchar](200) NOT NULL,
	[IsMaster] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Start] [datetime] NOT NULL,
	[Finish] [datetime] NOT NULL,
	[Version] [timestamp] NULL,
 CONSTRAINT [PK_PartyMapping] PRIMARY KEY CLUSTERED 
(
	[PartyMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PartyMappingAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PartyMappingAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[PartyMappingId] [int] NULL,
	[PartyId] [int] NULL,
	[SourceSystemId] [int] NULL,
	[MappingValue] [varchar](200) NULL,
	[IsMaster] [bit] NULL,
	[IsDefault] [bit] NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [varbinary](8) NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_PartyMappingAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PartyRole]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PartyRole](
	[PartyRoleId] [int] IDENTITY(1,1) NOT NULL,
	[PartyRoleClass] [varchar](50) NULL,
	[PartyRoleType] [varchar](50) NULL,
	[PartyId] [int] NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_PartyRole] PRIMARY KEY CLUSTERED 
(
	[PartyRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PartyRoleAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PartyRoleAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[PartyRoleId] [int] NULL,
	[PartyRoleClass] [varchar](50) NULL,
	[PartyRoleType] [varchar](50) NULL,
	[PartyId] [int] NULL,
	[Version] [binary](8) NOT NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_PartyRoleAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PartyRoleDetails]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PartyRoleDetails](
	[PartyRoleDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[PartyRoleDetailsClass] [varchar](50) NOT NULL,
	[PartyRoleId] [int] NOT NULL,
	[Name] [varchar](200) NULL,
	[BrokerPhone] [varchar](200) NULL,
	[BrokerFax] [varchar](200) NULL,
	[ExchangePhone] [varchar](200) NULL,
	[ExchangeFax] [varchar](200) NULL,
	[CounterpartyPhone] [varchar](200) NULL,
	[CounterpartyFax] [varchar](200) NULL,
	[Rate] [decimal](18, 10) NULL,
	[ShortName] [nvarchar](20) NULL,
	[TaxLocationId] [int] NULL,
	[BusinessUnitPhone] [varchar](200) NULL,
	[BusinessUnitFax] [varchar](200) NULL,
	[Start] [datetime] NOT NULL,
	[Finish] [datetime] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[LegalEntityRegisteredName] [varchar](200) NULL,
	[LegalEntityRegistrationNumber] [varchar](200) NULL,
	[LegalEntityAddress] [varchar](200) NULL,
	[LegalEntityWebsite] [varchar](200) NULL,
	[LegalEntityEmail] [varchar](200) NULL,
	[LegalEntityFax] [varchar](200) NULL,
	[LegalEntityPhone] [varchar](200) NULL,
	[LegalEntityCountryOfInc] [varchar](200) NULL,
	[LegalEntityPartyStatus] [varchar](200) NULL,
	[BusinessUnitAccountType] [varchar](20) NULL,
	[BusinessUnitAddress] [varchar](200) NULL,
	[BusinessUnitStatus] [varchar](20) NULL,
	[LegalEntityInvoiceSetup] [varchar](128) NULL,
	[LegalEntityCustomerAddress] [varchar](512) NULL,
	[LegalEntityVendorAddress] [varchar](512) NULL,
 CONSTRAINT [PK_PartyRoleDetails] PRIMARY KEY CLUSTERED 
(
	[PartyRoleDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PartyRoleDetailsAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PartyRoleDetailsAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[PartyRoleDetailsId] [int] NULL,
	[PartyRoleDetailsClass] [varchar](50) NULL,
	[PartyRoleId] [int] NULL,
	[Name] [varchar](200) NULL,
	[BrokerPhone] [varchar](200) NULL,
	[BrokerFax] [varchar](200) NULL,
	[ExchangePhone] [varchar](200) NULL,
	[ExchangeFax] [varchar](200) NULL,
	[CounterpartyPhone] [varchar](200) NULL,
	[CounterpartyFax] [varchar](200) NULL,
	[Rate] [decimal](18, 10) NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [binary](8) NOT NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
	[ShortName] [nvarchar](20) NULL,
	[TaxLocationId] [int] NULL,
	[BusinessUnitPhone] [varchar](200) NULL,
	[BusinessUnitFax] [varchar](200) NULL,
	[LegalEntityRegisteredName] [varchar](200) NULL,
	[LegalEntityRegistrationNumber] [varchar](200) NULL,
	[LegalEntityAddress] [varchar](200) NULL,
	[LegalEntityWebsite] [varchar](200) NULL,
	[LegalEntityEmail] [varchar](200) NULL,
	[LegalEntityFax] [varchar](200) NULL,
	[LegalEntityPhone] [varchar](200) NULL,
	[LegalEntityCountryOfInc] [varchar](200) NULL,
	[LegalEntityPartyStatus] [varchar](200) NULL,
	[BusinessUnitAccountType] [varchar](20) NULL,
	[BusinessUnitAddress] [varchar](200) NULL,
	[BusinessUnitStatus] [varchar](20) NULL,
	[LegalEntityInvoiceSetup] [varchar](128) NULL,
	[LegalEntityCustomerAddress] [varchar](512) NULL,
	[LegalEntityVendorAddress] [varchar](512) NULL,
 CONSTRAINT [PK_PartyRoleDetailsAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PartyRoleMapping]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PartyRoleMapping](
	[PartyRoleMappingId] [int] IDENTITY(1,1) NOT NULL,
	[PartyRoleId] [int] NOT NULL,
	[SourceSystemId] [int] NOT NULL,
	[MappingValue] [varchar](200) NOT NULL,
	[IsMaster] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Start] [datetime] NOT NULL,
	[Finish] [datetime] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_PartyRoleMapping] PRIMARY KEY CLUSTERED 
(
	[PartyRoleMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PartyRoleMappingAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PartyRoleMappingAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[PartyRoleMappingId] [int] NULL,
	[PartyRoleId] [int] NULL,
	[SourceSystemId] [int] NULL,
	[MappingValue] [varchar](200) NULL,
	[IsMaster] [bit] NULL,
	[IsDefault] [bit] NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [binary](8) NOT NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_PartyRoleMappingAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Person]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Person](
	[PersonId] [int] IDENTITY(1,1) NOT NULL,
	[Tag] [varchar](200) NULL,
	[Version] [timestamp] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PersonAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PersonAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NULL,
	[Tag] [varchar](200) NULL,
	[Version] [varbinary](8) NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_PersonAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PersonDetails]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PersonDetails](
	[PersonDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
	[Email] [varchar](200) NULL,
	[Start] [datetime] NOT NULL,
	[Finish] [datetime] NOT NULL,
	[Version] [timestamp] NULL,
 CONSTRAINT [PK_PersonDetail] PRIMARY KEY CLUSTERED 
(
	[PersonDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PersonDetailsAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PersonDetailsAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[PersonDetailsId] [int] NULL,
	[PersonId] [int] NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Role] [varchar](50) NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [varbinary](8) NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_PersonDetailsAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PersonMapping]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PersonMapping](
	[PersonMappingId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[SourceSystemId] [int] NOT NULL,
	[MappingValue] [varchar](200) NOT NULL,
	[IsMaster] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Start] [datetime] NOT NULL,
	[Finish] [datetime] NOT NULL,
	[Version] [timestamp] NULL,
 CONSTRAINT [PK_PersonMapping] PRIMARY KEY CLUSTERED 
(
	[PersonMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PersonMappingAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PersonMappingAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[PersonMappingId] [int] NULL,
	[PersonId] [int] NULL,
	[SourceSystemId] [int] NULL,
	[MappingValue] [varchar](200) NULL,
	[IsMaster] [bit] NULL,
	[IsDefault] [bit] NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [varbinary](8) NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_PersonMappingAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PersonRole]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PersonRole](
	[PersonRoleId] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[RoleName] [varchar](200) NOT NULL,
	[Start] [datetime] NOT NULL,
	[Finish] [datetime] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_PersonRole] PRIMARY KEY CLUSTERED 
(
	[PersonRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PersonRoleAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PersonRoleAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[PersonRoleId] [int] NULL,
	[PersonId] [int] NULL,
	[RoleName] [varchar](200) NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [binary](8) NOT NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_PersonRoleAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReferenceData]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferenceData](
	[ReferenceDataId] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceKey] [nvarchar](200) NOT NULL,
	[Value] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_ReferenceData] PRIMARY KEY CLUSTERED 
(
	[ReferenceDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReferenceDataAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReferenceDataAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceDataId] [int] NULL,
	[ReferenceKey] [nvarchar](200) NULL,
	[Value] [nvarchar](200) NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_ReferenceDataAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SourceSystem]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SourceSystem](
	[SourceSystemId] [int] IDENTITY(1,1) NOT NULL,
	[ParentSourceSystemId] [int] NULL,
	[SourceSystemName] [varchar](200) NOT NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_SourceSystem] PRIMARY KEY CLUSTERED 
(
	[SourceSystemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [CK_SourceSystem] UNIQUE NONCLUSTERED 
(
	[SourceSystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SourceSystemAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SourceSystemAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[SourceSystemId] [int] NULL,
	[SourceSystemName] [varchar](200) NULL,
	[ParentSourceSystemId] [int] NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [binary](8) NOT NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_SourceSystemAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SourceSystemMapping]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SourceSystemMapping](
	[SourceSystemMappingId] [int] IDENTITY(1,1) NOT NULL,
	[SystemId] [int] NOT NULL,
	[SourceSystemId] [int] NOT NULL,
	[MappingValue] [varchar](200) NOT NULL,
	[IsMaster] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Start] [datetime] NOT NULL,
	[Finish] [datetime] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_SourceSystemMapping] PRIMARY KEY CLUSTERED 
(
	[SourceSystemMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SourceSystemMappingAudit]    Script Date: 25/03/2014 11:53:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SourceSystemMappingAudit](
	[AuditId] [int] IDENTITY(1,1) NOT NULL,
	[SourceSystemMappingId] [int] NULL,
	[SystemId] [int] NULL,
	[SourceSystemId] [int] NULL,
	[MappingValue] [varchar](200) NULL,
	[IsMaster] [bit] NULL,
	[IsDefault] [bit] NULL,
	[Start] [datetime] NULL,
	[Finish] [datetime] NULL,
	[Version] [binary](8) NOT NULL,
	[AuditAction] [char](1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[AuditUser] [varchar](50) NOT NULL,
	[AuditApp] [varchar](128) NULL,
 CONSTRAINT [PK_SourceSystemMappingAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[LocationAudit] ADD  CONSTRAINT [DF_LocationAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[LocationAudit] ADD  CONSTRAINT [DF_LocationAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[LocationAudit] ADD  CONSTRAINT [DF_LocationAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[LocationMappingAudit] ADD  CONSTRAINT [DF_LocationMappingAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[LocationMappingAudit] ADD  CONSTRAINT [DF_LocationMappingAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[LocationMappingAudit] ADD  CONSTRAINT [DF_LocationMappingAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[PartyDetails] ADD  CONSTRAINT [DF__PartyDeta__IsInt__6B6FCE9C]  DEFAULT ((0)) FOR [IsInternal]
GO
ALTER TABLE [dbo].[PartyDetailsAudit] ADD  CONSTRAINT [DF_PartyDetailsAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[PartyDetailsAudit] ADD  CONSTRAINT [DF_PartyDetailsAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[PartyDetailsAudit] ADD  CONSTRAINT [DF_PartyDetailsAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[PartyMappingAudit] ADD  CONSTRAINT [DF_PartyMappingAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[PartyMappingAudit] ADD  CONSTRAINT [DF_PartyMappingAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[PartyMappingAudit] ADD  CONSTRAINT [DF_PartyMappingAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[PartyRoleAudit] ADD  CONSTRAINT [DF_PartyRoleAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[PartyRoleAudit] ADD  CONSTRAINT [DF_PartyRoleAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[PartyRoleAudit] ADD  CONSTRAINT [DF_PartyRoleAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[PartyRoleDetailsAudit] ADD  CONSTRAINT [DF_PartyRoleDetailsAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[PartyRoleDetailsAudit] ADD  CONSTRAINT [DF_PartyRoleDetailsAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[PartyRoleDetailsAudit] ADD  CONSTRAINT [DF_PartyRoleDetailsAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[PartyRoleMappingAudit] ADD  CONSTRAINT [DF_PartyRoleMappingAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[PartyRoleMappingAudit] ADD  CONSTRAINT [DF_PartyRoleMappingAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[PartyRoleMappingAudit] ADD  CONSTRAINT [DF_PartyRoleMappingAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[PersonAudit] ADD  CONSTRAINT [DF_PersonAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[PersonAudit] ADD  CONSTRAINT [DF_PersonAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[PersonAudit] ADD  CONSTRAINT [DF_PersonAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[PersonDetailsAudit] ADD  CONSTRAINT [DF_PersonDetailsAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[PersonDetailsAudit] ADD  CONSTRAINT [DF_PersonDetailsAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[PersonDetailsAudit] ADD  CONSTRAINT [DF_PersonDetailsAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[PersonMappingAudit] ADD  CONSTRAINT [DF_PersonMappingAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[PersonMappingAudit] ADD  CONSTRAINT [DF_PersonMappingAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[PersonMappingAudit] ADD  CONSTRAINT [DF_PersonMappingAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[PersonRoleAudit] ADD  CONSTRAINT [DF_PersonRoleAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[PersonRoleAudit] ADD  CONSTRAINT [DF_PersonRoleAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[PersonRoleAudit] ADD  CONSTRAINT [DF_PersonRoleAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[ReferenceDataAudit] ADD  CONSTRAINT [DF_ReferenceDataAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[ReferenceDataAudit] ADD  CONSTRAINT [DF_ReferenceDataAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[ReferenceDataAudit] ADD  CONSTRAINT [DF_ReferenceDataAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[SourceSystemAudit] ADD  CONSTRAINT [DF_SourceSystemAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[SourceSystemAudit] ADD  CONSTRAINT [DF_SourceSystemAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[SourceSystemAudit] ADD  CONSTRAINT [DF_SourceSystemAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[SourceSystemMappingAudit] ADD  CONSTRAINT [DF_SourceSystemMappingAudit_AuditDate]  DEFAULT (getdate()) FOR [AuditDate]
GO
ALTER TABLE [dbo].[SourceSystemMappingAudit] ADD  CONSTRAINT [DF_SourceSystemMappingAudit_AuditUser]  DEFAULT (suser_sname()) FOR [AuditUser]
GO
ALTER TABLE [dbo].[SourceSystemMappingAudit] ADD  CONSTRAINT [DF_SourceSystemMappingAudit_AuditApp]  DEFAULT (('App=('+rtrim(isnull(app_name(),'')))+') ') FOR [AuditApp]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Location] FOREIGN KEY([ParentLocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Location]
GO
ALTER TABLE [dbo].[LocationMapping]  WITH CHECK ADD  CONSTRAINT [FK_LocationMapping_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[LocationMapping] CHECK CONSTRAINT [FK_LocationMapping_Location]
GO
ALTER TABLE [dbo].[LocationMapping]  WITH CHECK ADD  CONSTRAINT [FK_LocationMapping_SourceSystem] FOREIGN KEY([SourceSystemId])
REFERENCES [dbo].[SourceSystem] ([SourceSystemId])
GO
ALTER TABLE [dbo].[LocationMapping] CHECK CONSTRAINT [FK_LocationMapping_SourceSystem]
GO
ALTER TABLE [dbo].[PartyDetails]  WITH CHECK ADD  CONSTRAINT [FK_PartyDetails_Party] FOREIGN KEY([PartyId])
REFERENCES [dbo].[Party] ([PartyId])
GO
ALTER TABLE [dbo].[PartyDetails] CHECK CONSTRAINT [FK_PartyDetails_Party]
GO
ALTER TABLE [dbo].[PartyMapping]  WITH NOCHECK ADD  CONSTRAINT [FK_PartyMapping_Party] FOREIGN KEY([PartyId])
REFERENCES [dbo].[Party] ([PartyId])
GO
ALTER TABLE [dbo].[PartyMapping] CHECK CONSTRAINT [FK_PartyMapping_Party]
GO
ALTER TABLE [dbo].[PartyMapping]  WITH CHECK ADD  CONSTRAINT [FK_PartyMapping_SourceSystem] FOREIGN KEY([SourceSystemId])
REFERENCES [dbo].[SourceSystem] ([SourceSystemId])
GO
ALTER TABLE [dbo].[PartyMapping] CHECK CONSTRAINT [FK_PartyMapping_SourceSystem]
GO
ALTER TABLE [dbo].[PartyRole]  WITH CHECK ADD  CONSTRAINT [FK_PartyRole_Party] FOREIGN KEY([PartyId])
REFERENCES [dbo].[Party] ([PartyId])
GO
ALTER TABLE [dbo].[PartyRole] CHECK CONSTRAINT [FK_PartyRole_Party]
GO
ALTER TABLE [dbo].[PartyRoleDetails]  WITH CHECK ADD  CONSTRAINT [FK_PartyRoleDetails_Location] FOREIGN KEY([TaxLocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[PartyRoleDetails] CHECK CONSTRAINT [FK_PartyRoleDetails_Location]
GO
ALTER TABLE [dbo].[PartyRoleDetails]  WITH CHECK ADD  CONSTRAINT [FK_PartyRoleDetails_PartyRole] FOREIGN KEY([PartyRoleId])
REFERENCES [dbo].[PartyRole] ([PartyRoleId])
GO
ALTER TABLE [dbo].[PartyRoleDetails] CHECK CONSTRAINT [FK_PartyRoleDetails_PartyRole]
GO
ALTER TABLE [dbo].[PartyRoleMapping]  WITH NOCHECK ADD  CONSTRAINT [FK_PartyRoleMapping_PartyRole] FOREIGN KEY([PartyRoleId])
REFERENCES [dbo].[PartyRole] ([PartyRoleId])
GO
ALTER TABLE [dbo].[PartyRoleMapping] CHECK CONSTRAINT [FK_PartyRoleMapping_PartyRole]
GO
ALTER TABLE [dbo].[PartyRoleMapping]  WITH CHECK ADD  CONSTRAINT [FK_PartyRoleMapping_SourceSystem] FOREIGN KEY([SourceSystemId])
REFERENCES [dbo].[SourceSystem] ([SourceSystemId])
GO
ALTER TABLE [dbo].[PartyRoleMapping] CHECK CONSTRAINT [FK_PartyRoleMapping_SourceSystem]
GO
ALTER TABLE [dbo].[PersonDetails]  WITH CHECK ADD  CONSTRAINT [FK_PersonDetail_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[PersonDetails] CHECK CONSTRAINT [FK_PersonDetail_Person]
GO
ALTER TABLE [dbo].[PersonMapping]  WITH CHECK ADD  CONSTRAINT [FK_PersonMapping_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[PersonMapping] CHECK CONSTRAINT [FK_PersonMapping_Person]
GO
ALTER TABLE [dbo].[PersonMapping]  WITH CHECK ADD  CONSTRAINT [FK_PersonMapping_SourceSystem] FOREIGN KEY([SourceSystemId])
REFERENCES [dbo].[SourceSystem] ([SourceSystemId])
GO
ALTER TABLE [dbo].[PersonMapping] CHECK CONSTRAINT [FK_PersonMapping_SourceSystem]
GO
ALTER TABLE [dbo].[PersonRole]  WITH NOCHECK ADD  CONSTRAINT [FK_PersonRole_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([PersonId])
GO
ALTER TABLE [dbo].[PersonRole] CHECK CONSTRAINT [FK_PersonRole_Person]
GO
ALTER TABLE [dbo].[SourceSystem]  WITH CHECK ADD  CONSTRAINT [FK_SourceSystem_SourceSystem] FOREIGN KEY([ParentSourceSystemId])
REFERENCES [dbo].[SourceSystem] ([SourceSystemId])
GO
ALTER TABLE [dbo].[SourceSystem] CHECK CONSTRAINT [FK_SourceSystem_SourceSystem]
GO
