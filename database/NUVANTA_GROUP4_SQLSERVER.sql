-- =====================================================
-- NUVANTA | SQL Server | GROUP 4: Procurement (PR, PO, Goods Receipt)
-- Run groups in order 1 -> 8 (SSMS / sqlcmd, uses GO)
-- =====================================================
USE nuvanta;
GO

-- ---------- TABLES ----------
CREATE TABLE dbo.PurchaseRequests (
    PurchaseRequestID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    RequestedBy INT NOT NULL,
    DepartmentID INT NOT NULL,
    BudgetID INT NOT NULL,
    CostCenterID INT NULL,
    RequestDate DATETIME2(3) NOT NULL,
    SubmittedAt DATETIME2(3) NULL,
    ApprovedAt DATETIME2(3) NULL,
    Status NVARCHAR(50) NOT NULL,
    Justification NVARCHAR(MAX) NOT NULL,
    UpdatedAt DATETIME2(3) NULL CONSTRAINT DF_PurchaseRequests_UpdatedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_PurchaseRequests PRIMARY KEY (PurchaseRequestID),
    CONSTRAINT FK_PurchaseRequests_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_PurchaseRequests_RequestedBy FOREIGN KEY (RequestedBy) REFERENCES dbo.Users(UserID),
    CONSTRAINT FK_PurchaseRequests_DepartmentID FOREIGN KEY (DepartmentID) REFERENCES dbo.Departments(DepartmentID),
    CONSTRAINT FK_PurchaseRequests_BudgetID FOREIGN KEY (BudgetID) REFERENCES dbo.Budgets(BudgetID),
    CONSTRAINT FK_PurchaseRequests_CostCenterID FOREIGN KEY (CostCenterID) REFERENCES dbo.CostCenters(CostCenterID)
);
GO

CREATE TABLE dbo.BudgetReservations (
    ReservationID INT IDENTITY(1,1) NOT NULL,
    AllocationID INT NOT NULL,
    PurchaseRequestID INT NOT NULL,
    ReservedAmount DECIMAL(18,4) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    ReservedAt DATETIME2(3) NULL CONSTRAINT DF_BudgetReservations_ReservedAt DEFAULT (SYSDATETIME()),
    ReleasedAt DATETIME2(3) NULL,
    CONSTRAINT PK_BudgetReservations PRIMARY KEY (ReservationID),
    CONSTRAINT FK_BudgetReservations_AllocationID FOREIGN KEY (AllocationID) REFERENCES dbo.BudgetAllocations(AllocationID) ON DELETE CASCADE,
    CONSTRAINT FK_BudgetReservations_PurchaseRequestID FOREIGN KEY (PurchaseRequestID) REFERENCES dbo.PurchaseRequests(PurchaseRequestID) ON DELETE CASCADE
);
GO

CREATE TABLE dbo.PurchaseRequestItems (
    RequestItemID INT IDENTITY(1,1) NOT NULL,
    PurchaseRequestID INT NOT NULL,
    ItemID INT NULL,
    ItemDescription NVARCHAR(MAX) NOT NULL,
    Quantity DECIMAL(18,4) NOT NULL,
    EstimatedUnitPrice DECIMAL(18,4) NOT NULL,
    LineTotal AS (CONVERT(DECIMAL(18,4), Quantity * EstimatedUnitPrice)) PERSISTED,
    CONSTRAINT PK_PurchaseRequestItems PRIMARY KEY (RequestItemID),
    CONSTRAINT FK_PurchaseRequestItems_PurchaseRequestID FOREIGN KEY (PurchaseRequestID) REFERENCES dbo.PurchaseRequests(PurchaseRequestID) ON DELETE CASCADE,
    CONSTRAINT FK_PurchaseRequestItems_ItemID FOREIGN KEY (ItemID) REFERENCES dbo.InventoryItems(ItemID) ON DELETE SET NULL
);
GO

CREATE FUNCTION dbo.fn_PRExpectedTotal (@PurchaseRequestID INT)
RETURNS DECIMAL(18,4)
AS
BEGIN
    RETURN (SELECT COALESCE(SUM(LineTotal), 0) FROM dbo.PurchaseRequestItems WHERE PurchaseRequestID = @PurchaseRequestID);
END;
GO
ALTER TABLE dbo.PurchaseRequests ADD ExpectedTotalAmount AS dbo.fn_PRExpectedTotal(PurchaseRequestID);
GO

