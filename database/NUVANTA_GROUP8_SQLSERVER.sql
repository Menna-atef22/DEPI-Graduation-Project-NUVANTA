-- =====================================================
-- NUVANTA | SQL Server | GROUP 8: Approvals & Audit
-- Run groups in order 1 -> 8 (SSMS / sqlcmd, uses GO)
-- =====================================================
USE nuvanta;
GO

-- ---------- TABLES ----------
CREATE TABLE dbo.ApprovalRules (
    ApprovalRuleID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    RuleName NVARCHAR(150) NOT NULL,
    EntityType NVARCHAR(100) NOT NULL,
    MinAmount DECIMAL(18,4) NOT NULL CONSTRAINT DF_ApprovalRules_MinAmount DEFAULT (0),
    MaxAmount DECIMAL(18,4) NULL,
    Priority INT NOT NULL CONSTRAINT DF_ApprovalRules_Priority DEFAULT (0),
    IsActive BIT NOT NULL CONSTRAINT DF_ApprovalRules_IsActive DEFAULT (1),
    CONSTRAINT PK_ApprovalRules PRIMARY KEY (ApprovalRuleID),
    CONSTRAINT FK_ApprovalRules_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID)
);
GO

CREATE TABLE dbo.ApprovalRequests (
    ApprovalRequestID INT IDENTITY(1,1) NOT NULL,
    ApprovalRuleID INT NOT NULL,
    RequestedBy INT NOT NULL,
    EntityType NVARCHAR(100) NOT NULL,
    EntityID INT NOT NULL,
    SubmittedAt DATETIME2(3) NULL CONSTRAINT DF_ApprovalRequests_SubmittedAt DEFAULT (SYSDATETIME()),
    Status NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_ApprovalRequests PRIMARY KEY (ApprovalRequestID),
    CONSTRAINT FK_ApprovalRequests_ApprovalRuleID FOREIGN KEY (ApprovalRuleID) REFERENCES dbo.ApprovalRules(ApprovalRuleID),
    CONSTRAINT FK_ApprovalRequests_RequestedBy FOREIGN KEY (RequestedBy) REFERENCES dbo.Users(UserID)
);
GO

CREATE TABLE dbo.ApprovalSteps (
    ApprovalStepID INT IDENTITY(1,1) NOT NULL,
    ApprovalRequestID INT NOT NULL,
    StepNumber INT NOT NULL,
    AssignedTo INT NULL,
    RequiredRoleID INT NULL,
    IsRequired BIT NOT NULL CONSTRAINT DF_ApprovalSteps_IsRequired DEFAULT (1),
    Status NVARCHAR(50) NOT NULL,
    DecisionDate DATETIME2(3) NULL,
    CONSTRAINT PK_ApprovalSteps PRIMARY KEY (ApprovalStepID),
    CONSTRAINT FK_ApprovalSteps_ApprovalRequestID FOREIGN KEY (ApprovalRequestID) REFERENCES dbo.ApprovalRequests(ApprovalRequestID) ON DELETE CASCADE,
    CONSTRAINT FK_ApprovalSteps_AssignedTo FOREIGN KEY (AssignedTo) REFERENCES dbo.Users(UserID) ON DELETE SET NULL,
    CONSTRAINT FK_ApprovalSteps_RequiredRoleID FOREIGN KEY (RequiredRoleID) REFERENCES dbo.Roles(RoleID) ON DELETE SET NULL
);
GO

CREATE TABLE dbo.ApprovalActions (
    ApprovalActionID INT IDENTITY(1,1) NOT NULL,
    ApprovalStepID INT NOT NULL,
    ActionBy INT NOT NULL,
    ActionType NVARCHAR(50) NOT NULL,
    ActionDate DATETIME2(3) NULL CONSTRAINT DF_ApprovalActions_ActionDate DEFAULT (SYSDATETIME()),
    Comments NVARCHAR(MAX) NULL,
    CONSTRAINT PK_ApprovalActions PRIMARY KEY (ApprovalActionID),
    CONSTRAINT FK_ApprovalActions_ApprovalStepID FOREIGN KEY (ApprovalStepID) REFERENCES dbo.ApprovalSteps(ApprovalStepID) ON DELETE CASCADE,
    CONSTRAINT FK_ApprovalActions_ActionBy FOREIGN KEY (ActionBy) REFERENCES dbo.Users(UserID)
);
GO

