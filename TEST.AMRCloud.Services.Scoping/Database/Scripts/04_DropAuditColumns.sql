-- ================================================================
-- Database Script: Drop Audit Trail Columns
-- Project: TEST.AMRCloud.Services.Scoping
-- Description: Removes audit trail columns (CreatedDate, CreatedBy, 
--              ModifiedDate, ModifiedBy, IsActive) from all tables
-- ================================================================

USE TestScopingDb;

-- ================================================================
-- Drop dependent constraints and indexes first
-- ================================================================

-- Drop indexes that depend on CreatedDate
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_EngagementCreatedDate')
BEGIN
    DROP INDEX [IX_EngagementCreatedDate] ON [dbo].[Engagements];
    PRINT 'Index IX_EngagementCreatedDate dropped.';
END;

-- Drop indexes that depend on IsActive
IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_EngagementIsActive')
BEGIN
    DROP INDEX [IX_EngagementIsActive] ON [dbo].[Engagements];
    PRINT 'Index IX_EngagementIsActive dropped.';
END;

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('Engagements') AND name = 'IX_Engagements_EngagementManager_IsActive')
BEGIN
    DROP INDEX [IX_Engagements_EngagementManager_IsActive] ON [dbo].[Engagements];
    PRINT 'Index IX_Engagements_EngagementManager_IsActive dropped.';
END;

-- Drop default constraints before dropping columns
DECLARE @ConstraintName NVARCHAR(200);
DECLARE @SQL NVARCHAR(MAX) = '';

-- Alternative approach - drop all default constraints on these columns
SELECT @SQL = @SQL + 'ALTER TABLE [dbo].[' + t.name + '] DROP CONSTRAINT ' + d.name + ';' + CHAR(10)
FROM sys.default_constraints d
INNER JOIN sys.columns c ON d.parent_object_id = c.object_id AND d.parent_column_id = c.column_id
INNER JOIN sys.tables t ON d.parent_object_id = t.object_id
WHERE c.name IN ('CreatedDate', 'CreatedBy', 'ModifiedDate', 'ModifiedBy', 'IsActive')
AND t.name IN ('Engagements', 'Funds', 'Routines', 'FundRoutineMappings', 'ScopingDetails');

IF @SQL <> ''
BEGIN
    EXEC sp_executesql @SQL;
    PRINT 'Default constraints dropped.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'CreatedDate')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    DROP COLUMN [CreatedDate];
    PRINT 'Column CreatedDate dropped from Engagements table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'CreatedBy')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    DROP COLUMN [CreatedBy];
    PRINT 'Column CreatedBy dropped from Engagements table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'ModifiedDate')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    DROP COLUMN [ModifiedDate];
    PRINT 'Column ModifiedDate dropped from Engagements table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'ModifiedBy')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    DROP COLUMN [ModifiedBy];
    PRINT 'Column ModifiedBy dropped from Engagements table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Engagements' AND COLUMN_NAME = 'IsActive')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    DROP COLUMN [IsActive];
    PRINT 'Column IsActive dropped from Engagements table.';
END;

-- ================================================================
-- Drop audit columns from Funds table
-- ================================================================

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Funds' AND COLUMN_NAME = 'CreatedDate')
BEGIN
    ALTER TABLE [dbo].[Funds]
    DROP COLUMN [CreatedDate];
    PRINT 'Column CreatedDate dropped from Funds table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Funds' AND COLUMN_NAME = 'CreatedBy')
BEGIN
    ALTER TABLE [dbo].[Funds]
    DROP COLUMN [CreatedBy];
    PRINT 'Column CreatedBy dropped from Funds table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Funds' AND COLUMN_NAME = 'ModifiedDate')
BEGIN
    ALTER TABLE [dbo].[Funds]
    DROP COLUMN [ModifiedDate];
    PRINT 'Column ModifiedDate dropped from Funds table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Funds' AND COLUMN_NAME = 'ModifiedBy')
BEGIN
    ALTER TABLE [dbo].[Funds]
    DROP COLUMN [ModifiedBy];
    PRINT 'Column ModifiedBy dropped from Funds table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Funds' AND COLUMN_NAME = 'IsActive')
BEGIN
    ALTER TABLE [dbo].[Funds]
    DROP COLUMN [IsActive];
    PRINT 'Column IsActive dropped from Funds table.';
END;

-- ================================================================
-- Drop audit columns from Routines table
-- ================================================================

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Routines' AND COLUMN_NAME = 'CreatedDate')
BEGIN
    ALTER TABLE [dbo].[Routines]
    DROP COLUMN [CreatedDate];
    PRINT 'Column CreatedDate dropped from Routines table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Routines' AND COLUMN_NAME = 'CreatedBy')
