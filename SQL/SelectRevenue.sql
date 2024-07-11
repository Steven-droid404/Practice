CREATE PROCEDURE SelectRevenue
    @CompanyCode CHAR(4)
AS
BEGIN
    SELECT * FROM RevenueData WHERE CompanyCode = @CompanyCode;
END;
GO