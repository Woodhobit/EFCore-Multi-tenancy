﻿CREATE TABLE [Profiles1] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NOT NULL,
    [FirstName] nvarchar(255) NOT NULL,
    [SecondName] nvarchar(255) NOT NULL,
    [Email] nvarchar(127) NOT NULL,
    CONSTRAINT [PK_Profiles1] PRIMARY KEY ([Id])
);