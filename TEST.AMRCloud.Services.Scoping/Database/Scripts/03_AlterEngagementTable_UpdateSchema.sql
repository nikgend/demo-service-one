-- ================================================================
-- Database Script: Alter Engagement Table Schema
-- Project: TEST.AMRCloud.Services.Scoping
-- Description: Updates Engagement table to new structure
--              Removes ClientName, StartDate, EndDate
--              Adds EngagementPartner, PeriodEndDate
-- ================================================================

USE TestScopingDb;

-- ================================================================
-- Drop existing indexes that reference old columns
-- ================================================================

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_Engagements_ClientName')
BEGIN
    DROP INDEX [IX_Engagements_ClientName] ON [dbo].[Engagements];
    PRINT 'Index IX_Engagements_ClientName dropped.';
END;

-- ================================================================
-- Drop and recreate the view (will be recreated after table changes)
-- ================================================================

IF OBJECT_ID('[dbo].[vw_EngagementSummary]', 'V') IS NOT NULL
BEGIN
    DROP VIEW [dbo].[vw_EngagementSummary];
    PRINT 'View vw_EngagementSummary dropped.';
END;

-- ================================================================
-- Alter table: Remove unused columns
-- ================================================================

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'ClientName')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    DROP COLUMN [ClientName];
    PRINT 'Column ClientName dropped from Engagements table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'StartDate')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    DROP COLUMN [StartDate];
    PRINT 'Column StartDate dropped from Engagements table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'EndDate')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    DROP COLUMN [EndDate];
    PRINT 'Column EndDate dropped from Engagements table.';
END;

-- ================================================================
-- Add new columns
-- ================================================================

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'EngagementPartner')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    ADD [EngagementPartner] NVARCHAR(255) NULL;
    PRINT 'Column EngagementPartner added to Engagements table.';
END;

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'PeriodEndDate')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    ADD [PeriodEndDate] DATETIME2 NULL;
    PRINT 'Column PeriodEndDate added to Engagements table.';
END;

-- ================================================================
-- Create new indexes
-- ================================================================

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_EngagementPartner')
BEGIN
    CREATE INDEX [IX_EngagementPartner] ON [dbo].[Engagements]([EngagementPartner]);
    PRINT 'Index IX_EngagementPartner created.';
END;

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_EngagementPeriodEndDate')
BEGIN
    CREATE INDEX [IX_EngagementPeriodEndDate] ON [dbo].[Engagements]([PeriodEndDate]);
    PRINT 'Index IX_EngagementPeriodEndDate created.';
END;

-- ================================================================
-- Recreate view with new columns
-- ================================================================

IF OBJECT_ID('[dbo].[vw_EngagementSummary]', 'V') IS NULL
BEGIN
    EXEC sp_executesql N'
    CREATE VIEW [dbo].[vw_EngagementSummary] AS
    SELECT
        e.[Id],
        e.[EngagementName],
        e.[EngagementCode],
        e.[EngagementManager],
        e.[EngagementPartner],
        e.[PeriodEndDate],
        e.[CreatedDate],
        e.[CreatedBy],
        COUNT(f.[Id]) AS [FundCount],
        e.[IsActive]
    FROM [dbo].[Engagements] e
    LEFT JOIN [dbo].[Funds] f ON e.[Id] = f.[EngagementId]
    WHERE e.[IsActive] = 1
    GROUP BY e.[Id], e.[EngagementName], e.[EngagementCode], e.[EngagementManager], e.[EngagementPartner],
             e.[PeriodEndDate], e.[CreatedDate], e.[CreatedBy], e.[IsActive]';

    PRINT 'View vw_EngagementSummary recreated with new columns.';
END;

-- ================================================================
-- Verification
-- ================================================================

PRINT '';
PRINT '========== SCHEMA UPDATE COMPLETE ==========';
PRINT '';
PRINT 'Updated Engagements table structure:';
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Engagements'
ORDER BY ORDINAL_POSITION;

PRINT '';
PRINT 'Engagements table indexes:';
SELECT name AS IndexName, type_desc AS IndexType
FROM sys.indexes
WHERE object_id = OBJECT_ID('Engagements')
AND name IS NOT NULL
ORDER BY name;

PRINT '';
PRINT 'Schema update migration completed successfully.';
