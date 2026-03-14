-- ================================================================
-- Database Script: Engagement Table Creation
-- Project: TEST.AMRCloud.Services.Scoping
-- Description: Creates the Engagements table for engagement management
-- ================================================================

-- Create Engagements Table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Engagements')
BEGIN
    CREATE TABLE [dbo].[Engagements] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [EngagementName] NVARCHAR(255) NULL,
        [EngagementCode] NVARCHAR(50) NULL,
        [ClientName] NVARCHAR(255) NULL,
        [EngagementManager] NVARCHAR(255) NULL,
        [StartDate] DATETIME2 NULL,
        [EndDate] DATETIME2 NULL,
        [CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [CreatedBy] NVARCHAR(255) NULL,
        [ModifiedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [ModifiedBy] NVARCHAR(255) NULL,
        [IsActive] BIT NOT NULL DEFAULT 1
    );

    PRINT 'Engagements table created successfully.';
END
ELSE
BEGIN
    PRINT 'Engagements table already exists.';
END;

-- ================================================================
-- Create Indexes for Performance
-- ================================================================

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_EngagementManager')
BEGIN
    CREATE INDEX [IX_EngagementManager] ON [dbo].[Engagements]([EngagementManager]);
    PRINT 'Index IX_EngagementManager created.';
END;

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_EngagementCreatedDate')
BEGIN
    CREATE INDEX [IX_EngagementCreatedDate] ON [dbo].[Engagements]([CreatedDate]);
    PRINT 'Index IX_EngagementCreatedDate created.';
END;

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_EngagementIsActive')
BEGIN
    CREATE INDEX [IX_EngagementIsActive] ON [dbo].[Engagements]([IsActive]);
    PRINT 'Index IX_EngagementIsActive created.';
END;

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_Engagements_EngagementManager_IsActive')
BEGIN
    CREATE INDEX [IX_Engagements_EngagementManager_IsActive] ON [dbo].[Engagements]([EngagementManager], [IsActive]);
    PRINT 'Index IX_Engagements_EngagementManager_IsActive created.';
END;

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_Engagements_ClientName')
BEGIN
    CREATE INDEX [IX_Engagements_ClientName] ON [dbo].[Engagements]([ClientName]);
    PRINT 'Index IX_Engagements_ClientName created.';
END;

-- ================================================================
-- Update Funds Table to add Foreign Key to Engagements
-- ================================================================

IF NOT EXISTS (
    SELECT CONSTRAINT_NAME
    FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
    WHERE TABLE_NAME = 'Funds' AND COLUMN_NAME = 'EngagementId' AND CONSTRAINT_NAME LIKE 'FK%'
)
BEGIN
    ALTER TABLE [dbo].[Funds]
    ADD CONSTRAINT [FK_Funds_Engagements]
    FOREIGN KEY ([EngagementId])
    REFERENCES [dbo].[Engagements]([Id])
    ON DELETE CASCADE;

    PRINT 'Foreign key FK_Funds_Engagements created successfully.';
END
ELSE
BEGIN
    PRINT 'Foreign key already exists on Funds table.';
END;

-- ================================================================
-- View Creation for Reporting (Optional)
-- ================================================================

IF OBJECT_ID('[dbo].[vw_EngagementSummary]', 'V') IS NULL
BEGIN
    EXEC sp_executesql N'
    CREATE VIEW [dbo].[vw_EngagementSummary] AS
    SELECT
        e.[Id],
        e.[EngagementName],
        e.[EngagementCode],
        e.[ClientName],
        e.[EngagementManager],
        e.[StartDate],
        e.[EndDate],
        e.[CreatedDate],
        e.[CreatedBy],
        COUNT(f.[Id]) AS [FundCount],
        e.[IsActive]
    FROM [dbo].[Engagements] e
    LEFT JOIN [dbo].[Funds] f ON e.[Id] = f.[EngagementId]
    WHERE e.[IsActive] = 1
    GROUP BY e.[Id], e.[EngagementName], e.[EngagementCode], e.[ClientName], e.[EngagementManager],
             e.[StartDate], e.[EndDate], e.[CreatedDate], e.[CreatedBy], e.[IsActive]';

    PRINT 'View vw_EngagementSummary created.';
END
ELSE
BEGIN
    PRINT 'View vw_EngagementSummary already exists.';
END;

-- ================================================================
-- Verification Queries
-- ================================================================

PRINT '';
PRINT '========== VERIFICATION ==========';
PRINT '';

-- Check table structure
PRINT 'Table columns:';
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Engagements'
ORDER BY ORDINAL_POSITION;

PRINT '';
PRINT 'Table indexes:';
SELECT name AS IndexName, type_desc AS IndexType
FROM sys.indexes
WHERE object_id = OBJECT_ID('Engagements')
AND name IS NOT NULL;

PRINT '';
PRINT 'Table constraints:';
SELECT CONSTRAINT_NAME, CONSTRAINT_TYPE
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE TABLE_NAME = 'Engagements';

PRINT '';
PRINT '========== SETUP COMPLETE ==========';
