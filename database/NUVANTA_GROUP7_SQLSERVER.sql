-- =====================================================
-- NUVANTA | SQL Server | GROUP 7: Accounting & Cash
-- Run groups in order 1 -> 8 (SSMS / sqlcmd, uses GO)
-- =====================================================
USE nuvanta;
GO

-- ---------- TABLES ----------
CREATE TABLE dbo.Accounts (
    AccountID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    ParentAccountID INT NULL,
    AccountCode NVARCHAR(50) NOT NULL,
    AccountName NVARCHAR(150) NOT NULL,
    AccountType NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Accounts_IsActive DEFAULT (1),
    CONSTRAINT PK_Accounts PRIMARY KEY (AccountID),
    CONSTRAINT UK_Company_AccountCode UNIQUE (CompanyID, AccountCode),
    CONSTRAINT FK_Accounts_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_Accounts_ParentAccountID FOREIGN KEY (ParentAccountID) REFERENCES dbo.Accounts(AccountID)
);
GO

CREATE TABLE dbo.LedgerEntries (
    LedgerEntryID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    FinancialPeriodID INT NOT NULL,
    EntryDate DATE NOT NULL,
    Description NVARCHAR(MAX) NULL,
    SourceType NVARCHAR(50) NULL,
    SourceID INT NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedBy INT NOT NULL,
    PostedAt DATETIME2(3) NULL,
    ReversedByEntryID INT NULL,
    CurrencyCode NVARCHAR(10) NOT NULL,
    UpdatedAt DATETIME2(3) NULL CONSTRAINT DF_LedgerEntries_UpdatedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_LedgerEntries PRIMARY KEY (LedgerEntryID),
    CONSTRAINT FK_LedgerEntries_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_LedgerEntries_FinancialPeriodID FOREIGN KEY (FinancialPeriodID) REFERENCES dbo.FinancialPeriods(FinancialPeriodID),
    CONSTRAINT FK_LedgerEntries_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID),
    CONSTRAINT FK_LedgerEntries_ReversedByEntryID FOREIGN KEY (ReversedByEntryID) REFERENCES dbo.LedgerEntries(LedgerEntryID)
);
GO

CREATE TABLE dbo.LedgerEntryLines (
    LedgerEntryLineID INT IDENTITY(1,1) NOT NULL,
    LedgerEntryID INT NOT NULL,
    AccountID INT NOT NULL,
    DebitAmount DECIMAL(18,4) NOT NULL CONSTRAINT DF_LedgerEntryLines_DebitAmount DEFAULT (0),
    CreditAmount DECIMAL(18,4) NOT NULL CONSTRAINT DF_LedgerEntryLines_CreditAmount DEFAULT (0),
    Description NVARCHAR(MAX) NULL,
    CONSTRAINT PK_LedgerEntryLines PRIMARY KEY (LedgerEntryLineID),
    CONSTRAINT FK_LedgerEntryLines_LedgerEntryID FOREIGN KEY (LedgerEntryID) REFERENCES dbo.LedgerEntries(LedgerEntryID) ON DELETE CASCADE,
    CONSTRAINT FK_LedgerEntryLines_AccountID FOREIGN KEY (AccountID) REFERENCES dbo.Accounts(AccountID)
);
GO

CREATE TABLE dbo.CashAccounts (
    CashAccountID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    AccountName NVARCHAR(150) NOT NULL,
    AccountType NVARCHAR(50) NOT NULL,
    CurrencyCode NVARCHAR(10) NOT NULL,
    OpeningBalance DECIMAL(18,4) NOT NULL CONSTRAINT DF_CashAccounts_OpeningBalance DEFAULT (0),
    IsActive BIT NOT NULL CONSTRAINT DF_CashAccounts_IsActive DEFAULT (1),
    CONSTRAINT PK_CashAccounts PRIMARY KEY (CashAccountID),
    CONSTRAINT FK_CashAccounts_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID)
);
GO

CREATE TABLE dbo.CashTransactions (
    CashTransactionID INT IDENTITY(1,1) NOT NULL,
    CashAccountID INT NOT NULL,
    PaymentID INT NULL,
    TransactionDate DATETIME2(3) NOT NULL,
    TransactionType NVARCHAR(50) NOT NULL,
    Amount DECIMAL(18,4) NOT NULL,
    ReferenceNumber NVARCHAR(100) NULL,
    Status NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_CashTransactions PRIMARY KEY (CashTransactionID),
    CONSTRAINT FK_CashTransactions_CashAccountID FOREIGN KEY (CashAccountID) REFERENCES dbo.CashAccounts(CashAccountID) ON DELETE CASCADE,
    CONSTRAINT FK_CashTransactions_PaymentID FOREIGN KEY (PaymentID) REFERENCES dbo.Payments(PaymentID) ON DELETE SET NULL
);
GO

