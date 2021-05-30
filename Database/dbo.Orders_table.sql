CREATE TABLE [dbo].[Orders] (
    [OrderId]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (MAX) NULL,
    [Line1]      NVARCHAR (MAX) NULL,
    [Line2]      NVARCHAR (MAX) NULL,
    [Line3]      NVARCHAR (MAX) NULL,
    [City]       NVARCHAR (MAX) NULL,
    [State]      NVARCHAR (MAX) NULL,
    [GiftWrap]   BIT            NOT NULL,
    [Dispatched] BIT            NOT NULL,
    [Zip] NVARCHAR(MAX) NULL, 
    [Country] NVARCHAR(MAX) NOT NULL, 
    PRIMARY KEY CLUSTERED ([OrderId] ASC)
);

