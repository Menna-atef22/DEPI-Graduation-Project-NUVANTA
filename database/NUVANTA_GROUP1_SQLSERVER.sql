-- =====================================================
-- NUVANTA | SQL Server | GROUP 1: Organization & Security
-- Run groups in order 1 -> 8 (SSMS / sqlcmd, uses GO)
-- =====================================================
IF DB_ID(N'nuvanta') IS NULL CREATE DATABASE nuvanta;
GO
USE nuvanta;
GO

-- ---------- TABLES ----------
CREATE TABLE dbo.Companies (
    CompanyID INT IDENTITY(1,1) NOT NULL,
    CompanyName NVARCHAR(150) NOT NULL,
    TaxNumber NVARCHAR(50) NULL,
    Email NVARCHAR(150) NULL,
    Phone NVARCHAR(50) NULL,
    Street NVARCHAR(255) NULL,
    City NVARCHAR(100) NULL,
    Country NVARCHAR(100) NULL,
    CurrencyCode NVARCHAR(10) NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Companies_IsActive DEFAULT (1),
    CreatedAt DATETIME2(3) NULL CONSTRAINT DF_Companies_CreatedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_Companies PRIMARY KEY (CompanyID)
);
GO

CREATE TABLE dbo.Branches (
    BranchID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    BranchName NVARCHAR(150) NOT NULL,
    Address NVARCHAR(500) NULL,
    Phone NVARCHAR(50) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Branches_IsActive DEFAULT (1),
    CONSTRAINT PK_Branches PRIMARY KEY (BranchID),
    CONSTRAINT FK_Branches_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID)
);
GO

CREATE TABLE dbo.Departments (
    DepartmentID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    BranchID INT NULL,
    ParentDepartmentID INT NULL,
    DepartmentName NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CONSTRAINT PK_Departments PRIMARY KEY (DepartmentID),
    CONSTRAINT FK_Departments_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_Departments_BranchID FOREIGN KEY (BranchID) REFERENCES dbo.Branches(BranchID) ON DELETE SET NULL,
    CONSTRAINT FK_Departments_ParentDepartmentID FOREIGN KEY (ParentDepartmentID) REFERENCES dbo.Departments(DepartmentID)
);
GO

CREATE TABLE dbo.Users (
    UserID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    BranchID INT NULL,
    DepartmentID INT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_Users_IsActive DEFAULT (1),
    CreatedAt DATETIME2(3) NULL CONSTRAINT DF_Users_CreatedAt DEFAULT (SYSDATETIME()),
    UpdatedAt DATETIME2(3) NULL CONSTRAINT DF_Users_UpdatedAt DEFAULT (SYSDATETIME()),
    LastLoginAt DATETIME2(3) NULL,
    CONSTRAINT PK_Users PRIMARY KEY (UserID),
    CONSTRAINT UQ_Users_Email UNIQUE (Email),
    CONSTRAINT FK_Users_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_Users_BranchID FOREIGN KEY (BranchID) REFERENCES dbo.Branches(BranchID) ON DELETE SET NULL,
    CONSTRAINT FK_Users_DepartmentID FOREIGN KEY (DepartmentID) REFERENCES dbo.Departments(DepartmentID)
);
GO

CREATE TABLE dbo.Roles (
    RoleID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NULL,
    RoleName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CONSTRAINT PK_Roles PRIMARY KEY (RoleID),
    CONSTRAINT FK_Roles_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID)
);
GO

CREATE TABLE dbo.UserRoles (
    UserID INT NOT NULL,
    RoleID INT NOT NULL,
    AssignedAt DATETIME2(3) NULL CONSTRAINT DF_UserRoles_AssignedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_UserRoles PRIMARY KEY (UserID, RoleID),
    CONSTRAINT FK_UserRoles_UserID FOREIGN KEY (UserID) REFERENCES dbo.Users(UserID) ON DELETE CASCADE,
    CONSTRAINT FK_UserRoles_RoleID FOREIGN KEY (RoleID) REFERENCES dbo.Roles(RoleID) ON DELETE CASCADE
);
GO

CREATE TABLE dbo.Permissions (
    PermissionID INT IDENTITY(1,1) NOT NULL,
    PermissionName NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CONSTRAINT PK_Permissions PRIMARY KEY (PermissionID),
    CONSTRAINT UQ_Permissions_PermissionName UNIQUE (PermissionName)
);
GO

