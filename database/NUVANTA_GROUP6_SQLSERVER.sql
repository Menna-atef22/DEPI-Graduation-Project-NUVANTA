-- =====================================================
-- NUVANTA | SQL Server | GROUP 6: Supplier Invoices & Payments
-- Run groups in order 1 -> 8 (SSMS / sqlcmd, uses GO)
-- =====================================================
USE nuvanta;
GO

-- ---------- TABLES ----------
CREATE TABLE dbo.SupplierInvoices (
    SupplierInvoiceID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    SupplierID INT NOT NULL,
    PurchaseOrderID INT NULL,
    InvoiceNumber NVARCHAR(100) NOT NULL,
    InvoiceDate DATE NOT NULL,
    DueDate DATE NULL,
    CurrencyCode NVARCHAR(10) NOT NULL,
    TotalAmount DECIMAL(18,4) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2(3) NULL CONSTRAINT DF_SupplierInvoices_CreatedAt DEFAULT (SYSDATETIME()),
    CreatedBy INT NOT NULL,
    CONSTRAINT PK_SupplierInvoices PRIMARY KEY (SupplierInvoiceID),
    CONSTRAINT FK_SupplierInvoices_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_SupplierInvoices_SupplierID FOREIGN KEY (SupplierID) REFERENCES dbo.Suppliers(SupplierID),
    CONSTRAINT FK_SupplierInvoices_PurchaseOrderID FOREIGN KEY (PurchaseOrderID) REFERENCES dbo.PurchaseOrders(PurchaseOrderID) ON DELETE SET NULL,
    CONSTRAINT FK_SupplierInvoices_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID)
);
GO

CREATE TABLE dbo.SupplierInvoiceItems (
    InvoiceItemID INT IDENTITY(1,1) NOT NULL,
    SupplierInvoiceID INT NOT NULL,
    PurchaseOrderItemID INT NULL,
    ItemID INT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Quantity DECIMAL(18,4) NOT NULL,
    UnitPrice DECIMAL(18,4) NOT NULL,
    TaxAmount DECIMAL(18,4) NULL CONSTRAINT DF_SupplierInvoiceItems_TaxAmount DEFAULT (0),
    DiscountAmount DECIMAL(18,4) NULL CONSTRAINT DF_SupplierInvoiceItems_DiscountAmount DEFAULT (0),
    LineTotal AS (CONVERT(DECIMAL(18,4), Quantity * UnitPrice - DiscountAmount + TaxAmount)) PERSISTED,
    CONSTRAINT PK_SupplierInvoiceItems PRIMARY KEY (InvoiceItemID),
    CONSTRAINT FK_SupplierInvoiceItems_SupplierInvoiceID FOREIGN KEY (SupplierInvoiceID) REFERENCES dbo.SupplierInvoices(SupplierInvoiceID) ON DELETE CASCADE,
    CONSTRAINT FK_SupplierInvoiceItems_PurchaseOrderItemID FOREIGN KEY (PurchaseOrderItemID) REFERENCES dbo.PurchaseOrderItems(PurchaseOrderItemID),
    CONSTRAINT FK_SupplierInvoiceItems_ItemID FOREIGN KEY (ItemID) REFERENCES dbo.InventoryItems(ItemID) ON DELETE SET NULL
);
GO

CREATE TABLE dbo.Payments (
    PaymentID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    SupplierID INT NULL,
    PaymentDate DATETIME2(3) NOT NULL,
    Amount DECIMAL(18,4) NOT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL,
    ProviderName NVARCHAR(100) NULL,
    ProviderTransactionID NVARCHAR(150) NULL,
    PaymentReference NVARCHAR(150) NULL,
    FailureReason NVARCHAR(MAX) NULL,
    TransactionReference NVARCHAR(150) NULL,
    Status NVARCHAR(50) NOT NULL,
    RecordedBy INT NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CONSTRAINT PK_Payments PRIMARY KEY (PaymentID),
    CONSTRAINT FK_Payments_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_Payments_SupplierID FOREIGN KEY (SupplierID) REFERENCES dbo.Suppliers(SupplierID) ON DELETE SET NULL,
    CONSTRAINT FK_Payments_RecordedBy FOREIGN KEY (RecordedBy) REFERENCES dbo.Users(UserID)
);
GO

