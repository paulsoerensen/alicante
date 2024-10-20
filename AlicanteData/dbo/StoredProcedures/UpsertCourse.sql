CREATE PROCEDURE [dbo].[UpsertCourse]
    @CourseId INT = NULL, -- Nullable, since this might be a new course
    @CourseName VARCHAR(255),
    @CourseRating DECIMAL(3, 1),
    @Slope INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare a table variable to hold the output (the modified record)
    DECLARE @ModifiedRecords TABLE
    (
        CourseId INT,
        CourseName VARCHAR(255),
        CourseRating DECIMAL(3, 1),
        Slope INT
    );

    -- Merge operation between Source (S) and Target (T) tables
    MERGE INTO [dbo].[Course] AS T
    USING (SELECT @CourseId AS CourseId, @CourseName AS CourseName, @CourseRating AS CourseRating, @Slope AS Slope) AS S
    ON T.CourseId = S.CourseId
    WHEN MATCHED THEN
        UPDATE SET 
            T.CourseName = S.CourseName,
            T.CourseRating = S.CourseRating,
            T.Slope = S.Slope
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (CourseName, CourseRating, Slope)
        VALUES (S.CourseName, S.CourseRating, S.Slope)
    OUTPUT inserted.CourseId, inserted.CourseName, inserted.CourseRating, inserted.Slope
    INTO @ModifiedRecords;

    -- Return the modified record(s)
    SELECT * FROM @ModifiedRecords;
END;

GO

