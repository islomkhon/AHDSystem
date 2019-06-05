--CREATE TABLE [NueRequestType]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--    [RequestType] VARCHAR(250),
--    [AddedOn] DATE NOT NULL DEFAULT GETDATE()
--)

--CREATE TABLE [NueRequestSubType]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--    [RequestSubType] VARCHAR(250),
--    [RequestType] INT FOREIGN KEY REFERENCES NueRequestType(Id),
--    [AddedOn] DATE NOT NULL DEFAULT GETDATE()
--)

--CREATE TABLE [NueRequestStatus]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--    [RequestStatus] VARCHAR(350),
--    [AddedOn] DATE NOT NULL DEFAULT GETDATE()
--)

--CREATE TABLE [NueUserOrgMapper]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--    [UserId] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--	[OrgUserId] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--	[OrgUserType] INT FOREIGN KEY REFERENCES Designation(Id),
--    [AddedOn] DATE NOT NULL DEFAULT GETDATE(),
--)


--CREATE TABLE [NueRequestMaster]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--    [RequestId] VARCHAR(350),
--	[IsApprovalProcess] INT DEFAULT 0,
--	[CreatedBy] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--	[RequestStatus] INT FOREIGN KEY REFERENCES NueRequestStatus(Id),
--	[PayloadId] INT,
--    [RequestCatType] INT FOREIGN KEY REFERENCES NueRequestSubType(Id),
--    [AddedOn] DATE NOT NULL DEFAULT GETDATE(),
--  [ModifiedOn] DATE NOT NULL DEFAULT GETDATE()
--)

--CREATE TABLE [NueRequestAceessLog]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--    [RequestId] INT FOREIGN KEY REFERENCES NueRequestMaster(Id),
--	[UserId] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--	[Completed] INT DEFAULT 0,
--	[OwnerId] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--    [AddedOn] DATE NOT NULL DEFAULT GETDATE(),
--	[ModifiedOn] DATE NOT NULL DEFAULT GETDATE()
--)

--CREATE TABLE [NueLeaveCancelationRequest]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--    [RequestId] VARCHAR(350),
--	[UserId] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--	[StartDate] VARCHAR(250),
--	[EndDate] VARCHAR(250),
--	[Message] TEXT,
--  [AddedOn] DATE NOT NULL DEFAULT GETDATE(),
--	[ModifiedOn] DATE NOT NULL DEFAULT GETDATE()
--)

--TRUNCATE TABLE NueRequestAceessLog;
--TRUNCATE TABLE NueLeaveCancelationRequest;
--TRUNCATE TABLE NueRequestMaster;

--CREATE TABLE [NueManagerMapper]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--	[UserId] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--	[ManagerId] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--	[IsConsultentManager] INT DEFAULT 0,
--    [AddedOn] DATE NOT NULL DEFAULT GETDATE(),
--	[ModifiedOn] DATE NOT NULL DEFAULT GETDATE()
--)


--CREATE TABLE [NueRequestActivityMaster]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--    [ActivityDesc] VARCHAR(350),
--    [AddedOn] DATE NOT NULL DEFAULT GETDATE()
--)

--CREATE TABLE [NueRequestActivity]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--	[Payload] TEXT,
--	[PayloadType] INT FOREIGN KEY REFERENCES NueRequestActivityMaster(Id),
--	[UserId] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--	[RequestId] INT FOREIGN KEY REFERENCES NueRequestMaster(Id),
--	[Request] varchar(350),
--    [AddedOn] DATE NOT NULL DEFAULT GETDATE(),
--	[ModifiedOn] DATE NOT NULL DEFAULT GETDATE()
--)

--CREATE TABLE [NueRequestAttachmentLog]
--(
--    [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--	[RequestId] INT FOREIGN KEY REFERENCES NueRequestMaster(Id),
--	[Request] varchar(350),
--	[UserId] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--	[OwnerId] INT FOREIGN KEY REFERENCES NueUserProfile(Id),
--	[FileName] varchar(1050),
--	[FileExt] varchar(350),
--	[VFileName] varchar(2050),
--    [AddedOn] datetime,
--	[ModifiedOn] datetime
--)

select nrm.Id as NueRequestMasterId, nrm.RequestId as RequestId, nrst.Id as NueRequestSubTypeId, nrst.RequestSubType as RequestSubType, 
nrs.Id as NueRequestStatusId, nrs.RequestStatus as RequestStatus, nrm.AddedOn as AddedOn, nrm.ModifiedOn as ModifiedOn from
NueRequestMaster nrm 
join NueRequestAceessLog nral on nrm.Id = nral.RequestId
join NueRequestSubType nrst on nrm.RequestCatType = nrst.id
join NueRequestType nrt on nrst.RequestType = nrt.Id
join NueRequestStatus nrs on nrm.RequestStatus = nrs.Id
where nral.Completed = 0 
and nral.UserId = 1 
and nral.OwnerId=1
and nrt.RequestType='HCM'