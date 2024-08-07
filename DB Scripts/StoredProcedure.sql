CREATE PROCEDURE [dbo].[GetOrdersByCustomerId]
    @CustomerId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;

    SELECT dbo.OrderTable.OrderId,dbo.OrderTable.ProductId,dbo.OrderTable.OrderStatus,dbo.OrderTable.OrderType,dbo.OrderTable.OrderBy,dbo.OrderTable.OrderOn,dbo.OrderTable.ShippedOn,dbo.OrderTable.IsActive,dbo.Product.ProductName,dbo.Product.UnitPrice,dbo.Product.CreatedOn,dbo.Product.IsActive,dbo.Supplier.SupplierId,dbo.Supplier.SupplierName,dbo.Supplier.CreatedOn,dbo.Supplier.IsActive
    FROM DCE_Test.dbo.OrderTable
    INNER JOIN DCE_Test.dbo.Product ON OrderTable.ProductId = Product.ProductId
    INNER JOIN DCE_Test.dbo.Supplier ON Product.SupplierId = Supplier.SupplierId
    WHERE OrderTable.OrderBy = @CustomerId;
END