CREATE TABLE dbo.AuditLogs (
    AuditLogID INT IDENTITY(1,1) NOT NULL,
    CompanyID INT NOT NULL,
    UserID INT NULL,
    ActionType NVARCHAR(50) NOT NULL,
    EntityType NVARCHAR(100) NOT NULL,
    EntityID INT NULL,
    OldValues NVARCHAR(MAX) NULL,
    NewValues NVARCHAR(MAX) NULL,
    IPAddress NVARCHAR(45) NULL,
    UserAgent NVARCHAR(MAX) NULL,
    CorrelationID NVARCHAR(100) NULL,
    CreatedAt DATETIME2(3) NULL CONSTRAINT DF_AuditLogs_CreatedAt DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_AuditLogs PRIMARY KEY (AuditLogID),
    CONSTRAINT FK_AuditLogs_CompanyID FOREIGN KEY (CompanyID) REFERENCES dbo.Companies(CompanyID),
    CONSTRAINT FK_AuditLogs_UserID FOREIGN KEY (UserID) REFERENCES dbo.Users(UserID) ON DELETE SET NULL
);
GO

-- ---------- SEED DATA ----------
SET IDENTITY_INSERT dbo.ApprovalRules ON;
INSERT INTO dbo.ApprovalRules (ApprovalRuleID, CompanyID, RuleName, EntityType, MinAmount, MaxAmount, Priority, IsActive)
VALUES
    (1, 1, N'General Purchase Approval', N'PurchaseRequest', 0.0000, 50000.0000, 1, 1);
SET IDENTITY_INSERT dbo.ApprovalRules OFF;
GO

SET IDENTITY_INSERT dbo.ApprovalRequests ON;
INSERT INTO dbo.ApprovalRequests (ApprovalRequestID, ApprovalRuleID, RequestedBy, EntityType, EntityID, Status)
VALUES
    (1, 1, 2, N'PurchaseRequest', 1, N'Approved');
SET IDENTITY_INSERT dbo.ApprovalRequests OFF;
GO

SET IDENTITY_INSERT dbo.ApprovalSteps ON;
INSERT INTO dbo.ApprovalSteps (ApprovalStepID, ApprovalRequestID, StepNumber, AssignedTo, RequiredRoleID, IsRequired, Status, DecisionDate)
VALUES
    (1, 1, 1, 1, 1, 1, N'Approved', N'2026-03-01 10:00:00');
SET IDENTITY_INSERT dbo.ApprovalSteps OFF;
GO

SET IDENTITY_INSERT dbo.ApprovalActions ON;
INSERT INTO dbo.ApprovalActions (ApprovalActionID, ApprovalStepID, ActionBy, ActionType, ActionDate, Comments)
VALUES
    (1, 1, 1, N'Approve', N'2026-03-01 10:00:00', N'Approved successfully, budget is available.');
SET IDENTITY_INSERT dbo.ApprovalActions OFF;
GO

SET IDENTITY_INSERT dbo.AuditLogs ON;
INSERT INTO dbo.AuditLogs (AuditLogID, CompanyID, UserID, ActionType, EntityType, EntityID, OldValues, NewValues, IPAddress, UserAgent, CorrelationID)
VALUES
    (1, 1, 2, N'CREATE', N'PurchaseRequest', 1, NULL, N'{"PurchaseRequestID": 1, "Status": "Submitted", "Amount": 15000}', N'192.168.1.50', N'Mozilla/5.0', N'CORR-ABC-12345');
SET IDENTITY_INSERT dbo.AuditLogs OFF;
GO

-- ---------- CHECK QUERY ----------
SELECT ar.ApprovalRequestID, ar.EntityType, ar.EntityID, ar.Status AS RequestStatus, ast.StepNumber,
       CONCAT(u.FirstName, N' ', u.LastName) AS AssignedToUser, r.RoleName AS RequiredRole, ast.Status AS StepStatus, aa.ActionType, aa.Comments
FROM dbo.ApprovalRequests ar
JOIN dbo.ApprovalSteps ast ON ar.ApprovalRequestID = ast.ApprovalRequestID
LEFT JOIN dbo.Users u ON ast.AssignedTo = u.UserID
LEFT JOIN dbo.Roles r ON ast.RequiredRoleID = r.RoleID
LEFT JOIN dbo.ApprovalActions aa ON ast.ApprovalStepID = aa.ApprovalStepID;
SELECT TOP (20) al.AuditLogID, al.CreatedAt, CONCAT(u.FirstName, N' ', u.LastName) AS UserName, al.ActionType, al.EntityType, al.EntityID, al.NewValues, al.IPAddress
FROM dbo.AuditLogs al LEFT JOIN dbo.Users u ON al.UserID = u.UserID
ORDER BY al.CreatedAt DESC;
GO
