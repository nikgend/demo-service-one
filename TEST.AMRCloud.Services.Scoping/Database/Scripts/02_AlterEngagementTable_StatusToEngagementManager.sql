-- ================================================================
-- Database Script: Alter Engagement Table
-- Project: TEST.AMRCloud.Services.Scoping
-- Description: Migrates Status column to EngagementManager
-- ================================================================

USE TestScopingDb;

-- ================================================================
-- Drop existing indexes that reference Status
-- ================================================================

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_EngagementStatus')
BEGIN
    DROP INDEX [IX_EngagementStatus] ON [dbo].[Engagements];
    PRINT 'Index IX_EngagementStatus dropped.';
END;

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_Engagements_Status_IsActive')
BEGIN
    DROP INDEX [IX_Engagements_Status_IsActive] ON [dbo].[Engagements];
    PRINT 'Index IX_Engagements_Status_IsActive dropped.';
END;

-- ================================================================
-- Drop the view that references Status
-- ================================================================

IF OBJECT_ID('[dbo].[vw_EngagementSummary]', 'V') IS NOT NULL
BEGIN
    DROP VIEW [dbo].[vw_EngagementSummary];
    PRINT 'View vw_EngagementSummary dropped.';
END;

-- ================================================================
-- Alter table: Drop Status column and Add EngagementManager column
-- ================================================================

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'Status')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    DROP COLUMN [Status];
    PRINT 'Column Status dropped from Engagements table.';
END;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'EngagementManager')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    ADD [EngagementManager] NVARCHAR(255) NULL;
    PRINT 'Column EngagementManager added to Engagements table.';
END;

-- ================================================================
-- Create new indexes
-- ================================================================

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_EngagementManager')
BEGIN
    CREATE INDEX [IX_EngagementManager] ON [dbo].[Engagements]([EngagementManager]);
    PRINT 'Index IX_EngagementManager created.';
END;

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_Engagements_EngagementManager_IsActive')
BEGIN
    CREATE INDEX [IX_Engagements_EngagementManager_IsActive] ON [dbo].[Engagements]([EngagementManager], [IsActive]);
    PRINT 'Index IX_Engagements_EngagementManager_IsActive created.';
END;

-- ================================================================
-- Recreate view with EngagementManager
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

    PRINT 'View vw_EngagementSummary created with EngagementManager column.';
END;

-- ================================================================
-- Verification
-- ================================================================

PRINT '';
PRINT '========== MIGRATION COMPLETE ==========';
PRINT '';
PRINT 'Updated table structure:';
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