CREATE TABLE dbo.PurchaseOrders (
    PurchaseOrderID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    SupplierID INT NOT NULL,
    PurchaseRequestID INT NULL,
    OrderDate DATE NOT NULL,
    ExpectedDeliveryDate DATE NULL,
    DeliveryAddress NVARCHAR(500) NULL,
    CurrencyCode NVARCHAR(10) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    TotalAmount DECIMAL(18,4) NOT NULL,
    CreatedBy INT NOT NULL,
    CONSTRAINT PK_PurchaseOrders PRIMARY KEY (PurchaseOrderID),
    CONSTRAINT FK_PurchaseOrders_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_PurchaseOrders_SupplierID FOREIGN KEY (SupplierID) REFERENCES dbo.Suppliers(SupplierID),
    CONSTRAINT FK_PurchaseOrders_PurchaseRequestID FOREIGN KEY (PurchaseRequestID) REFERENCES dbo.PurchaseRequests(PurchaseRequestID) ON DELETE SET NULL,
    CONSTRAINT FK_PurchaseOrders_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID)
);
GO

CREATE TABLE dbo.PurchaseOrderItems (
    PurchaseOrderItemID INT IDENTITY(1,1) NOT NULL,
    PurchaseOrderID INT NOT NULL,
    RequestItemID INT NULL,
    ItemID INT NOT NULL,
    Quantity DECIMAL(18,4) NOT NULL,
    UnitPrice DECIMAL(18,4) NOT NULL,
    LineTotal AS (CONVERT(DECIMAL(18,4), Quantity * UnitPrice)) PERSISTED,
    CONSTRAINT PK_PurchaseOrderItems PRIMARY KEY (PurchaseOrderItemID),
    CONSTRAINT FK_PurchaseOrderItems_PurchaseOrderID FOREIGN KEY (PurchaseOrderID) REFERENCES dbo.PurchaseOrders(PurchaseOrderID) ON DELETE CASCADE,
    CONSTRAINT FK_PurchaseOrderItems_RequestItemID FOREIGN KEY (RequestItemID) REFERENCES dbo.PurchaseRequestItems(RequestItemID),
    CONSTRAINT FK_PurchaseOrderItems_ItemID FOREIGN KEY (ItemID) REFERENCES dbo.InventoryItems(ItemID)
);
GO

CREATE TABLE dbo.GoodsReceipts (
    GoodsReceiptID INT IDENTITY(1,1) NOT NULL,
    PurchaseOrderID INT NOT NULL,
    WarehouseID INT NOT NULL,
    ReceivedBy INT NOT NULL,
    ReceiptNumber NVARCHAR(100) NOT NULL,
    ReceiptDate DATE NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CONSTRAINT PK_GoodsReceipts PRIMARY KEY (GoodsReceiptID),
    CONSTRAINT FK_GoodsReceipts_PurchaseOrderID FOREIGN KEY (PurchaseOrderID) REFERENCES dbo.PurchaseOrders(PurchaseOrderID),
    CONSTRAINT FK_GoodsReceipts_WarehouseID FOREIGN KEY (WarehouseID) REFERENCES dbo.Warehouses(WarehouseID),
    CONSTRAINT FK_GoodsReceipts_ReceivedBy FOREIGN KEY (ReceivedBy) REFERENCES dbo.Users(UserID)
);
GO

CREATE TABLE dbo.GoodsReceiptItems (
    GoodsReceiptItemID INT IDENTITY(1,1) NOT NULL,
    GoodsReceiptID INT NOT NULL,
    PurchaseOrderItemID INT NOT NULL,
    ReceivedQuantity DECIMAL(18,4) NOT NULL,
    AcceptedQuantity DECIMAL(18,4) NOT NULL,
    RejectedQuantity DECIMAL(18,4) NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CONSTRAINT PK_GoodsReceiptItems PRIMARY KEY (GoodsReceiptItemID),
    CONSTRAINT FK_GoodsReceiptItems_GoodsReceiptID FOREIGN KEY (GoodsReceiptID) REFERENCES dbo.GoodsReceipts(GoodsReceiptID) ON DELETE CASCADE,
    CONSTRAINT FK_GoodsReceiptItems_PurchaseOrderItemID FOREIGN KEY (PurchaseOrderItemID) REFERENCES dbo.PurchaseOrderItems(PurchaseOrderItemID)
);
GO

CREATE FUNCTION dbo.fn_POItemReceivedQty (@PurchaseOrderItemID INT)
RETURNS DECIMAL(18,4)
AS
BEGIN
    RETURN (SELECT COALESCE(SUM(ReceivedQuantity), 0) FROM dbo.GoodsReceiptItems WHERE PurchaseOrderItemID = @PurchaseOrderItemID);
END;
GO
ALTER TABLE dbo.PurchaseOrderItems ADD ReceivedQuantity AS dbo.fn_POItemReceivedQty(PurchaseOrderItemID);
GO

