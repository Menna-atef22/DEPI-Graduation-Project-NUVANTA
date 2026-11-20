-- =====================================================
-- NUVANTA | SQL Server | GROUP 2: Finance Setup & Budgeting
-- Run groups in order 1 -> 8 (SSMS / sqlcmd, uses GO)
-- =====================================================
USE nuvanta;
GO

-- ---------- TABLES ----------
CREATE TABLE dbo.FinancialPeriods (
    FinancialPeriodID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    PeriodName NVARCHAR(100) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_FinancialPeriods PRIMARY KEY (FinancialPeriodID),
    CONSTRAINT FK_FinancialPeriods_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID)
);
GO

CREATE TABLE dbo.CostCenters (
    CostCenterID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    DepartmentID INT NULL,
    CostCenterName NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CONSTRAINT PK_CostCenters PRIMARY KEY (CostCenterID),
    CONSTRAINT FK_CostCenters_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_CostCenters_DepartmentID FOREIGN KEY (DepartmentID) REFERENCES dbo.Departments(DepartmentID) ON DELETE SET NULL
);
GO

CREATE TABLE dbo.ExpenseCategories (
    CategoryID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    CategoryName NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CONSTRAINT PK_ExpenseCategories PRIMARY KEY (CategoryID),
    CONSTRAINT FK_ExpenseCategories_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID)
);
GO

CREATE TABLE dbo.Budgets (
    BudgetID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    FinancialPeriodID INT NOT NULL,
    BudgetName NVARCHAR(150) NOT NULL,
    TotalAmount DECIMAL(18,4) NOT NULL,
    CurrencyCode NVARCHAR(10) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedBy INT NOT NULL,
    ApprovedBy INT NULL,
    ApprovedAt DATETIME2(3) NULL,
    CreatedAt DATETIME2(3) NULL CONSTRAINT DF_Budgets_CreatedAt DEFAULT (SYSDATETIME()),
    UpdatedAt DATETIME2(3) NULL CONSTRAINT DF_Budgets_UpdatedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_Budgets PRIMARY KEY (BudgetID),
    CONSTRAINT FK_Budgets_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_Budgets_FinancialPeriodID FOREIGN KEY (FinancialPeriodID) REFERENCES dbo.FinancialPeriods(FinancialPeriodID) ON DELETE CASCADE,
    CONSTRAINT FK_Budgets_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID),
    CONSTRAINT FK_Budgets_ApprovedBy FOREIGN KEY (ApprovedBy) REFERENCES dbo.Users(UserID)
);
GO

CREATE TABLE dbo.BudgetAllocations (
    AllocationID INT IDENTITY(1,1) NOT NULL,
    BudgetID INT NOT NULL,
    DepartmentID INT NULL,
    CostCenterID INT NULL,
    CategoryID INT NULL,
    AllocatedAmount DECIMAL(18,4) NOT NULL,
    CreatedAt DATETIME2(3) NULL CONSTRAINT DF_BudgetAllocations_CreatedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_BudgetAllocations PRIMARY KEY (AllocationID),
    CONSTRAINT FK_BudgetAllocations_BudgetID FOREIGN KEY (BudgetID) REFERENCES dbo.Budgets(BudgetID) ON DELETE CASCADE,
    CONSTRAINT FK_BudgetAllocations_DepartmentID FOREIGN KEY (DepartmentID) REFERENCES dbo.Departments(DepartmentID) ON DELETE SET NULL,
    CONSTRAINT FK_BudgetAllocations_CostCenterID FOREIGN KEY (CostCenterID) REFERENCES dbo.CostCenters(CostCenterID),
    CONSTRAINT FK_BudgetAllocations_CategoryID FOREIGN KEY (CategoryID) REFERENCES dbo.ExpenseCategories(CategoryID) ON DELETE SET NULL
);
GO

CREATE TABLE dbo.BudgetTransfers (
    TransferID INT IDENTITY(1,1) NOT NULL,
    FromAllocationID INT NOT NULL,
    ToAllocationID INT NOT NULL,
    Amount DECIMAL(18,4) NOT NULL,
    RequestedBy INT NOT NULL,
    ApprovedBy INT NULL,
    TransferDate DATETIME2(3) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    Reason NVARCHAR(MAX) NULL,
    CreatedAt DATETIME2(3) NULL CONSTRAINT DF_BudgetTransfers_CreatedAt DEFAULT (SYSDATETIME()),
    UpdatedAt DATETIME2(3) NULL CONSTRAINT DF_BudgetTransfers_UpdatedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_BudgetTransfers PRIMARY KEY (TransferID),
    CONSTRAINT FK_BudgetTransfers_FromAllocationID FOREIGN KEY (FromAllocationID) REFERENCES dbo.BudgetAllocations(AllocationID),
    CONSTRAINT FK_BudgetTransfers_ToAllocationID FOREIGN KEY (ToAllocationID) REFERENCES dbo.BudgetAllocations(AllocationID),
    CONSTRAINT FK_BudgetTransfers_RequestedBy FOREIGN KEY (RequestedBy) REFERENCES dbo.Users(UserID),
    CONSTRAINT FK_BudgetTransfers_ApprovedBy FOREIGN KEY (ApprovedBy) REFERENCES dbo.Users(UserID)
);
GO

