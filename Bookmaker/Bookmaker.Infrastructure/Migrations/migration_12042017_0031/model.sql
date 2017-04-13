CREATE TABLE [dbo].[Bets] (
    [Id]         INT      NOT NULL,
    [UserId]     INT      NOT NULL,
    [MatchId]    INT      NOT NULL,
    [TeamId]     INT      NOT NULL,
    [Price]      INT      NOT NULL,
    [ScoreId]    INT      NOT NULL,
    [CreatedAt]  DATETIME NOT NULL,
    [LastUpdate] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([MatchId]) REFERENCES [dbo].[Matches] ([Id]),
    FOREIGN KEY ([ScoreId]) REFERENCES [dbo].[Scores] ([Id]),
    FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

GO
CREATE TABLE [dbo].[Cities] (
    [Id]        INT            NOT NULL,
    [Name]      NVARCHAR (200) NOT NULL,
    [CountryId] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id])
);

GO
CREATE TABLE [dbo].[Countries] (
    [Id]   INT            NOT NULL,
    [Name] NVARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE TABLE [dbo].[Matches] (
    [Id]          INT      NOT NULL,
    [HostTeamId]  INT      NOT NULL,
    [GuestTeamId] INT      NOT NULL,
    [StadiumId]   INT      NOT NULL,
    [StartTime]   DATETIME NOT NULL,
    [ResultId]    INT      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([GuestTeamId]) REFERENCES [dbo].[Teams] ([Id]),
    FOREIGN KEY ([HostTeamId]) REFERENCES [dbo].[Teams] ([Id]),
    FOREIGN KEY ([ResultId]) REFERENCES [dbo].[Results] ([Id]),
    FOREIGN KEY ([StadiumId]) REFERENCES [dbo].[Stadiums] ([Id])
);

GO
CREATE TABLE [dbo].[Results] (
    [Id]           INT NOT NULL,
    [HostScoreId]  INT NOT NULL,
    [GuestScoreId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([GuestScoreId]) REFERENCES [dbo].[Scores] ([Id]),
    FOREIGN KEY ([HostScoreId]) REFERENCES [dbo].[Scores] ([Id])
);

GO
CREATE TABLE [dbo].[Scores] (
    [Id]    INT NOT NULL,
    [Goals] INT NULL,
    [Shots] INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE TABLE [dbo].[Stadiums] (
    [Id]        INT            NOT NULL,
    [Name]      NVARCHAR (200) NOT NULL,
    [CountryId] INT            NOT NULL,
    [CityId]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id])
);

GO
CREATE TABLE [dbo].[Teams] (
    [Id]        INT            NOT NULL,
    [StadiumId] INT            NOT NULL,
    [Name]      NVARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([StadiumId]) REFERENCES [dbo].[Stadiums] ([Id])
);

GO
CREATE TABLE [dbo].[Users] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Email]      NVARCHAR (200) NOT NULL,
    [Password]   NVARCHAR (200) NOT NULL,
    [Salt]       NVARCHAR (40)  NOT NULL,
    [Username]   NVARCHAR (200) NOT NULL,
    [FullName]   NVARCHAR (200) NULL,
    [CreatedAt]  DATETIME       NOT NULL,
    [LastUpdate] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE TABLE [dbo].[Wallets] (
    [Id]     INT NOT NULL,
    [UserId] INT NOT NULL,
    [Points] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

GO
CREATE PROCEDURE [dbo].[Users_GetAll]
AS
BEGIN
    SELECT *
    FROM   dbo.Users;
END

GO
CREATE PROCEDURE [dbo].[Users_GetByEmail]
@Email NVARCHAR (200) NULL
AS
BEGIN
    SELECT *
    FROM   dbo.Users
    WHERE  Email = @Email;
END

GO
CREATE PROCEDURE [dbo].[Users_Insert]
@Email NVARCHAR (200) NULL, @Password NVARCHAR (200) NULL, @Salt NVARCHAR (40) NULL, @Username NVARCHAR (200) NULL, @FullName NVARCHAR (200) NULL, @CreatedAt DATETIME NULL, @LastUpdate DATETIME NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT  INTO dbo.Users (Email, Password, Salt, Username, FullName, CreatedAt, LastUpdate)
    VALUES                (@Email, @Password, @Salt, @Username, @FullName, @CreatedAt, @LastUpdate);
END

GO
