-- =====================================================
-- NUVANTA | SQL Server | GROUP 5: Inventory Stock
-- Run groups in order 1 -> 8 (SSMS / sqlcmd, uses GO)
-- =====================================================
USE nuvanta;
GO

-- ---------- TABLES ----------
CREATE TABLE dbo.StockMovements (
    StockMovementID INT IDENTITY(1,1) NOT NULL,
    ItemID INT NOT NULL,
    WarehouseID INT NOT NULL,
    GoodsReceiptItemID INT NULL,
    MovementType NVARCHAR(50) NOT NULL,
    Quantity DECIMAL(18,4) NOT NULL,
    UnitCost DECIMAL(18,4) NULL,
    MovementDate DATETIME2(3) NULL CONSTRAINT DF_StockMovements_MovementDate DEFAULT (SYSDATETIME()),
    ReferenceType NVARCHAR(100) NULL,
    ReferenceID INT NULL,
    CreatedBy INT NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CONSTRAINT PK_StockMovements PRIMARY KEY (StockMovementID),
    CONSTRAINT FK_StockMovements_ItemID FOREIGN KEY (ItemID) REFERENCES dbo.InventoryItems(ItemID),
    CONSTRAINT FK_StockMovements_WarehouseID FOREIGN KEY (WarehouseID) REFERENCES dbo.Warehouses(WarehouseID),
    CONSTRAINT FK_StockMovements_GoodsReceiptItemID FOREIGN KEY (GoodsReceiptItemID) REFERENCES dbo.GoodsReceiptItems(GoodsReceiptItemID) ON DELETE SET NULL,
    CONSTRAINT FK_StockMovements_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserID)
);
GO

CREATE TABLE dbo.InventoryBalances (
    InventoryBalanceID INT IDENTITY(1,1) NOT NULL,
    ItemID INT NOT NULL,
    WarehouseID INT NOT NULL,
    QuantityOnHand DECIMAL(18,4) NOT NULL CONSTRAINT DF_InventoryBalances_QuantityOnHand DEFAULT (0),
    LastUpdatedAt DATETIME2(3) NULL CONSTRAINT DF_InventoryBalances_LastUpdatedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_InventoryBalances PRIMARY KEY (InventoryBalanceID),
    CONSTRAINT UK_Item_Warehouse UNIQUE (ItemID, WarehouseID),
    CONSTRAINT FK_InventoryBalances_ItemID FOREIGN KEY (ItemID) REFERENCES dbo.InventoryItems(ItemID) ON DELETE CASCADE,
    CONSTRAINT FK_InventoryBalances_WarehouseID FOREIGN KEY (WarehouseID) REFERENCES dbo.Warehouses(WarehouseID) ON DELETE CASCADE
);
GO

-- ---------- UpdatedAt triggers (replaces MySQL ON UPDATE CURRENT_TIMESTAMP) ----------
CREATE TRIGGER dbo.TR_InventoryBalances_LastUpdatedAt
ON dbo.InventoryBalances
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRIGGER_NESTLEVEL(OBJECT_ID('dbo.TR_InventoryBalances_LastUpdatedAt')) > 1 RETURN;
    UPDATE t SET LastUpdatedAt = SYSDATETIME()
    FROM dbo.InventoryBalances AS t
    INNER JOIN inserted AS i ON i.InventoryBalanceID = t.InventoryBalanceID;
END;
GO

-- ---------- SEED DATA ----------
SET IDENTITY_INSERT dbo.StockMovements ON;
INSERT INTO dbo.StockMovements (StockMovementID, ItemID, WarehouseID, GoodsReceiptItemID, MovementType, Quantity, UnitCost, ReferenceType, ReferenceID, CreatedBy, Notes)
VALUES
    (1, 1, 1, 1, N'IN', 5.0000, 3000.0000, N'GoodsReceipt', 1, 1, N'Goods inward from purchase order');
SET IDENTITY_INSERT dbo.StockMovements OFF;
GO

SET IDENTITY_INSERT dbo.InventoryBalances ON;
INSERT INTO dbo.InventoryBalances (InventoryBalanceID, ItemID, WarehouseID, QuantityOnHand)
VALUES
    (1, 1, 1, 5.0000);
SET IDENTITY_INSERT dbo.InventoryBalances OFF;
GO

-- ---------- CHECK QUERY ----------
SELECT i.SKU, i.ItemName, ic.CategoryName, w.WarehouseName, ib.QuantityOnHand, i.UnitOfMeasure
FROM dbo.InventoryBalances ib
JOIN dbo.InventoryItems i ON ib.ItemID = i.ItemID
LEFT JOIN dbo.InventoryCategories ic ON i.InventoryCategoryID = ic.InventoryCategoryID
JOIN dbo.Warehouses w ON ib.WarehouseID = w.WarehouseID;
GO
