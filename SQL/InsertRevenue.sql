CREATE PROCEDURE InsertRevenue
    @ReportDate DATE,
    @DataMonth CHAR(6),
    @CompanyCode CHAR(4),
    @CompanyName NVARCHAR(100),
    @Industry NVARCHAR(50),
    @MonthlyRevenue DECIMAL(15, 2),
    @LastMonthRevenue DECIMAL(15, 2),
    @LastYearMonthlyRevenue DECIMAL(15, 2),
    @MoMChange DECIMAL(10, 8),
    @YoYChange DECIMAL(10, 8),
    @CumulativeRevenue DECIMAL(15, 2),
    @LastYearCumulativeRevenue DECIMAL(15, 2),
    @CumulativeChange DECIMAL(10, 8),
    @Remarks NVARCHAR(255)
AS
BEGIN
    INSERT INTO RevenueData (ReportDate, DataMonth, CompanyCode, CompanyName, Industry, MonthlyRevenue, LastMonthRevenue, LastYearMonthlyRevenue, MoMChange, YoYChange, CumulativeRevenue, LastYearCumulativeRevenue, CumulativeChange, Remarks)
    VALUES (@ReportDate, @DataMonth, @CompanyCode, @CompanyName, @Industry, @MonthlyRevenue, @LastMonthRevenue, @LastYearMonthlyRevenue, @MoMChange, @YoYChange, @CumulativeRevenue, @LastYearCumulativeRevenue, @CumulativeChange, @Remarks);
END;