-- ---------- UpdatedAt triggers (replaces MySQL ON UPDATE CURRENT_TIMESTAMP) ----------
CREATE TRIGGER dbo.TR_PurchaseRequests_UpdatedAt
ON dbo.PurchaseRequests
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRIGGER_NESTLEVEL(OBJECT_ID('dbo.TR_PurchaseRequests_UpdatedAt')) > 1 RETURN;
    UPDATE t SET UpdatedAt = SYSDATETIME()
    FROM dbo.PurchaseRequests AS t
    INNER JOIN inserted AS i ON i.PurchaseRequestID = t.PurchaseRequestID;
END;
GO

-- ---------- SEED DATA ----------
SET IDENTITY_INSERT dbo.PurchaseRequests ON;
INSERT INTO dbo.PurchaseRequests (PurchaseRequestID, CompanyID, RequestedBy, DepartmentID, BudgetID, CostCenterID, RequestDate, SubmittedAt, Status, Justification)
VALUES
    (1, 1, 2, 2, 1, 2, N'2026-03-01', N'2026-03-01 09:30:00', N'Approved', N'Purchasing laptops for new employees');
SET IDENTITY_INSERT dbo.PurchaseRequests OFF;
GO

SET IDENTITY_INSERT dbo.BudgetReservations ON;
INSERT INTO dbo.BudgetReservations (ReservationID, AllocationID, PurchaseRequestID, ReservedAmount, Status)
VALUES
    (1, 1, 1, 15000.0000, N'Reserved');
SET IDENTITY_INSERT dbo.BudgetReservations OFF;
GO

SET IDENTITY_INSERT dbo.PurchaseRequestItems ON;
INSERT INTO dbo.PurchaseRequestItems (RequestItemID, PurchaseRequestID, ItemID, ItemDescription, Quantity, EstimatedUnitPrice)
VALUES
    (1, 1, 1, N'Dell Core i7 Laptop', 5.0000, 3000.0000);
SET IDENTITY_INSERT dbo.PurchaseRequestItems OFF;
GO

SET IDENTITY_INSERT dbo.PurchaseOrders ON;
INSERT INTO dbo.PurchaseOrders (PurchaseOrderID, CompanyID, SupplierID, PurchaseRequestID, OrderDate, ExpectedDeliveryDate, CurrencyCode, Status, TotalAmount, CreatedBy)
VALUES
    (1, 1, 1, 1, N'2026-03-02', N'2026-03-10', N'SAR', N'Issued', 15000.0000, 2);
SET IDENTITY_INSERT dbo.PurchaseOrders OFF;
GO

SET IDENTITY_INSERT dbo.PurchaseOrderItems ON;
INSERT INTO dbo.PurchaseOrderItems (PurchaseOrderItemID, PurchaseOrderID, RequestItemID, ItemID, Quantity, UnitPrice)
VALUES
    (1, 1, 1, 1, 5.0000, 3000.0000);
SET IDENTITY_INSERT dbo.PurchaseOrderItems OFF;
GO

SET IDENTITY_INSERT dbo.GoodsReceipts ON;
INSERT INTO dbo.GoodsReceipts (GoodsReceiptID, PurchaseOrderID, WarehouseID, ReceivedBy, ReceiptNumber, ReceiptDate, Status, Notes)
VALUES
    (1, 1, 1, 1, N'GR-2026-001', N'2026-03-08', N'Completed', N'Goods received in excellent condition');
SET IDENTITY_INSERT dbo.GoodsReceipts OFF;
GO

SET IDENTITY_INSERT dbo.GoodsReceiptItems ON;
INSERT INTO dbo.GoodsReceiptItems (GoodsReceiptItemID, GoodsReceiptID, PurchaseOrderItemID, ReceivedQuantity, AcceptedQuantity, RejectedQuantity, Notes)
VALUES
    (1, 1, 1, 5.0000, 5.0000, 0.0000, N'Matches the order');
SET IDENTITY_INSERT dbo.GoodsReceiptItems OFF;
GO

-- ---------- CHECK QUERY ----------
SELECT pr.PurchaseRequestID, pr.Status AS RequestStatus, pr.ExpectedTotalAmount, CONCAT(u.FirstName, N' ', u.LastName) AS RequestedByName,
       po.PurchaseOrderID, s.SupplierName, po.TotalAmount AS POAmount, po.Status AS POStatus, poi.LineTotal, poi.ReceivedQuantity
FROM dbo.PurchaseRequests pr
JOIN dbo.Users u ON pr.RequestedBy = u.UserID
LEFT JOIN dbo.PurchaseOrders po ON pr.PurchaseRequestID = po.PurchaseRequestID
LEFT JOIN dbo.Suppliers s ON po.SupplierID = s.SupplierID
LEFT JOIN dbo.PurchaseOrderItems poi ON po.PurchaseOrderID = poi.PurchaseOrderID;
GO
