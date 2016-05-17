USE RockawayWish
GO

-- ********* DROP FOREIGN KEYS
IF OBJECT_ID('FK_Dues_UsersDues_DuesId') IS NOT NULL
	ALTER TABLE UsersDues DROP CONSTRAINT FK_Dues_UsersDues_DuesId;
GO
IF OBJECT_ID('FK_UsersDues_PaymentTypes_PaymentTypeId') IS NOT NULL
	ALTER TABLE UsersDues DROP CONSTRAINT FK_UsersDues_PaymentTypes_PaymentTypeId;
GO
IF OBJECT_ID('FK_Events_EventLocation_EventLocationId') IS NOT NULL
	ALTER TABLE Events DROP CONSTRAINT FK_Events_EventLocation_EventLocationId;
GO
IF OBJECT_ID('FK_EventLocations_States_StateId') IS NOT NULL
	ALTER TABLE EventLocations DROP CONSTRAINT FK_EventLocations_States_StateId;
GO
IF OBJECT_ID('FK_ContactUs_States_StateId') IS NOT NULL
	ALTER TABLE ContactUs DROP CONSTRAINT FK_ContactUs_States_StateId;
GO



-- ******** TABLE EventLocation PRIMARY KEY
IF OBJECT_ID('States') IS NOT NULL
	DROP TABLE States;
GO