BEGIN
    ALTER TABLE [dbo].[Routines]
    DROP COLUMN [CreatedBy];
    PRINT 'Column CreatedBy dropped from Routines table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Routines' AND COLUMN_NAME = 'ModifiedDate')
BEGIN
    ALTER TABLE [dbo].[Routines]
    DROP COLUMN [ModifiedDate];
    PRINT 'Column ModifiedDate dropped from Routines table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Routines' AND COLUMN_NAME = 'ModifiedBy')
BEGIN
    ALTER TABLE [dbo].[Routines]
    DROP COLUMN [ModifiedBy];
    PRINT 'Column ModifiedBy dropped from Routines table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Routines' AND COLUMN_NAME = 'IsActive')
BEGIN
    ALTER TABLE [dbo].[Routines]
    DROP COLUMN [IsActive];
    PRINT 'Column IsActive dropped from Routines table.';
END;

-- ================================================================
-- Drop audit columns from FundRoutineMappings table
-- ================================================================

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FundRoutineMappings' AND COLUMN_NAME = 'CreatedDate')
BEGIN
    ALTER TABLE [dbo].[FundRoutineMappings]
    DROP COLUMN [CreatedDate];
    PRINT 'Column CreatedDate dropped from FundRoutineMappings table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FundRoutineMappings' AND COLUMN_NAME = 'CreatedBy')
BEGIN
    ALTER TABLE [dbo].[FundRoutineMappings]
    DROP COLUMN [CreatedBy];
    PRINT 'Column CreatedBy dropped from FundRoutineMappings table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FundRoutineMappings' AND COLUMN_NAME = 'ModifiedDate')
BEGIN
    ALTER TABLE [dbo].[FundRoutineMappings]
    DROP COLUMN [ModifiedDate];
    PRINT 'Column ModifiedDate dropped from FundRoutineMappings table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FundRoutineMappings' AND COLUMN_NAME = 'ModifiedBy')
BEGIN
    ALTER TABLE [dbo].[FundRoutineMappings]
    DROP COLUMN [ModifiedBy];
    PRINT 'Column ModifiedBy dropped from FundRoutineMappings table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'FundRoutineMappings' AND COLUMN_NAME = 'IsActive')
BEGIN
    ALTER TABLE [dbo].[FundRoutineMappings]
    DROP COLUMN [IsActive];
    PRINT 'Column IsActive dropped from FundRoutineMappings table.';
END;

-- ================================================================
-- Drop audit columns from ScopingDetails table
-- ================================================================

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ScopingDetails' AND COLUMN_NAME = 'CreatedDate')
BEGIN
    ALTER TABLE [dbo].[ScopingDetails]
    DROP COLUMN [CreatedDate];
    PRINT 'Column CreatedDate dropped from ScopingDetails table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ScopingDetails' AND COLUMN_NAME = 'CreatedBy')
BEGIN
    ALTER TABLE [dbo].[ScopingDetails]
    DROP COLUMN [CreatedBy];
    PRINT 'Column CreatedBy dropped from ScopingDetails table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ScopingDetails' AND COLUMN_NAME = 'ModifiedDate')
BEGIN
    ALTER TABLE [dbo].[ScopingDetails]
    DROP COLUMN [ModifiedDate];
    PRINT 'Column ModifiedDate dropped from ScopingDetails table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ScopingDetails' AND COLUMN_NAME = 'ModifiedBy')
BEGIN
    ALTER TABLE [dbo].[ScopingDetails]
    DROP COLUMN [ModifiedBy];
    PRINT 'Column ModifiedBy dropped from ScopingDetails table.';
END;

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ScopingDetails' AND COLUMN_NAME = 'IsActive')
BEGIN
    ALTER TABLE [dbo].[ScopingDetails]
    DROP COLUMN [IsActive];
    PRINT 'Column IsActive dropped from ScopingDetails table.';
END;

-- ================================================================
-- Verification
-- ================================================================

PRINT '';
PRINT '========== AUDIT COLUMNS REMOVAL COMPLETE ==========';
PRINT '';

PRINT 'Verification - Remaining columns:';
SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME IN ('Engagements', 'Funds', 'Routines', 'FundRoutineMappings', 'ScopingDetails')
ORDER BY TABLE_NAME, ORDINAL_POSITION;

PRINT '';
PRINT 'Audit columns removal migration completed successfully.';