CREATE TABLE dbo.RolePermissions (
    RoleID INT NOT NULL,
    PermissionID INT NOT NULL,
    CONSTRAINT PK_RolePermissions PRIMARY KEY (RoleID, PermissionID),
    CONSTRAINT FK_RolePermissions_RoleID FOREIGN KEY (RoleID) REFERENCES dbo.Roles(RoleID) ON DELETE CASCADE,
    CONSTRAINT FK_RolePermissions_PermissionID FOREIGN KEY (PermissionID) REFERENCES dbo.Permissions(PermissionID) ON DELETE CASCADE
);
GO

-- ---------- UpdatedAt triggers (replaces MySQL ON UPDATE CURRENT_TIMESTAMP) ----------
CREATE TRIGGER dbo.TR_Users_UpdatedAt
ON dbo.Users
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRIGGER_NESTLEVEL(OBJECT_ID('dbo.TR_Users_UpdatedAt')) > 1 RETURN;
    UPDATE t SET UpdatedAt = SYSDATETIME()
    FROM dbo.Users AS t
    INNER JOIN inserted AS i ON i.UserID = t.UserID;
END;
GO

-- ---------- SEED DATA ----------
SET IDENTITY_INSERT dbo.Companies ON;
INSERT INTO dbo.Companies (CompanyID, CompanyName, TaxNumber, Email, Phone, Street, City, Country, CurrencyCode, IsActive)
VALUES
    (1, N'Advanced Technology LLC', N'TAX-987654321', N'info@advancedtech.com', N'+966112233445', N'King Fahd Street', N'Riyadh', N'Saudi Arabia', N'SAR', 1);
SET IDENTITY_INSERT dbo.Companies OFF;
GO

SET IDENTITY_INSERT dbo.Branches ON;
INSERT INTO dbo.Branches (BranchID, CompanyID, BranchName, Phone, IsActive, Address)
VALUES
    (1, 1, N'Main Branch - Riyadh', N'+966112233446', 1, N'Engineer Fahd Road, Riyadh, Saudi Arabia');
SET IDENTITY_INSERT dbo.Branches OFF;
GO

SET IDENTITY_INSERT dbo.Departments ON;
INSERT INTO dbo.Departments (DepartmentID, CompanyID, BranchID, ParentDepartmentID, DepartmentName, Description)
VALUES
    (1, 1, 1, NULL, N'Finance Department', N'Department responsible for accounts and budgets'),
    (2, 1, 1, NULL, N'Procurement Department', N'Department responsible for company supplies');
SET IDENTITY_INSERT dbo.Departments OFF;
GO

SET IDENTITY_INSERT dbo.Users ON;
INSERT INTO dbo.Users (UserID, CompanyID, BranchID, DepartmentID, FirstName, LastName, Email, PasswordHash, IsActive)
VALUES
    (1, 1, 1, 1, N'Ahmed', N'Mohamed', N'ahmed.mohamed@advancedtech.com', N'$2y$10$abcdef...', 1),
    (2, 1, 1, 2, N'Sara', N'Ali', N'sara.ali@advancedtech.com', N'$2y$10$abcdef...', 1);
SET IDENTITY_INSERT dbo.Users OFF;
GO

SET IDENTITY_INSERT dbo.Roles ON;
INSERT INTO dbo.Roles (RoleID, CompanyID, RoleName, Description)
VALUES
    (1, 1, N'System Administrator', N'Full privileges on the system'),
    (2, 1, N'Procurement Officer', N'Manage purchase requests and purchase orders');
SET IDENTITY_INSERT dbo.Roles OFF;
GO

INSERT INTO dbo.UserRoles (UserID, RoleID)
VALUES
    (1, 1),
    (2, 2);
GO

SET IDENTITY_INSERT dbo.Permissions ON;
INSERT INTO dbo.Permissions (PermissionID, PermissionName, Description)
VALUES
    (1, N'MANAGE_USERS', N'Manage users'),
    (2, N'CREATE_PURCHASE_REQUEST', N'Create purchase request');
SET IDENTITY_INSERT dbo.Permissions OFF;
GO

INSERT INTO dbo.RolePermissions (RoleID, PermissionID)
VALUES
    (1, 1),
    (1, 2),
    (2, 2);
GO

-- ---------- CHECK QUERY ----------
SELECT u.UserID, CONCAT(u.FirstName, N' ', u.LastName) AS FullName, u.Email, d.DepartmentName, r.RoleName, p.PermissionName
FROM dbo.Users u
LEFT JOIN dbo.Departments d ON u.DepartmentID = d.DepartmentID
LEFT JOIN dbo.UserRoles ur ON u.UserID = ur.UserID
LEFT JOIN dbo.Roles r ON ur.RoleID = r.RoleID
LEFT JOIN dbo.RolePermissions rp ON r.RoleID = rp.RoleID
LEFT JOIN dbo.Permissions p ON rp.PermissionID = p.PermissionID;
GO
