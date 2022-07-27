﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Movies] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Overview] nvarchar(max) NOT NULL,
    [Tagline] nvarchar(max) NOT NULL,
    [Budget] decimal(18,2) NULL,
    [Revenue] decimal(18,2) NULL,
    [ImdbUrl] nvarchar(max) NOT NULL,
    [TmdbUrl] nvarchar(max) NOT NULL,
    [PosterUrl] nvarchar(max) NOT NULL,
    [BackdropUrl] nvarchar(max) NOT NULL,
    [OriginalLanguage] nvarchar(max) NOT NULL,
    [ReleaseDate] datetime2 NULL,
    [RunTime] int NULL,
    [Price] decimal(18,2) NULL,
    [CreatedDate] datetime2 NULL,
    [UpdatedDate] datetime2 NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220720194035_InitialMigration', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'UpdatedBy');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Movies] ALTER COLUMN [UpdatedBy] nvarchar(512) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'TmdbUrl');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Movies] ALTER COLUMN [TmdbUrl] nvarchar(2084) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Title');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Title] nvarchar(256) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Tagline');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Tagline] nvarchar(512) NOT NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Revenue');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Revenue] decimal(18,4) NULL;
ALTER TABLE [Movies] ADD DEFAULT 9.9 FOR [Revenue];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Price');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Price] decimal(5,2) NULL;
ALTER TABLE [Movies] ADD DEFAULT 9.9 FOR [Price];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'PosterUrl');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Movies] ALTER COLUMN [PosterUrl] nvarchar(2084) NOT NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'OriginalLanguage');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Movies] ALTER COLUMN [OriginalLanguage] nvarchar(64) NOT NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'ImdbUrl');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Movies] ALTER COLUMN [ImdbUrl] nvarchar(2084) NOT NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'CreatedDate');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Movies] ADD DEFAULT (getdate()) FOR [CreatedDate];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'CreatedBy');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Movies] ALTER COLUMN [CreatedBy] nvarchar(512) NOT NULL;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Budget');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Budget] decimal(18,4) NULL;
ALTER TABLE [Movies] ADD DEFAULT 9.9 FOR [Budget];
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'BackdropUrl');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Movies] ALTER COLUMN [BackdropUrl] nvarchar(2084) NOT NULL;
GO

CREATE INDEX [IX_Movies_Budget] ON [Movies] ([Budget]);
GO

CREATE INDEX [IX_Movies_Price] ON [Movies] ([Price]);
GO

CREATE INDEX [IX_Movies_Revenue] ON [Movies] ([Revenue]);
GO

CREATE INDEX [IX_Movies_Title] ON [Movies] ([Title]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220722024601_MovieTableChanges', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(24) NOT NULL,
    CONSTRAINT [PK_genres] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220722025354_CreatingGenreTable', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [trailers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [TrailerUrl] nvarchar(max) NOT NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_trailers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_trailers_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_trailers_MovieId] ON [trailers] ([MovieId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220722030458_CreatingTrailerTableWithMovieFK', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieGenres] (
    [GenreId] int NOT NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_MovieGenres] PRIMARY KEY ([MovieId], [GenreId]),
    CONSTRAINT [FK_MovieGenres_genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [genres] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieGenres_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieGenres_GenreId] ON [MovieGenres] ([GenreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220722033430_CreatingMoviesGenresJunctionTable', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Cast] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(128) NOT NULL,
    [Gender] nvarchar(16) NOT NULL,
    [TmdbUrl] nvarchar(2084) NOT NULL,
    [ProfilePath] nvarchar(2084) NOT NULL,
    CONSTRAINT [PK_Cast] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220722034412_CreatingCastsTable', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieCasts] (
    [MovieId] int NOT NULL,
    [CastId] int NOT NULL,
    [Character] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_MovieCasts] PRIMARY KEY ([CastId], [MovieId], [Character]),
    CONSTRAINT [FK_MovieCasts_Cast_CastId] FOREIGN KEY ([CastId]) REFERENCES [Cast] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieCasts_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieCasts_MovieId] ON [MovieCasts] ([MovieId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220722035101_CreatingMovieCastTable', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'UpdatedBy');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Movies] ALTER COLUMN [UpdatedBy] nvarchar(512) NULL;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'CreatedBy');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Movies] ALTER COLUMN [CreatedBy] nvarchar(512) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220722040202_ChangingMovieTable', N'6.0.7');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [MovieCasts] DROP CONSTRAINT [FK_MovieCasts_Cast_CastId];
GO

ALTER TABLE [MovieGenres] DROP CONSTRAINT [FK_MovieGenres_genres_GenreId];
GO

ALTER TABLE [trailers] DROP CONSTRAINT [FK_trailers_Movies_MovieId];
GO

ALTER TABLE [trailers] DROP CONSTRAINT [PK_trailers];
GO

ALTER TABLE [genres] DROP CONSTRAINT [PK_genres];
GO

ALTER TABLE [Cast] DROP CONSTRAINT [PK_Cast];
GO

EXEC sp_rename N'[trailers]', N'Trailers';
GO

EXEC sp_rename N'[genres]', N'Genres';
GO

EXEC sp_rename N'[Cast]', N'Casts';
GO

EXEC sp_rename N'[Trailers].[IX_trailers_MovieId]', N'IX_Trailers_MovieId', N'INDEX';
GO

ALTER TABLE [Trailers] ADD CONSTRAINT [PK_Trailers] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Genres] ADD CONSTRAINT [PK_Genres] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Casts] ADD CONSTRAINT [PK_Casts] PRIMARY KEY ([Id]);
GO

CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(64) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(128) NOT NULL,
    [LastName] nvarchar(128) NOT NULL,
    [DateOfBirth] datetime2 NULL,
    [Email] nvarchar(256) NOT NULL,
    [HashedPassword] nvarchar(1024) NOT NULL,
    [Salt] nvarchar(1024) NOT NULL,
    [PhoneNumber] nvarchar(64) NULL,
    [IsLocked] bit NULL DEFAULT CAST(0 AS bit),
    [ProfilePictureUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Favorites] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Favorites] PRIMARY KEY ([UserId], [MovieId]),
    CONSTRAINT [FK_Favorites_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Favorites_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Reviews] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    [Rating] decimal(3,2) NOT NULL,
    [ReviewText] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([UserId], [MovieId]),
    CONSTRAINT [FK_Reviews_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([RoleId], [UserId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Favorites_MovieId] ON [Favorites] ([MovieId]);
GO

CREATE INDEX [IX_Reviews_MovieId] ON [Reviews] ([MovieId]);
GO

CREATE INDEX [IX_UserRoles_UserId] ON [UserRoles] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
GO

ALTER TABLE [MovieCasts] ADD CONSTRAINT [FK_MovieCasts_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [MovieGenres] ADD CONSTRAINT [FK_MovieGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Trailers] ADD CONSTRAINT [FK_Trailers_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220724233343_CreatingUserAndRolesTables', N'6.0.7');
GO

COMMIT;
GO

