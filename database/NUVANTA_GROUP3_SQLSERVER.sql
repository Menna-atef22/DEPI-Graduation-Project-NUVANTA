-- =====================================================
-- NUVANTA | SQL Server | GROUP 3: Suppliers, Warehouses & Item Master
-- Run groups in order 1 -> 8 (SSMS / sqlcmd, uses GO)
-- =====================================================
USE nuvanta;
GO

-- ---------- TABLES ----------
CREATE TABLE dbo.Suppliers (
    SupplierID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    SupplierName NVARCHAR(150) NOT NULL,
    TaxNumber NVARCHAR(50) NULL,
    Email NVARCHAR(150) NULL,
    Phone NVARCHAR(50) NULL,
    Address NVARCHAR(500) NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2(3) NULL CONSTRAINT DF_Suppliers_CreatedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_Suppliers PRIMARY KEY (SupplierID),
    CONSTRAINT FK_Suppliers_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID)
);
GO

CREATE TABLE dbo.Warehouses (
    WarehouseID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    BranchID INT NULL,
    WarehouseName NVARCHAR(150) NOT NULL,
    Location NVARCHAR(500) NULL,
    Status NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_Warehouses PRIMARY KEY (WarehouseID),
    CONSTRAINT FK_Warehouses_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_Warehouses_BranchID FOREIGN KEY (BranchID) REFERENCES dbo.Branches(BranchID) ON DELETE SET NULL
);
GO

CREATE TABLE dbo.InventoryCategories (
    InventoryCategoryID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    CategoryName NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_InventoryCategories_IsActive DEFAULT (1),
    CONSTRAINT PK_InventoryCategories PRIMARY KEY (InventoryCategoryID),
    CONSTRAINT FK_InventoryCategories_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID)
);
GO

CREATE TABLE dbo.InventoryItems (
    ItemID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    InventoryCategoryID INT NULL,
    ItemName NVARCHAR(150) NOT NULL,
    SKU NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    UnitOfMeasure NVARCHAR(50) NOT NULL,
    MinimumStockLevel DECIMAL(18,4) NULL CONSTRAINT DF_InventoryItems_MinimumStockLevel DEFAULT (0),
    IsActive BIT NOT NULL CONSTRAINT DF_InventoryItems_IsActive DEFAULT (1),
    CONSTRAINT PK_InventoryItems PRIMARY KEY (ItemID),
    CONSTRAINT UK_Company_SKU UNIQUE (CompanyID, SKU),
    CONSTRAINT FK_InventoryItems_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_InventoryItems_InventoryCategoryID FOREIGN KEY (InventoryCategoryID) REFERENCES dbo.InventoryCategories(InventoryCategoryID) ON DELETE SET NULL
);
GO

-- ---------- SEED DATA ----------
SET IDENTITY_INSERT dbo.Suppliers ON;
INSERT INTO dbo.Suppliers (SupplierID, CompanyID, SupplierName, TaxNumber, Email, Phone, Status, Address)
VALUES
    (1, 1, N'Smart Solutions Trading Co.', N'TAX-SUP-111', N'sales@smartsoul.com', N'+966500000000', N'Active', N'Olaya Street, Riyadh, Saudi Arabia');
SET IDENTITY_INSERT dbo.Suppliers OFF;
GO

SET IDENTITY_INSERT dbo.Warehouses ON;
INSERT INTO dbo.Warehouses (WarehouseID, CompanyID, BranchID, WarehouseName, Status, Location)
VALUES
    (1, 1, 1, N'Riyadh Main Warehouse', N'Active', N'Al-Suli Industrial Area, Riyadh, Saudi Arabia');
SET IDENTITY_INSERT dbo.Warehouses OFF;
GO

SET IDENTITY_INSERT dbo.InventoryCategories ON;
INSERT INTO dbo.InventoryCategories (InventoryCategoryID, CompanyID, CategoryName, Description, IsActive)
VALUES
    (1, 1, N'Electronic Devices', N'Computers and Accessories', 1);
SET IDENTITY_INSERT dbo.InventoryCategories OFF;
GO

SET IDENTITY_INSERT dbo.InventoryItems ON;
INSERT INTO dbo.InventoryItems (ItemID, CompanyID, InventoryCategoryID, ItemName, SKU, Description, UnitOfMeasure, MinimumStockLevel, IsActive)
VALUES
    (1, 1, 1, N'Dell Core i7 Laptop', N'SKU-LAP-DELL-01', N'Core i7, 16GB RAM', N'Piece', 2.0000, 1);
SET IDENTITY_INSERT dbo.InventoryItems OFF;
GO

-- ---------- CHECK QUERY ----------
SELECT i.SKU, i.ItemName, ic.CategoryName, i.UnitOfMeasure, s.SupplierName, w.WarehouseName
FROM dbo.InventoryItems i
LEFT JOIN dbo.InventoryCategories ic ON i.InventoryCategoryID = ic.InventoryCategoryID
CROSS JOIN dbo.Suppliers s
CROSS JOIN dbo.Warehouses w;
GO
