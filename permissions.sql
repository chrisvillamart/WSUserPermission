
 
CREATE DATABASE N5_PERMISSIONS
GO

USE N5_PERMISSIONS
GO

CREATE TABLE Permissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeForename TEXT NOT NULL,
    EmployeeSurname TEXT NOT NULL,
	PermissionType INT NOT NULL,
	PermissionDate DATE NOT NULL
);


CREATE TABLE PermissionsTypes (
    Id INT FOREIGN KEY REFERENCES Permissions(Id),
    Description TEXT NOT NULL,
);

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee Forename' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'EmployeeForename'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee Surname' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'EmployeeSurname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'PermissionType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission granted on Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'PermissionDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PermissionsTypes', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PermissionsTypes', @level2type=N'COLUMN',@level2name=N'Description'
GO


INSERT INTO Permissions VALUES ('JOSE','MARTINEZ',1, GETDATE())

INSERT INTO PermissionsTypes VALUES (1,'DESCRIPCION')