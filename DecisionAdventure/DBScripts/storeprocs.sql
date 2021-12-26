/****** Object:  StoredProcedure [dbo].[InsertAdventurePath]    Script Date: 12/27/2021 1:28:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InsertAdventurePath]
	@ID uniqueidentifier,
	@AdventureID uniqueidentifier,
	@PreviousAnswer uniqueidentifier,
	@Question NVARCHAR(200),
	@Level int,
	@Options AdventurePathOption readonly,
    @Error NVARCHAR(MAX) OUTPUT
AS
BEGIN
 
    BEGIN TRANSACTION;
 
    BEGIN TRY
		--insert Question
		INSERT INTO [dbo].[AdventurePath] 
			(
				[ID],
				[AdventureID],
				[PreviousAnswer],
				[Question],
				[Level]
			) 
			Values 
			(
				@ID,
				@AdventureID,
				@PreviousAnswer,
				@Question,
				@Level
			)

			INSERT INTO [dbo].[AdventurePathOption] (ID, PathID, Label)
			SELECT ID, AdventureID, Label
			From @Options
        
    END TRY
    BEGIN CATCH
 
        SET @Error = 'Error Number: ' + CAST(ERROR_NUMBER() AS VARCHAR(10)) + '; ' + Char(10) +
        'Error Severity: ' + CAST(ERROR_SEVERITY() AS VARCHAR(10)) + '; ' + Char(10) +
        'Error State: ' + CAST(ERROR_STATE() AS VARCHAR(10)) + '; ' + Char(10) +
        'Error Line: ' + CAST(ERROR_LINE() AS VARCHAR(10)) + '; ' + Char(10) +
        'Error Message: ' + ERROR_MESSAGE()
 
        IF @@TRANCOUNT > 0  
            ROLLBACK TRANSACTION;
    END CATCH
 
    IF @@TRANCOUNT > 0  
        COMMIT TRANSACTION;
 
END
GO


