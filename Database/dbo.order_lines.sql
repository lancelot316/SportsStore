CREATE TABLE [dbo].[OrderLines]
(
	[OrderLineId] INT NOT NULL PRIMARY KEY Identity(1,1), 
    [Quantity] INT NOT NULL, 
    [Product_ProductId] INT NULL, 
    [Order_OrderId] INT NULL, 
    CONSTRAINT [FK_dbo.OrderLines_dbo_Products] FOREIGN KEY ([Product_ProductId]) REFERENCES [dbo].[Products]([ProductId]), 
    CONSTRAINT [FK_dbo.OrderLines_dbo.Orders_Order_OrderId] FOREIGN KEY ([Order_OrderId]) REFERENCES [dbo].[Orders]([OrderId])

)