CREATE TABLE States
(
	StateId				INT IDENTITY(1001, 1) NOT NULL,
	StateName			VARCHAR(100) NOT NULL,
	StateAbbr			VARCHAR(2) NOT NULL,
	create_dt			DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- ******** STATES PRIMARY KEY
ALTER TABLE States ADD CONSTRAINT PK_States PRIMARY KEY (StateId);
GO
-- ******** STATES UNIQUE INDEX TO AVOId DUPLICATE EventLocation
IF OBJECT_ID('UDX_States_StateName') IS NOT NULL
	DROP INDEX State.UDX_States_StateName;
GO
CREATE UNIQUE INDEX UDX_State_StateName ON States(StateName);
GO
-- ******** State SAMPLE DATA
INSERT INTO States (StateName, StateAbbr) VALUES ('Alabama', 'AL');
INSERT INTO States (StateName, StateAbbr) VALUES ('Alaska', 'AK');
INSERT INTO States (StateName, StateAbbr) VALUES ('Arizona', 'AZ');
INSERT INTO States (StateName, StateAbbr) VALUES ('Arkansas', 'AR');
INSERT INTO States (StateName, StateAbbr) VALUES ('California', 'CA');
INSERT INTO States (StateName, StateAbbr) VALUES ('Colorado', 'CO');
INSERT INTO States (StateName, StateAbbr) VALUES ('Connecticut', 'CT');
INSERT INTO States (StateName, StateAbbr) VALUES ('Delaware', 'DE');
INSERT INTO States (StateName, StateAbbr) VALUES ('District of Columbia', 'DC');
INSERT INTO States (StateName, StateAbbr) VALUES ('Florida', 'FL');
INSERT INTO States (StateName, StateAbbr) VALUES ('Georgia', 'GA');
INSERT INTO States (StateName, StateAbbr) VALUES ('Hawaii', 'HI');
INSERT INTO States (StateName, StateAbbr) VALUES ('Idaho', 'ID');
INSERT INTO States (StateName, StateAbbr) VALUES ('Illinois', 'IL');
INSERT INTO States (StateName, StateAbbr) VALUES ('Indiana', 'IN');
INSERT INTO States (StateName, StateAbbr) VALUES ('Iowa', 'IA');
INSERT INTO States (StateName, StateAbbr) VALUES ('Kansas', 'KS');
INSERT INTO States (StateName, StateAbbr) VALUES ('Kentucky', 'KY');
INSERT INTO States (StateName, StateAbbr) VALUES ('Louisiana', 'LA');
INSERT INTO States (StateName, StateAbbr) VALUES ('Maine', 'ME');
INSERT INTO States (StateName, StateAbbr) VALUES ('Maryland', 'MD');
INSERT INTO States (StateName, StateAbbr) VALUES ('Massachusetts', 'MA');
INSERT INTO States (StateName, StateAbbr) VALUES ('Michigan', 'MI');
INSERT INTO States (StateName, StateAbbr) VALUES ('Minnesota', 'MN');
INSERT INTO States (StateName, StateAbbr) VALUES ('Mississippi', 'MS');
INSERT INTO States (StateName, StateAbbr) VALUES ('Missouri', 'MO');
INSERT INTO States (StateName, StateAbbr) VALUES ('Montana', 'MT');
INSERT INTO States (StateName, StateAbbr) VALUES ('Nebraska', 'NE');
INSERT INTO States (StateName, StateAbbr) VALUES ('Nevada', 'NV');
INSERT INTO States (StateName, StateAbbr) VALUES ('New Hampshire', 'NH');
INSERT INTO States (StateName, StateAbbr) VALUES ('New Jersey', 'NJ');
INSERT INTO States (StateName, StateAbbr) VALUES ('New Mexico', 'NM');
INSERT INTO States (StateName, StateAbbr) VALUES ('New York', 'NY');
INSERT INTO States (StateName, StateAbbr) VALUES ('North Carolina', 'NC');
INSERT INTO States (StateName, StateAbbr) VALUES ('North Dakota', 'ND');
INSERT INTO States (StateName, StateAbbr) VALUES ('Ohio', 'OH');
INSERT INTO States (StateName, StateAbbr) VALUES ('Oklahoma', 'OK');
INSERT INTO States (StateName, StateAbbr) VALUES ('Oregon', 'OR');
INSERT INTO States (StateName, StateAbbr) VALUES ('Pennsylvania', 'PA');
INSERT INTO States (StateName, StateAbbr) VALUES ('Puerto Rico', 'PR');
INSERT INTO States (StateName, StateAbbr) VALUES ('South Carolina', 'SC');
INSERT INTO States (StateName, StateAbbr) VALUES ('South Dakota', 'SD');
INSERT INTO States (StateName, StateAbbr) VALUES ('Tennessee', 'TN');
INSERT INTO States (StateName, StateAbbr) VALUES ('Texas', 'TX');
INSERT INTO States (StateName, StateAbbr) VALUES ('Utah', 'UT');
INSERT INTO States (StateName, StateAbbr) VALUES ('Vermont', 'VT');
INSERT INTO States (StateName, StateAbbr) VALUES ('Virginia', 'VA');
INSERT INTO States (StateName, StateAbbr) VALUES ('Washington', 'WA');
INSERT INTO States (StateName, StateAbbr) VALUES ('West Virgina', 'WV');
INSERT INTO States (StateName, StateAbbr) VALUES ('Wisconsin', 'WI');
INSERT INTO States (StateName, StateAbbr) VALUES ('Wyoming', 'WY');
INSERT INTO States (StateName, StateAbbr) VALUES ('Rhode Island', 'RI');
INSERT INTO States (StateName, StateAbbr) VALUES ('U.S. Armed Forces – Europe', 'AE');
INSERT INTO States (StateName, StateAbbr) VALUES ('Virgin Islands', 'VI');
INSERT INTO States (StateName, StateAbbr) VALUES ('Guam', 'GU');
INSERT INTO States (StateName, StateAbbr) VALUES ('American Samoa', 'AS');
INSERT INTO States (StateName, StateAbbr) VALUES ('Palau', 'PW');
INSERT INTO States (StateName, StateAbbr) VALUES ('Federated States of Micronesia', 'FM');
INSERT INTO States (StateName, StateAbbr) VALUES ('Northern Mariana Islands', 'MP');
INSERT INTO States (StateName, StateAbbr) VALUES ('Marshall Islands', 'MH');
INSERT INTO States (StateName, StateAbbr) VALUES ('U.S. Armed Forces – Pacific', 'AP');
INSERT INTO States (StateName, StateAbbr) VALUES ('U.S. Armed Forces – Americas', 'AA');

GO

---- ******** CHECK IF create_dt EXISTS ON ASPNetUsers TABLE
--IF NOT EXISTS(SELECT * FROM sys.columns WHERE [Name] = N'create_dt' AND OBJECT_ID = OBJECT_ID(N'ASPNetUsers'))
--	ALTER TABLE ASPNetUsers ADD create_dt DATETIME DEFAULT GETDATE() NOT NULL;
--GO


-- ******** TABLE DUES PRIMARY KEY
IF OBJECT_ID('Dues') IS NOT NULL
	DROP TABLE Dues;
GO

CREATE TABLE Dues
(
	DuesId			INT IDENTITY(1001, 1) NOT NULL,
	DuesYear		INT NOT NULL,
	DuesAmount		DECIMAL(9,2) NOT NULL,
	create_dt		DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- ******** DUES PRIMARY KEY
ALTER TABLE Dues ADD CONSTRAINT PK_Dues PRIMARY KEY (DuesId);
GO


-- ******** DUES UNIQUE INDEX TO AVOId DUPLICATE YEARS
IF OBJECT_ID('UDX_Dues_DuesYear') IS NOT NULL
	DROP INDEX Dues.UDX_Dues_DuesYear;
GO

CREATE UNIQUE INDEX UDX_Dues_DuesYear ON Dues(DuesYear);
GO


-- ******** DUES SAMPLE DATA
INSERT INTO Dues (DuesYear, DuesAmount) VALUES (2015, 100);
INSERT INTO Dues (DuesYear, DuesAmount) VALUES (2016, 125);
GO















-- ******** TABLE PaymentType PRIMARY KEY
IF OBJECT_ID('PaymentTypes') IS NOT NULL
	DROP TABLE PaymentTypes;
GO

CREATE TABLE PaymentTypes
(
	PaymentTypeId			INT IDENTITY(1001, 1) NOT NULL,
	PaymentTypeName			VARCHAR(50) NOT NULL,
	create_dt				DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- ******** PaymentTypes PRIMARY KEY
ALTER TABLE PaymentTypes ADD CONSTRAINT PK_PaymentType PRIMARY KEY (PaymentTypeId);
GO
-- ******** PaymentType UNIQUE INDEX TO AVOId DUPLICATE PaymentType
IF OBJECT_ID('UDX_PaymentTypes_PaymentTypeName') IS NOT NULL
	DROP INDEX PaymentTypes.UDX_PaymentType_PaymentTypeName;
GO
CREATE UNIQUE INDEX UDX_PaymentTypes_PaymentTypeName ON PaymentTypes(PaymentTypeName);
GO

-- ******** PaymentType SAMPLE DATA
INSERT INTO PaymentTypes (PaymentTypeName) VALUES ('Cash');
INSERT INTO PaymentTypes (PaymentTypeName) VALUES ('Master Card');
INSERT INTO PaymentTypes (PaymentTypeName) VALUES ('Visa');






-- ******** TABLE UsersDues PRIMARY KEY
IF OBJECT_ID('UsersDues') IS NOT NULL
	DROP TABLE UsersDues;
GO

CREATE TABLE UsersDues
(
	UsersDuesId			INT IDENTITY(1001, 1) NOT NULL,
	UserId					UNIQUEIDENTIFIER NOT NULL,
	DuesId				INT NOT NULL,
	PaymentTypeId		INT NOT NULL,
	DuesPaidDate		DATETIME NOT NULL,
	create_dt			DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- ******** DUES PRIMARY KEY
ALTER TABLE UsersDues ADD CONSTRAINT PK_UsersDues PRIMARY KEY (UsersDuesId);
GO
-- ******** FOREIGN KEYS ( UsersDues(DuesId) --> Dues(DuesId)
ALTER TABLE UsersDues ADD CONSTRAINT FK_Dues_UsersDues_DuesId FOREIGN KEY (DuesId) REFERENCES Dues(DuesId);
GO
-- ******** FOREIGN KEYS ( UsersDues(PaymentTypeId) --> PaymentTypes(PaymentTypeId)
ALTER TABLE UsersDues ADD CONSTRAINT FK_UsersDues_PaymentTypes_PaymentTypeId FOREIGN KEY (PaymentTypeId) REFERENCES PaymentTypes(PaymentTypeId);
GO
-- ******** DUES UNIQUE INDEX TO AVOId DUPLICATE UsersDues
IF OBJECT_ID('UDX_Dues_DuesYear') IS NOT NULL
	DROP INDEX UsersDues.UDX_UsersDues_Id_DuesId;
GO
CREATE UNIQUE INDEX UDX_UsersDues_Id_DuesId ON UsersDues(UserId, DuesId);
GO

-- ******** UsersDues SAMPLE DATA
DECLARE @DuesId INT;
SELECT TOP 1 @DuesId = DuesId FROM Dues;
DECLARE @UserId UNIQUEIDENTIFIER;
--SELECT TOP 1 @UserId = UserId FROM MembershipDB..Users;
SET @UserId = 'b2fda7f2-b07b-4466-8e88-9cdca4d3fd5e';
DECLARE @PaymentTypeId INT;
SELECT TOP 1 @PaymentTypeId = PaymentTypeId FROM PaymentTypes;

INSERT INTO UsersDues (UserId, DuesId, PaymentTypeId, DuesPaidDate) VALUES (@UserId, @DuesId, @PaymentTypeId, GETDATE());
GO






-- ******** TABLE EventLocation PRIMARY KEY
IF OBJECT_ID('EventLocations') IS NOT NULL
	DROP TABLE EventLocations;
GO

CREATE TABLE EventLocations
(
	EventLocationId			INT IDENTITY(1001, 1) NOT NULL,
	EventLocationName		VARCHAR(100) NOT NULL,
	EventLocationAddress	VARCHAR(100) NOT NULL,
	EventLocationAddress2	VARCHAR(100) NULL,
	EventLocationCity		VARCHAR(100) NOT NULL,
	EventLocationStateId	INT NOT NULL,
	EventLocationZipCode	VARCHAR(20) NOT NULL,
	create_dt				DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- ******** DUES PRIMARY KEY
ALTER TABLE EventLocations ADD CONSTRAINT PK_EventLocations PRIMARY KEY (EventLocationId);
GO
-- ******** FOREIGN KEYS ( EventLocation(StateId) --> States(StateId)
ALTER TABLE EventLocations ADD CONSTRAINT FK_EventLocations_States_StateId FOREIGN KEY (EventLocationStateId) REFERENCES States(StateId);
GO
-- ******** DUES UNIQUE INDEX TO AVOId DUPLICATE EventLocation
IF OBJECT_ID('UDX_EventLocations_EventLocationName') IS NOT NULL
	DROP INDEX EventLocations.EventLocationName;
GO
CREATE UNIQUE INDEX UDX_EventLocations_EventLocationName ON EventLocations(EventLocationName);
GO

INSERT INTO EventLocations 
	(EventLocationName, EventLocationAddress, EventLocationAddress2, EventLocationCity, EventLocationStateId, EventLocationZipCode)
VALUES
	('Richie''s House', '102-00 Shore Front Parkway', '#4J', 'Rockaway Park', 1033, '07640');




-- ******** TABLE Events PRIMARY KEY
IF OBJECT_ID('Events') IS NOT NULL
	DROP TABLE Events;
GO

CREATE TABLE Events
(
	EventId				INT IDENTITY(1001, 1) NOT NULL,
	EventLocationId		INT NOT NULL,
	EventDateTime		DATETIME NOT NULL,
	EventDescription	VARCHAR(MAX) NOT NULL,
	create_dt			DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- ******** EVENTS PRIMARY KEY
ALTER TABLE Events ADD CONSTRAINT PK_Events PRIMARY KEY (EventId);
GO
-- ******** FOREIGN KEYS ( Events(EventLocationId) --> EventLocation(EventLocationId)
ALTER TABLE Events ADD CONSTRAINT FK_Events_EventLocation_EventLocationId FOREIGN KEY (EventLocationId) REFERENCES EventLocations(EventLocationId);
GO
-- ******** DUES UNIQUE INDEX TO AVOId DUPLICATE EventLocation
IF OBJECT_ID('UDX_EventLocations_EventLocationName') IS NOT NULL
	DROP INDEX EventLocations.EventLocationName;
GO
CREATE UNIQUE INDEX UDX_EventLocations_EventLocationId_EventDateTime ON Events(EventLocationId, EventDateTime);
GO

-- ******** EVENTS SAMPLE DATAT
DECLARE @EventLocationId INT;
SELECT TOP 1 @EventLocationId = EventLocationId FROM EventLocations;
INSERT INTO EventS (EventLocationId, EventDateTime, EventDescription) VALUES (@EventLocationId, GETDATE(), 'RockawayWish first event');








-- ******** TABLE EventLocation PRIMARY KEY
IF OBJECT_ID('ContactUs') IS NOT NULL
	DROP TABLE ContactUs;
GO

CREATE TABLE ContactUs
(
	ContactUsId			INT IDENTITY(1001, 1) NOT NULL,
	ContactUsName		VARCHAR(100) NOT NULL,
	ContactUsAddress	VARCHAR(100) NOT NULL,
	ContactUsAddress2	VARCHAR(100) NULL,
	ContactUsCity		VARCHAR(100) NOT NULL,
	ContactUsStateId	INT NOT NULL,
	ContactUsZipCode	VARCHAR(20) NOT NULL,
	ContactUsPhone		VARCHAR(20) NULL,
	ContactUsFax		VARCHAR(20) NULL,
	create_dt			DATETIME DEFAULT GETDATE() NOT NULL
);
GO

-- ******** CONTACTUS PRIMARY KEY
ALTER TABLE ContactUs ADD CONSTRAINT PK_ContactUs PRIMARY KEY (ContactUsId);
GO
-- ******** FOREIGN KEYS ( ContactUs(ContactUsStateId) --> States(StateId)
ALTER TABLE ContactUs ADD CONSTRAINT FK_ContactUs_States_StateId FOREIGN KEY (ContactUsStateId) REFERENCES States(StateId);
GO
-- ******** CONTACTUS UNIQUE INDEX TO AVOId DUPLICATE EventLocation
IF OBJECT_ID('UDX_ContactUs_ContactUsName') IS NOT NULL
	DROP INDEX ContactUs.UDX_ContactUs_ContactUsName;
GO
CREATE UNIQUE INDEX UDX_ContactUs_ContactUsName ON ContactUs(ContactUsName);
GO
-- ******** CONTACTUS SAMPLE DATA
INSERT INTO ContactUs 
	(ContactUsName, ContactUsAddress, ContactUsAddress2, ContactUsCity, ContactUsStateId, ContactUsZipCode, ContactUsPhone, ContactUsFax)
VALUES
	('Richie''s House', '102-00 Shore Front Parkway', '#4J', 'Rockaway Park', 1033, '07640', '(999) 999-9999', '(999) 999-9999');




SELECT * FROM ASPNetUsers;
SELECT * FROM Dues;
SELECT * FROM PaymentTypes;
SELECT * FROM UsersDues;
SELECT * FROM EventLocations;
SELECT * FROM Events;
SELECT * FROM ContactUs;
SELECT * FROM States;

