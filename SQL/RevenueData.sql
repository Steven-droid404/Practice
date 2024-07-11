CREATE TABLE RevenueData (
    ReportDate DATE,                         -- 出表日期
    DataMonth CHAR(5),                       -- 資料年月
    CompanyCode CHAR(4),                     -- 公司代號
    CompanyName NVARCHAR(100),               -- 公司名稱
    Industry NVARCHAR(50),                   -- 產業別
    MonthlyRevenue DECIMAL(15,2),            -- 當月營收
    LastMonthRevenue DECIMAL(15,2),          -- 上月營收
    LastYearMonthlyRevenue DECIMAL(15,2),    -- 去年當月營收
    MoMChange DECIMAL(10,8),                 -- 上月比較增減百分比
    YoYChange DECIMAL(10,8),                 -- 去年同月增減百分比
    CumulativeRevenue DECIMAL(15,2),         -- 當月累計營收
    LastYearCumulativeRevenue DECIMAL(15,2), -- 去年累計營收
    CumulativeChange DECIMAL(10,8),          -- 累計營收前期比較增減百分比
    Remarks NVARCHAR(255)                    -- 備註
);