CREATE TABLE dbo.PaymentAllocations (
    PaymentAllocationID INT IDENTITY(1,1) NOT NULL,
    PaymentID INT NOT NULL,
    SupplierInvoiceID INT NOT NULL,
    AllocatedAmount DECIMAL(18,4) NOT NULL,
    CONSTRAINT PK_PaymentAllocations PRIMARY KEY (PaymentAllocationID),
    CONSTRAINT FK_PaymentAllocations_PaymentID FOREIGN KEY (PaymentID) REFERENCES dbo.Payments(PaymentID) ON DELETE CASCADE,
    CONSTRAINT FK_PaymentAllocations_SupplierInvoiceID FOREIGN KEY (SupplierInvoiceID) REFERENCES dbo.SupplierInvoices(SupplierInvoiceID) ON DELETE CASCADE
);
GO

-- ---------- SEED DATA ----------
SET IDENTITY_INSERT dbo.SupplierInvoices ON;
INSERT INTO dbo.SupplierInvoices (SupplierInvoiceID, CompanyID, SupplierID, PurchaseOrderID, InvoiceNumber, InvoiceDate, DueDate, CurrencyCode, TotalAmount, Status, CreatedBy)
VALUES
    (1, 1, 1, 1, N'INV-SUP-999', N'2026-03-09', N'2026-04-09', N'SAR', 15000.0000, N'Unpaid', 1);
SET IDENTITY_INSERT dbo.SupplierInvoices OFF;
GO

SET IDENTITY_INSERT dbo.SupplierInvoiceItems ON;
INSERT INTO dbo.SupplierInvoiceItems (InvoiceItemID, SupplierInvoiceID, PurchaseOrderItemID, ItemID, Description, Quantity, UnitPrice, TaxAmount, DiscountAmount)
VALUES
    (1, 1, 1, 1, N'Dell Core i7 Laptop', 5.0000, 3000.0000, 0.0000, 0.0000);
SET IDENTITY_INSERT dbo.SupplierInvoiceItems OFF;
GO

SET IDENTITY_INSERT dbo.Payments ON;
INSERT INTO dbo.Payments (PaymentID, CompanyID, SupplierID, PaymentDate, Amount, PaymentMethod, ProviderName, Status, RecordedBy, Notes)
VALUES
    (1, 1, 1, N'2026-03-10', 15000.0000, N'Bank Transfer', N'Al Rajhi Bank', N'Completed', 1, N'Full invoice payment');
SET IDENTITY_INSERT dbo.Payments OFF;
GO

SET IDENTITY_INSERT dbo.PaymentAllocations ON;
INSERT INTO dbo.PaymentAllocations (PaymentAllocationID, PaymentID, SupplierInvoiceID, AllocatedAmount)
VALUES
    (1, 1, 1, 15000.0000);
SET IDENTITY_INSERT dbo.PaymentAllocations OFF;
GO

-- ---------- CHECK QUERY ----------
SELECT si.InvoiceNumber, s.SupplierName, si.TotalAmount AS InvoiceTotal,
       COALESCE(SUM(pa.AllocatedAmount), 0) AS PaidAmount,
       si.TotalAmount - COALESCE(SUM(pa.AllocatedAmount), 0) AS RemainingAmount, si.Status
FROM dbo.SupplierInvoices si
JOIN dbo.Suppliers s ON si.SupplierID = s.SupplierID
LEFT JOIN dbo.PaymentAllocations pa ON si.SupplierInvoiceID = pa.SupplierInvoiceID
GROUP BY si.SupplierInvoiceID, si.InvoiceNumber, s.SupplierName, si.TotalAmount, si.Status;
GO
