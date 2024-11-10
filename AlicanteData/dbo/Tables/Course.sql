CREATE TABLE [dbo].[Course] (
    [CourseId]     INT            IDENTITY (1, 1) NOT NULL,
    [CourseName]   VARCHAR (255)  NOT NULL,
    [CourseRating] DECIMAL (3, 1) NOT NULL,
    [Slope]        INT            NOT NULL,
    [Par]          INT            NOT NULL
);
GO

ALTER TABLE [dbo].[Course]
    ADD CONSTRAINT [PK__Course] PRIMARY KEY CLUSTERED ([CourseId] ASC);
GO

ALTER TABLE [dbo].[Course]
    ADD CONSTRAINT [DF_Course_Par] DEFAULT ((72)) FOR [Par];
GO