-- ---------- UpdatedAt triggers (replaces MySQL ON UPDATE CURRENT_TIMESTAMP) ----------
CREATE TRIGGER dbo.TR_LedgerEntries_UpdatedAt
ON dbo.LedgerEntries
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRIGGER_NESTLEVEL(OBJECT_ID('dbo.TR_LedgerEntries_UpdatedAt')) > 1 RETURN;
    UPDATE t SET UpdatedAt = SYSDATETIME()
    FROM dbo.LedgerEntries AS t
    INNER JOIN inserted AS i ON i.LedgerEntryID = t.LedgerEntryID;
END;
GO

-- ---------- SEED DATA ----------
SET IDENTITY_INSERT dbo.Accounts ON;
INSERT INTO dbo.Accounts (AccountID, CompanyID, ParentAccountID, AccountCode, AccountName, AccountType, IsActive)
VALUES
    (1, 1, NULL, N'1000', N'Assets', N'Asset', 1),
    (2, 1, 1, N'1100', N'Cash and Bank', N'Asset', 1),
    (3, 1, NULL, N'2000', N'Liabilities', N'Liability', 1),
    (4, 1, 3, N'2100', N'Accounts Payable (Suppliers)', N'Liability', 1),
    (5, 1, NULL, N'5000', N'Expenses', N'Expense', 1),
    (6, 1, 5, N'5100', N'Equipment and Hardware Expenses', N'Expense', 1);
SET IDENTITY_INSERT dbo.Accounts OFF;
GO

SET IDENTITY_INSERT dbo.LedgerEntries ON;
INSERT INTO dbo.LedgerEntries (LedgerEntryID, CompanyID, FinancialPeriodID, EntryDate, Description, SourceType, SourceID, Status, CreatedBy, PostedAt, CurrencyCode)
VALUES
    (1, 1, 1, N'2026-03-09', N'Entry to record hardware purchase invoice', N'SupplierInvoice', 1, N'Posted', 1, N'2026-03-09 12:00:00', N'SAR');
SET IDENTITY_INSERT dbo.LedgerEntries OFF;
GO

SET IDENTITY_INSERT dbo.LedgerEntryLines ON;
INSERT INTO dbo.LedgerEntryLines (LedgerEntryLineID, LedgerEntryID, AccountID, DebitAmount, CreditAmount, Description)
VALUES
    (1, 1, 6, 15000.0000, 0.0000, N'Debit hardware expenses'),
    (2, 1, 4, 0.0000, 15000.0000, N'Credit accounts payable (suppliers)');
SET IDENTITY_INSERT dbo.LedgerEntryLines OFF;
GO

SET IDENTITY_INSERT dbo.CashAccounts ON;
INSERT INTO dbo.CashAccounts (CashAccountID, CompanyID, AccountName, AccountType, CurrencyCode, OpeningBalance, IsActive)
VALUES
    (1, 1, N'NCB - Main Account', N'Bank', N'SAR', 100000.0000, 1);
SET IDENTITY_INSERT dbo.CashAccounts OFF;
GO

SET IDENTITY_INSERT dbo.CashTransactions ON;
INSERT INTO dbo.CashTransactions (CashTransactionID, CashAccountID, PaymentID, TransactionDate, TransactionType, Amount, ReferenceNumber, Status)
VALUES
    (1, 1, 1, N'2026-03-10', N'Outgoing', 15000.0000, N'TXN-998877', N'Completed');
SET IDENTITY_INSERT dbo.CashTransactions OFF;
GO

-- ---------- CHECK QUERY ----------
SELECT le.LedgerEntryID, le.EntryDate, le.Description AS EntryDescription, a.AccountCode, a.AccountName, lel.DebitAmount, lel.CreditAmount
FROM dbo.LedgerEntries le
JOIN dbo.LedgerEntryLines lel ON le.LedgerEntryID = lel.LedgerEntryID
JOIN dbo.Accounts a ON lel.AccountID = a.AccountID;
GO
