CREATE TABLE [dbo].[Course] (
    [CourseId]     INT            IDENTITY (1, 1) NOT NULL,
    [CourseName]   VARCHAR (255)  NOT NULL,
    [CourseRating] DECIMAL (3, 1) NOT NULL,
    [Slope]        INT            NOT NULL,
    [Par]          INT            CONSTRAINT [DF_Course_Par] DEFAULT ((72)) NOT NULL,
    CONSTRAINT [PK__Course] PRIMARY KEY CLUSTERED ([CourseId] ASC)
);


GO

