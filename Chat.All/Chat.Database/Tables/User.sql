CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(200) NOT NULL, 
    [LastName] NVARCHAR(200) NOT NULL, 
    [Email] NVARCHAR(200) NOT NULL, 
    [PasswordHash] BINARY(32) NOT NULL, 
    [PasswordSalt] NVARCHAR(200) NOT NULL
)