-- ---------- UpdatedAt triggers (replaces MySQL ON UPDATE CURRENT_TIMESTAMP) ----------
CREATE TRIGGER dbo.TR_Budgets_UpdatedAt
ON dbo.Budgets
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRIGGER_NESTLEVEL(OBJECT_ID('dbo.TR_Budgets_UpdatedAt')) > 1 RETURN;
    UPDATE t SET UpdatedAt = SYSDATETIME()
    FROM dbo.Budgets AS t
    INNER JOIN inserted AS i ON i.BudgetID = t.BudgetID;
END;
GO

CREATE TRIGGER dbo.TR_BudgetTransfers_UpdatedAt
ON dbo.BudgetTransfers
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRIGGER_NESTLEVEL(OBJECT_ID('dbo.TR_BudgetTransfers_UpdatedAt')) > 1 RETURN;
    UPDATE t SET UpdatedAt = SYSDATETIME()
    FROM dbo.BudgetTransfers AS t
    INNER JOIN inserted AS i ON i.TransferID = t.TransferID;
END;
GO

-- ---------- SEED DATA ----------
SET IDENTITY_INSERT dbo.FinancialPeriods ON;
INSERT INTO dbo.FinancialPeriods (FinancialPeriodID, CompanyID, PeriodName, StartDate, EndDate, Status)
VALUES
    (1, 1, N'Fiscal Year 2026', N'2026-01-01', N'2026-12-31', N'Open');
SET IDENTITY_INSERT dbo.FinancialPeriods OFF;
GO

SET IDENTITY_INSERT dbo.CostCenters ON;
INSERT INTO dbo.CostCenters (CostCenterID, CompanyID, DepartmentID, CostCenterName, Description)
VALUES
    (1, 1, 1, N'Finance Cost Center', N'General financial management expenses'),
    (2, 1, 2, N'Procurement Cost Center', N'Procurement operations expenses');
SET IDENTITY_INSERT dbo.CostCenters OFF;
GO

SET IDENTITY_INSERT dbo.ExpenseCategories ON;
INSERT INTO dbo.ExpenseCategories (CategoryID, CompanyID, CategoryName, Description)
VALUES
    (1, 1, N'Office Equipment', N'Purchase of office devices and supplies');
SET IDENTITY_INSERT dbo.ExpenseCategories OFF;
GO

SET IDENTITY_INSERT dbo.Budgets ON;
INSERT INTO dbo.Budgets (BudgetID, CompanyID, FinancialPeriodID, BudgetName, TotalAmount, CurrencyCode, Status, CreatedBy, ApprovedBy, ApprovedAt)
VALUES
    (1, 1, 1, N'Corporate Budget 2026', 500000.0000, N'SAR', N'Approved', 1, 1, N'2026-01-05 10:00:00');
SET IDENTITY_INSERT dbo.Budgets OFF;
GO

SET IDENTITY_INSERT dbo.BudgetAllocations ON;
INSERT INTO dbo.BudgetAllocations (AllocationID, BudgetID, DepartmentID, CostCenterID, CategoryID, AllocatedAmount)
VALUES
    (1, 1, 2, 2, 1, 100000.0000);
SET IDENTITY_INSERT dbo.BudgetAllocations OFF;
GO

-- ---------- CHECK QUERY ----------
SELECT b.BudgetName, b.TotalAmount AS TotalBudget, fp.PeriodName, dep.DepartmentName, cc.CostCenterName, ba.AllocatedAmount
FROM dbo.Budgets b
JOIN dbo.FinancialPeriods fp ON b.FinancialPeriodID = fp.FinancialPeriodID
JOIN dbo.BudgetAllocations ba ON b.BudgetID = ba.BudgetID
LEFT JOIN dbo.Departments dep ON ba.DepartmentID = dep.DepartmentID
LEFT JOIN dbo.CostCenters cc ON ba.CostCenterID = cc.CostCenterID;
GO
