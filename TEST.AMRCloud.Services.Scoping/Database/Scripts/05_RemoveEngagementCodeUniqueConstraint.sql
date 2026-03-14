-- ================================================================
-- Database Script: Remove Unique Constraint on EngagementCode
-- Project: TEST.AMRCloud.Services.Scoping
-- Description: Removes the unique constraint from EngagementCode field
--              to allow multiple engagements with the same code
-- ================================================================

USE TestScopingDb;

-- ================================================================
-- Drop the unique constraint on EngagementCode
-- ================================================================

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
           WHERE TABLE_NAME = 'Engagements' AND CONSTRAINT_NAME = 'UK_EngagementCode')
BEGIN
    ALTER TABLE [dbo].[Engagements]
    DROP CONSTRAINT [UK_EngagementCode];
    PRINT 'Unique constraint UK_EngagementCode dropped from Engagements table.';
END
ELSE
BEGIN
    PRINT 'Unique constraint UK_EngagementCode does not exist.';
END;

-- ================================================================
-- Verification
-- ================================================================

PRINT '';
PRINT 'Current constraints on Engagements table:';
SELECT CONSTRAINT_NAME, CONSTRAINT_TYPE
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE TABLE_NAME = 'Engagements';
